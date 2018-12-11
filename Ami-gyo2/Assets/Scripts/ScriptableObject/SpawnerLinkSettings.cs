using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Amigyo.Fishes;

namespace Amigyo{
	namespace Spawners{

		[CreateAssetMenu( menuName = "Scriptable/SpawnerLink")]
		public class SpawnerLinkSettings : ScriptableObject {

			[Header("Spawner Infos")]
			[SerializeField] SpawnerInfo fishList_Standard;
			[SerializeField] SpawnerInfo fishList_MassGeneration;
			[SerializeField] SpawnerInfo fishList_BigFish;

			[Header("Fish Prefabs")]
			[SerializeField] GameObject bigFish;
			[SerializeField] GameObject groupFish;
			[SerializeField] GameObject standardFish;
			
			public SpawnerInfo GetSpawnerInfo(EventType type){
				switch(type){
					case EventType.None:			return fishList_Standard;
					case EventType.MassGeneration:	return fishList_MassGeneration;
					case EventType.StrongFish:		return fishList_BigFish;
					default: throw new NullReferenceException();
				}
			}

			public Dictionary<FishEnum, GameObject> GetFishPrefabs(){
				return new Dictionary<FishEnum, GameObject>(){
					{FishEnum.BigFish, bigFish}, {FishEnum.GroupFish, groupFish}, {FishEnum.StandardFish, standardFish},
				};
			}

		}
	}
}