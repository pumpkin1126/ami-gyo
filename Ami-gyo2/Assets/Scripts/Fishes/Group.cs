using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amigyo{
	namespace Fishes{
		[RequireComponent (typeof(Migration))]
		public class Group : MonoBehaviour, IFishBehavior {

			public int FishAmountInGroup;
			List<Group> otherFishScripts;

			public void SetUp(List<Group> fishScripts){
				otherFishScripts = new List<Group>(fishScripts);
				otherFishScripts.Remove(this);
			}

			public Vector3 GetVelocity(Vector3 currentVelocity){
				return Vector3.zero;
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