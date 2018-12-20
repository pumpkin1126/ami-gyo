using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Fishes;

namespace Amigyo{
	namespace Spawners{

		public abstract class Spawner{

			protected Dictionary<FishEnum, GameObject> fishPrefabs;
			protected GameObject area;
			protected GameParams gameParams;
			protected SpawnerInfo info;

			public void SetUp(Dictionary<FishEnum, GameObject> prefabs, GameObject area, GameParams gameParams, SpawnerInfo info){
				fishPrefabs = prefabs;
				this.area = area;
				this.gameParams = gameParams;
				this.info = info;
			}

			public abstract void Activate();
			public abstract void Update();
		}
	}
}