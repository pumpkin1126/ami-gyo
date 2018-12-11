using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Fishes;

namespace Amigyo{
	namespace Spawners{
		public class StandardSpawner : Spawner {

			//@super Fields
			//protected IReadOnlyList<GameObject> fishPrefabs;
			//public GameParams gameParams;

			public Vector3 upperLeft;
			public Vector3 lowerRight;

			public override void Activate(){
				//foreach(var prefab in fishPrefabs)
				//	Debug.Log(prefab.name);
			}
			
			public override void Update () {
				//Debug.Log("update");
			}
		}
	}
}