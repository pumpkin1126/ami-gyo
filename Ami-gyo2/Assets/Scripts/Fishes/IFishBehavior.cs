using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amigyo{
	namespace Fishes{
		public interface IFishBehavior{
		
			Vector3 GetVelocity(Vector3 currentVelocity);
		}
	}
}
