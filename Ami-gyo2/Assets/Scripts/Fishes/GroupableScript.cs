using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amigyo{
	namespace Fishes{
		public abstract class GroupableScript : MonoBehaviour {
		
			void Start(){

				//群れのリーダーか、群れを作らない魚しかこのスクリプトを有効化しない
				//（群れのリーダーの場合は、別の関数からInitialize()が呼ばれる）
				if(GetComponent<Group>() == null)
					Initialize();
			}

			//リーダーの場合はここから呼ばれる
			public void SetUp(bool isLeader){

				if(isLeader){
					Initialize();
				}
			}

			protected abstract void Initialize();
			public abstract void Die(List<Group> groupScripts, Group nextLeaderScript);
		}
	}
}
