using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amigyo{
	namespace Fishes{
		public interface IGroupable {
		
			void Die(List<Group> groupScripts, Group nextLeaderScript);
		}
	}
}
