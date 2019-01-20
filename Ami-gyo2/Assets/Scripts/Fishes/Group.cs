using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Amigyo{
	namespace Fishes{
		public class Group : MonoBehaviour, IFishBehavior {
			
			public float ViewRange_r = 7f;
			[Range(0.0f, 1.0f)]
			public float View2CrowdedRange = 0.5f;
			public int FishAmountInGroup = 3;
			
			[Header("Intensity")]
			[Range(0f, 1f)] [Tooltip("このスクリプトによる速度が全体の速度にどれだけ影響するか")] 
			public float VelocityIntensity = 1f;		//強度（このスクリプトによる速度が全体の速度にどの程度影響するか）を表す

			[Range(0.0f, 1.0f)]	[Tooltip("周りが進もうとする方向に進む強さ")]
			public float DirectionIntensity = 1f;
			[Range(0.0f, 1.0f)]	[Tooltip("集まろうとする強さ")]
			public float GatherIntensity = 1f;
			[Range(0.0f, 1.0f)]	[Tooltip("混雑を感じる強さ")]
			public float CrowdedIntensity = 1f;
			
			public float LeaderMagnifV = 5f;
			public float LeaderMagnifLoc = 5f;

			List<Group> otherGroupScripts;
			bool isLeader;

			public LineRenderer renderer_debug;


			public void SetUp(List<Group> fishScripts){
				isLeader = false;		//この魚が群れのリーダーかどうか（リーダーは、プログラム中で行く先の決定権を持つ）

				//他の魚をリストとして保持
				otherGroupScripts = new List<Group>(fishScripts);
				if(otherGroupScripts[0] == this)
					isLeader = true;
				otherGroupScripts.Remove(this);
				
				//放浪スクリプトの初期化
				var wandering = GetComponent<Wandering>();
				if(wandering != null)
					wandering.SetIsLeader(isLeader);

				//リーダーは生成後、群れのほかの魚が集まるのを待つ
				var leaderGroupScript = (isLeader ? this : otherGroupScripts[0]);
				Observable.TimerFrame(1).Subscribe(_ => {
					var leaderScript = leaderGroupScript.GetComponent<Fish>();
					leaderScript.MultipleSpeed(0.05f);
					Observable.TimerFrame(30).Subscribe(__ => {
						leaderScript.InitializeSpeed();
					}) ;
				});
			}

			public Vector3 GetVelocity(Vector3 currentVelocity){
				Vector3 sumV = Vector3.zero;      //周囲の魚の進行方向の総和
    			Vector3 sumLoc = Vector3.zero;    //周囲の魚の重心
    			Vector3 crowdedSumV = Vector3.zero;    //混雑していない方向の総和

    			float divNumForSumLoc = 0;		//重心を求めるのに用いた魚の数（見えている魚の数だけ位置を足して、この数で割る）
				bool isFirstElement = !isLeader;	//自分が群れのリーダーの場合は、このflagによる分岐処理は必要ない

				//上記3つのベクトルを求める
    			foreach(var fish in otherGroupScripts){

    				Vector3 distV = (transform.position - fish.transform.position);
    				float dist = distV.magnitude;
    				if(dist > ViewRange_r)  continue;		//視認範囲外の魚は考慮しない

					Vector3 addV = fish.GetComponent<Rigidbody>().velocity.normalized;
					float locMagnif = 1;		//質量
					if(isFirstElement){
						addV *= LeaderMagnifV;			//群れのリーダー（最初の参照先）の影響力を倍率として掛ける
						locMagnif = LeaderMagnifLoc;	//リーダーは重心に及ぼす影響力も大きい
					}
					sumV += addV;
    				sumLoc += fish.transform.position * locMagnif;
					divNumForSumLoc += locMagnif;

					isFirstElement = false;		//最初の要素以外はfalse

					//混雑していると感じる範囲よりも距離が大きければ、この先の計算は必要ない
    				if(dist > ViewRange_r*View2CrowdedRange)  continue;

    				crowdedSumV += distV.normalized;
    			}

				//自分以外の魚の重心に向かう方向
    			Vector3 toCentroid = Vector3.zero;
				var loc_debug = Vector3.zero;
    			if(divNumForSumLoc > 0){
    			  sumLoc /= divNumForSumLoc;
    			  loc_debug = sumLoc - transform.position;
				  toCentroid = (sumLoc - transform.position).normalized;
    			}

				//周囲の魚の進行方向、重心、混雑していない方向をすべて足したもの
				var velocity = (sumV + toCentroid + crowdedSumV);

				//デバッグ用（速度の方向をLineRendererで描画
				if(renderer_debug != null && loc_debug != Vector3.zero){
					renderer_debug.SetPosition(0, this.transform.position);
					renderer_debug.SetPosition(1, this.transform.position + loc_debug);
					renderer_debug.SetColors(Color.yellow, Color.red);
				}
				//////

    			return velocity.normalized * VelocityIntensity;
			}

			public void Die(System.Action<int> decreaseFishCount, int id){
				
				if(otherGroupScripts.Count == 0){
					decreaseFishCount(id);		//群れ1つでFishCount1つ分なので、群れの魚がすべて消えない限り、decreaseしちゃいけない
				}else{
					foreach(var otherScript in otherGroupScripts)
						otherScript.GetComponent<Group>().Delete(this);	//他の魚がもつListから自分を削除
				}
			}

			//Listから指定された魚を削除する
			public void Delete(Group script){
				otherGroupScripts.Remove(script);
			}

		}
	}
}