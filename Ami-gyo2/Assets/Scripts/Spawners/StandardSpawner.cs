
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Fishes;
using UniRx;
using UniRx.Triggers;

namespace Amigyo{
	namespace Spawners{
		public class StandardSpawner : Spawner {

			//@super Fields
			//protected IReadOnlyList<GameObject> fishElements;
			//protected GameObject area;
			//protected GameParams gameParams;
			//protected SpawnerInfo info;

			List<System.IDisposable> disposables;

			float marginFromArea;		//範囲の外枠から、この距離だけ離して生成
			Vector3 Range;		//範囲÷2

			public override void Activate(){
				if(disposables != null)
					foreach(var dispose in disposables)	dispose.Dispose();

				disposables = new List<System.IDisposable>();

				//コライダーの読み取り
				marginFromArea = 1f;
				var collider = area.GetComponent<BoxCollider>();
				Range = collider.bounds.size;

				//Debug.LogWarning("Activate");

				//生成タイミングは3秒おき（種類ごとにオフセットはランダム）

				for(int i = 0; i < fishElements.Count; i++){
					int index = i;		//iはforの最後で++されるため、isSpawableにそのまま使うとOutOfRangeする

					Observable.Timer(System.TimeSpan.FromSeconds(Random.value*5)).Take(1).Subscribe(_ => {
						//Debug.LogWarning("Timer Set");

						disposables.Add(Observable.Interval(System.TimeSpan.FromSeconds(3f)).Subscribe(__ => {
							//Debug.LogWarning("Create timing");
							if(isSpawable(index)){
								Instantiate(index);
							}
						}));
					});
				}
			}
			
			public override void Update () {
				//特に何も書くことなかった
				
				/*
				string s = "";
				foreach(var pair in fishCounts)
					s += pair + "    ";

				Debug.Log(s);
				*/
			}

			
			bool isSpawable(int index){
				return (fishElements[index].prefab.GetComponent<Fish>().info.MaxAmount > fishElements[index].existCount);
			}

			void Instantiate(int index){
				int side = (int)(UnityEngine.Random.value * 3);

				var spawnPoint = Vector3.zero;

				switch(side%2){
					case 0: spawnPoint.z = Range.z * UnityEngine.Random.value - Range.z/2;	break;
					case 1: spawnPoint.x = Range.x * UnityEngine.Random.value - Range.x/2;	spawnPoint.z = Range.z/2 + marginFromArea; break;
				}

				if(side%2 == 0){
					spawnPoint.x = (side - 1) * (marginFromArea + Range.x/2);
				}

				spawnPoint += area.transform.position;

				var rotation = Quaternion.LookRotation(area.transform.position - spawnPoint);
				var fishObj = GameObject.Instantiate(fishElements[index].prefab, spawnPoint, rotation);
				fishObj.GetComponent<Fish>().SetIdAndDieMethod(index, DecreaseFishCount);
				
				fishElements[index].existCount++;
			}

			public void DecreaseFishCount(int index){
				fishElements[index].existCount--;
			}
		}
	}
}