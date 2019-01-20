using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Amigyo.Spawners;

namespace Amigyo{
	namespace Fishes{
		public class Wandering : MonoBehaviour, IFishBehavior {
			const float Extents2turnR = 1/5f;
			const float Extents2MinDistance = 1/4f;
			const float MinDeg = 180f;
			const float MaxDeg = 270f;
			const int MaxTurnTimes = 10;
			const int MinTurnTimes = 5;

			[Range(0.0f, 1.0f)] [Tooltip("このスクリプトによる速度が全体の速度にどれだけ影響するか")] 
			public float VelocityIntensity = 1f;		//強度（このスクリプトによる速度が全体の速度にどの程度影響するか）を表す

			int turnCount;
			int turnCountMax;

			Vector3 AreaExtents;
			Vector3 nextDistance;
			Vector3 turnedLocation;
			bool isTurning = false;
			Quaternion omega;
			float destinationRad = 0;
			Vector3 currentVelocity;

			List<Fish> otherWanderScripts;
			bool isLeader = true;

			void Start(){

				//群れのリーダーか、群れを作らない魚しかこのスクリプトを有効化しない
				//（群れのリーダーの場合は、別の関数からSetUp()が呼ばれる）
				if(GetComponent<Group>() == null)
					SetUp();
			}

			void SetUp(){
				turnCount = 0;
				turnCountMax = (int)(Random.value * (MaxTurnTimes - MinTurnTimes)) + MinTurnTimes;
				
				var Area = GameManager.Instance.GetComponent<SpawnerHolder>().Area;
				AreaExtents = Area.GetComponent<BoxCollider>().bounds.extents;
				var direction = (Area.transform.position - transform.position);
				direction += direction.normalized * AreaExtents.x/5;

				nextDistance = direction;
				turnedLocation = transform.position;
			}

			void Update(){
				if(!isLeader)	return;

				var currentDistance = (turnedLocation - transform.position);

				
				//ターン中もしくは今後ターンしないなら、ここで終了
				if(!isTurning && turnCount > turnCountMax)	return;

				//一定距離動いたら、ターン
				if(currentDistance.magnitude > nextDistance.magnitude){
					isTurning = true;

					//回転の様々な初期設定
					destinationRad = (Random.value * (MaxDeg - MinDeg) + MinDeg)*Mathf.Deg2Rad;
					float rad = GetComponent<Fish>().Speed / (AreaExtents.x/Extents2turnR);
					omega = Quaternion.AngleAxis(rad*Mathf.Rad2Deg, Vector3.up);
				}

			}

			public Vector3 GetVelocity(Vector3 _currentVelocity){
				if(!isLeader)	return Vector3.zero;
				
				if(isTurning){
					
					//ある程度回ったならターン終了（次の直進方向などを決めて、速度は前フレームから変えずにreturn
					var startVector = nextDistance;
					if(StaticTools.GetAngleFromVector(currentVelocity, startVector) > destinationRad){
						isTurning = false;
						var additionalDist = Mathf.Abs(StaticTools.Gaussian(0, 1)/2 * AreaExtents.x/5);
						nextDistance = transform.forward * (additionalDist + AreaExtents.x*Extents2MinDistance);
						turnedLocation = this.transform.position;
						turnCount++;

						return currentVelocity;
					}

					currentVelocity = omega * currentVelocity;

				}else
					currentVelocity = nextDistance.normalized;				//回ってない間はまっすぐ進む
				
				return currentVelocity * VelocityIntensity;
			}

			public void SetIsLeader(bool isLeader){
				this.isLeader = isLeader;
				if(isLeader)
					SetUp();
			}

		}
	}
}
