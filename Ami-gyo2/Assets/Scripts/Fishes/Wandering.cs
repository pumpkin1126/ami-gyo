using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Amigyo.Spawners;

namespace Amigyo{
	namespace Fishes{
		public class Wandering : MonoBehaviour, IFishBehavior, IGroupable {
			const float Extents2turnR = 1/5f;
			const float Extents2MinDistance = 1/4f;
			const float MinDeg = 180f;
			const float MaxDeg = 270f;
			const int MaxTurnTimes = 10;
			const int MinTurnTimes = 5;

			[Range(0.0f, 1.0f)] [Tooltip("このスクリプトによる速度が全体の速度にどれだけ影響するか")] 
			public float VelocityIntensity = 1f;		//強度（このスクリプトによる速度が全体の速度にどの程度影響するか）を表す

			public WanderingValues values;

			void Start(){

				//群れのリーダーか、群れを作らない魚しかこのスクリプトを有効化しない
				//（群れのリーダーの場合は、別の関数からSetUp()が呼ばれる）
				if(GetComponent<Group>() == null)
					SetUp();
			}

			void SetUp(){
				values = new WanderingValues();
				var v = values;

				v.turnCount = 0;
				v.turnCountMax = (int)(Random.value * (MaxTurnTimes - MinTurnTimes)) + MinTurnTimes;
				
				var Area = GameManager.Instance.GetComponent<SpawnerHolder>().Area;
				v.AreaExtents = Area.GetComponent<BoxCollider>().bounds.extents;
				var direction = (Area.transform.position - transform.position);
				direction += direction.normalized * v.AreaExtents.x/5;

				v.nextDistance = direction;
				v.turnedLocation = transform.position;
			}

			void Update(){
				var v = values;
				if(v == null)	return;

				var currentDistance = (v.turnedLocation - transform.position);

				
				//ターン中もしくは今後ターンしないなら、ここで終了
				if(!v.isTurning && v.turnCount > v.turnCountMax)	return;

				//一定距離動いたら、ターン
				if(currentDistance.magnitude > v.nextDistance.magnitude){
					v.isTurning = true;

					//回転の様々な初期設定
					v.destinationRad = (Random.value * (MaxDeg - MinDeg) + MinDeg)*Mathf.Deg2Rad;
					float rad = GetComponent<Fish>().Speed / (v.AreaExtents.x/Extents2turnR);
					v.omega = Quaternion.AngleAxis(rad*Mathf.Rad2Deg, Vector3.up);
				}

			}

			public Vector3 GetVelocity(Vector3 _currentVelocity){
				if(values == null)	return Vector3.zero;
				var v = values;
				
				if(v.isTurning){
					
					//ある程度回ったならターン終了（次の直進方向などを決めて、速度は前フレームから変えずにreturn
					var startVector = v.nextDistance;
					if(StaticTools.GetAngleFromVector(v.currentVelocity, startVector) > v.destinationRad){
						v.isTurning = false;
						var additionalDist = Mathf.Abs(StaticTools.Gaussian(0, 1)/2 * v.AreaExtents.x/5);
						v.nextDistance = transform.forward * (additionalDist + v.AreaExtents.x*Extents2MinDistance);
						v.turnedLocation = this.transform.position;
						v.turnCount++;

						return v.currentVelocity;
					}

					v.currentVelocity = v.omega * v.currentVelocity;

				}else
					v.currentVelocity = v.nextDistance.normalized;				//回ってない間はまっすぐ進む
				
				return v.currentVelocity * VelocityIntensity;
			}

			//リーダーが死んだときに、放浪に関する変数の値を引き継ぐ（引数がnullの場合は自分が最初のリーダー）
			public void SetWanderingValues(WanderingValues v){
				if(v == null)	SetUp();
				else			values = v;
			}

			public void Die(List<Group> groupScripts, Group nextLeaderScript){
				if(nextLeaderScript != null)
					nextLeaderScript.GetComponent<Wandering>().InheritLeader(values);
			}

			public void InheritLeader(WanderingValues v){
				values = v;
			}

		}

		public class WanderingValues{
			public int turnCount;
			public int turnCountMax;
			public Vector3 AreaExtents;
			public Vector3 nextDistance;
			public Vector3 turnedLocation;
			public bool isTurning = false;
			public Quaternion omega;
			public float destinationRad = 0;
			public Vector3 currentVelocity;
		}
	}
}
