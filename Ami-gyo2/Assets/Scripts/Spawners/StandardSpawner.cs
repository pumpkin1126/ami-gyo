
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
			//protected Dictionary<FishEnum, GameObject> fishPrefabs;
			//protected GameObject area;
			//protected GameParams gameParams;
			//protected SpawnerInfo info;

			Dictionary<FishEnum, int> fishCounts;
			List<System.IDisposable> disposables;

			float marginFromArea;		//範囲の外枠から、この距離だけ離して生成
			Vector3 Range;		//範囲÷2

			public override void Activate(){
				if(disposables != null)
					foreach(var dispose in disposables)	dispose.Dispose();

				disposables = new List<System.IDisposable>();

				//魚カウンターの初期化
				fishCounts = new Dictionary<FishEnum, int>();
				foreach(var enu in info.FishTypes){
					fishCounts.Add((FishEnum)enu, 0);
				}

				//コライダーの読み取り
				marginFromArea = 1f;
				var collider = area.GetComponent<BoxCollider>();
				Range = collider.bounds.size;

				//Debug.LogWarning("Activate");

				//生成タイミングは3秒おき（種類ごとにオフセットはランダム）
				foreach(var pair in fishPrefabs){
					Observable.Timer(System.TimeSpan.FromSeconds(Random.value*5)).Take(1).Subscribe(_ => {
						//Debug.LogWarning("Timer Set");
						disposables.Add(Observable.Interval(System.TimeSpan.FromSeconds(3f)).Subscribe(__ => {
							//Debug.LogWarning("Create timing");
							if(isSpawable(pair.Key))
								Instantiate(pair.Key);
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

			
			bool isSpawable(FishEnum fishEnum){
				return (gameParams.GetMaxAmount(fishEnum) > fishCounts[fishEnum]);
			}

			void Instantiate(FishEnum fishEnum){
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

				GameObject.Instantiate(fishPrefabs[fishEnum], spawnPoint, Quaternion.identity);
				fishCounts[fishEnum]++;
			}

			public void DecreaseFishCount(FishEnum enu){
				fishCounts[enu]--;
			}
		}
	}
}