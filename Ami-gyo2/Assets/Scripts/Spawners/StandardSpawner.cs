
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Fishes;
using UniRx;
using UniRx.Triggers;
using System.Linq;

namespace Amigyo{
	namespace Spawners{
		public class StandardSpawner : Spawner {
			const float OffsetRandomSec = 3f;
			const float IntervalRandomSec = 4f;

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


				//生成タイミングは3秒おき（種類ごとにオフセットはランダム）

				for(int i = 0; i < fishElements.Count; i++){
					int index = i;		//iはforの最後で++されるため、isSpawableにそのまま使うとOutOfRangeする

					Observable.Timer(System.TimeSpan.FromSeconds(Random.value*OffsetRandomSec)).Take(1).Subscribe(_ => {
						//Debug.LogWarning("Timer Set");

						disposables.Add(Observable.Interval(System.TimeSpan.FromSeconds(IntervalRandomSec)).Subscribe(__ => {
							//Debug.LogWarning("Create timing");
							if(isSpawable(index)){
								Instantiate(index);
							}
						}));
					});
				}
			}
			
			public override void Update () {
				
			}

			
			bool isSpawable(int index){
				return (fishElements[index].prefab.GetComponent<Fish>().info.MaxAmount > fishElements[index].existCount);
			}

			//一連の生成処理（群れの魚の生成なら、その群れに含まれるすべての魚を生成する処理）

			void Instantiate(int index){
				var centerPoint = GetInstantiateLocation();

				var prefab = fishElements[index].prefab;

				int end = 1;

				//群れかどうかのチェック
				var groupScript = prefab.GetComponent<Group>();
				if(groupScript != null){
					end = groupScript.FishAmountInGroup;
					if(end == 0)	Debug.LogError(prefab.name+"プレハブのGroupスクリプトのFishAmountInGroupが0になっています");
				}

				var spawnFishScripts = new List<Group>();		//生成した魚を一時的に保持

				for(int i = 0; i < end; i++){
					var spawnPoint = centerPoint;
					//群れの場合、魚のスポーン地点が重ならないようにする（群れじゃない場合はforが1回しか呼ばれない）
					if(i != 0)	spawnPoint += new Vector3(Random.value, 0, Random.value)*Range.x/5;
					var spawnFish = Spawn(index, spawnPoint);
					spawnFishScripts.Add(spawnFish.GetComponent<Group>());
				}

				fishElements[index].existCount++;		//現在存在している魚の数++（群れの場合は群れ1つでカウント1つ分）

				if(end == 1)	return;

				//群れを作る魚にほかの魚を渡す
				foreach(var fishScript in spawnFishScripts)
					fishScript.SetUp(spawnFishScripts);

			}

			//一匹の生成処理

			GameObject Spawn(int index, Vector3 spawnPoint){

				var fishObj = GameObject.Instantiate(fishElements[index].prefab, spawnPoint, Quaternion.identity);
				fishObj.GetComponent<Fish>().SetIdAndDieMethod(index, DecreaseFishCount);

				return fishObj;
			}

			Vector3 GetInstantiateLocation(){
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
				return spawnPoint;
			}

			public void DecreaseFishCount(int index){
				fishElements[index].existCount--;
			}
		}
	}
}