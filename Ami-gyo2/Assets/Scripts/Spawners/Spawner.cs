using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Amigyo.Fishes;

namespace Amigyo{
	namespace Spawners{

		public abstract class Spawner{

			protected IReadOnlyList<FishElement> fishElements;
			protected GameObject area;
			protected GameParams gameParams;
			protected SpawnerInfo info;

			public void SetUp(GameObject area, GameParams gameParams, SpawnerInfo info){
				fishElements = info.FishPrefabs.Select(prefab => new FishElement(prefab)).ToList();
				this.area = area;
				this.gameParams = gameParams;
				this.info = info;
			}

			public abstract void Activate();
			public abstract void Update();
		}

		public class FishElement{
			public GameObject prefab;
			public int existCount;

			public FishElement(GameObject prefab){
				this.prefab = prefab;
				existCount = 0;
			}
		}
	}
}