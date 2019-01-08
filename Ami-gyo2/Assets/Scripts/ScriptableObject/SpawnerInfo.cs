using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Fishes;

namespace Amigyo{
	namespace Spawners{
		[CreateAssetMenu( menuName = "Scriptable/SpawnerInfo")]
		public class SpawnerInfo : ScriptableObject {

			[SerializeField]
			private List<GameObject> fishPrefabs;


			public IReadOnlyList<GameObject> FishPrefabs{
				get{	return fishPrefabs;	}
			}
		}
	}
}