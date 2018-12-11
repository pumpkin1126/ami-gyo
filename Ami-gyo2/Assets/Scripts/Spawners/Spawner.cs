using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Fishes;

namespace Amigyo{
	namespace Spawners{

		public abstract class Spawner{

			protected IReadOnlyList<GameObject> fishPrefabs;
			public GameParams gameParams;

			public void SetUp(List<GameObject> prefabs){
				fishPrefabs = prefabs;
			}

			public abstract void Activate();
			public abstract void Update();
		}
	}
}