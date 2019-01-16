using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amigyo{
	namespace Fishes{
		public class Group : MonoBehaviour, IFishBehavior {
			
			public float ViewRange_r = 7f;
			[Range(0.0f, 1.0f)]
			public float View2CrowdedRange = 0.5f;
			public int FishAmountInGroup = 3;
			List<Group> otherFishScripts;


			public void SetUp(List<Group> fishScripts){
				otherFishScripts = new List<Group>(fishScripts);
				otherFishScripts.Remove(this);
			}

			public Vector3 GetVelocity(Vector3 currentVelocity){
				Vector3 addV = Vector3.zero;      //周囲の魚の進行方向の総和
    			Vector3 addLoc = Vector3.zero;    //周囲の魚の重心
    			Vector3 crowdedAddV = Vector3.zero;    //混雑していない方向の総和

    			float divNumForAddLoc = 0;		//重心を求めるのに用いた魚の数（見えている魚の数だけ位置を足して、この数で割る）

				//上記3つのベクトルを求める
    			foreach(var fish in otherFishScripts){

    				Vector3 distV = (transform.position - fish.transform.position);
    				float dist = distV.magnitude;
    				if(dist > ViewRange_r)  continue;		//視認範囲外の魚は考慮しない

    				addV += fish.GetComponent<Rigidbody>().velocity.normalized;
    				addLoc += fish.transform.position;
					divNumForAddLoc++;

					//混雑していると感じる範囲よりも距離が大きければ、この先の計算は必要ない
    				if(dist > ViewRange_r*View2CrowdedRange)  continue;

    				crowdedAddV += distV.normalized;
    			}

				//自分以外の魚の重心に向かう方向
    			Vector3 toCentroid = Vector3.zero;
    			if(divNumForAddLoc > 0){
    			  addLoc /= divNumForAddLoc;
    			  toCentroid = (addLoc - transform.position).normalized;
    			}

				//周囲の魚の進行方向、重心、混雑していない方向をすべて足したもの
    			return (addV + toCentroid + crowdedAddV).normalized;
			}

			public void Die(System.Action<int> decreaseFishCount, int id){
				
				if(otherFishScripts.Count == 0){
					decreaseFishCount(id);		//群れ1つでFishCount1つ分なので、群れの魚がすべて消えない限り、decreaseしちゃいけない
				}else{
					foreach(var otherScript in otherFishScripts)
						otherScript.GetComponent<Group>().Delete(this);	//他の魚がもつListから自分を削除
				}
			}

			//Listから指定された魚を削除する
			public void Delete(Group script){
				otherFishScripts.Remove(script);
			}

		}
	}
}