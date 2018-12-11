using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Amigyo.Fishes;
using System.Linq;

namespace Amigyo{
	namespace Spawners{
		public class SpawnerHolder : MonoBehaviour {

			public SpawnerLinkSettings links;
			public GameParams gameParams;

			Dictionary<EventType, Spawner> spawners;
			//Dictionary<FishEnum, GameObject> prefabs;
			
			Spawner spawner;

			void Start () {
				spawners = new Dictionary<EventType, Spawner>(){
					{EventType.None, new StandardSpawner()}
				};

				var prefabs = links.GetFishPrefabs();

				foreach(var pair in spawners){
					var fishTypes = links.GetSpawnerInfo(pair.Key).FishTypes;
					var selectedPrefabs = fishTypes.Select(type => prefabs[type]).ToList();
					
					pair.Value.SetUp(selectedPrefabs);
					pair.Value.gameParams = gameParams;
				}

				ChangeSpawner(EventType.None);
			}
			
			void Update () {
				spawner.Update();
			}

			public void ChangeSpawner(EventType type){
				if(!spawners.ContainsKey(type))
					throw new NullReferenceException();

				spawner = spawners[type];
				spawner.Activate();
			}

		}
	}
}