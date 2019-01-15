using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amigyo{
	namespace Fishes{
		public class Wandering : MonoBehaviour, IFishBehavior {
		
			public Vector3 GetVelocity(Vector3 currentVelocity){
				return Vector3.zero;
			}
		}
	}
}
