using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Fishes;

namespace Amigyo{
	namespace Spawners{
		[CreateAssetMenu( menuName = "Scriptable/SpawnerInfo")]
		public class SpawnerInfo : ScriptableObject {

			[SerializeField]
			private List<FishEnum> fishTypes;

			public IReadOnlyList<FishEnum> FishTypes{
				get{	return fishTypes;	}
			}
		}
	}
}