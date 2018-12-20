using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Amigyo.Fishes;
using System.Linq;

namespace Amigyo{
	namespace Spawners{
		public class SpawnerHolder : MonoBehaviour {

			public GameParams gameParams;
			public SpawnerLinkSettings links;
			public GameObject Area;

			Dictionary<EventType, Spawner> spawners;
			//Dictionary<FishEnum, GameObject> prefabs;
			
			Spawner spawner;

			void Start () {
				spawners = new Dictionary<EventType, Spawner>(){
					{EventType.None, new StandardSpawner()}
				};

				var prefabs = links.GetFishPrefabs();

				foreach(var pair in spawners){
					var info = links.GetSpawnerInfo(pair.Key);
					var fishTypes = info.FishTypes;
					//var selectedPrefabs = fishTypes.Select(type => prefabs[type]).ToList();
					var selectedPrefabs = fishTypes.ToDictionary(type => type, type => prefabs[type]);

					pair.Value.SetUp(selectedPrefabs, Area, gameParams, info);
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