using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amigyo{
	namespace Fishes{
		public abstract class GroupableScript : MonoBehaviour {
		
			[Range(0.0f, 1.0f)] [Tooltip("このスクリプトによる速度が全体の速度にどれだけ影響するか")] 
			public float VelocityIntensity = 1f;		//強度（このスクリプトによる速度が全体の速度にどの程度影響するか）を表す

			protected float currentIntensity;		//現在の強度（このスクリプトによる速度を無効化したい場合は0にする）

			void Start(){
				currentIntensity = VelocityIntensity;

				//群れのリーダーか、群れを作らない魚しかこのスクリプトを有効化しない
				//（群れのリーダーの場合は、別の関数からInitialize()が呼ばれる）
				if(GetComponent<Group>() == null)
					Initialize();
			}

			//リーダーの場合はここから呼ばれる
			public void SetUp(bool isLeader){

				if(isLeader){
					Initialize();
				}else{
					SetZeroToIntensity();
				}
			}

			protected abstract void Initialize();
			public abstract void Die(List<Group> groupScripts, Group nextLeaderScript);

			public void InitializeIntensity(){
				currentIntensity = VelocityIntensity;
			}
			public void SetZeroToIntensity(){
				currentIntensity = 0;
			}
		}
	}
}
