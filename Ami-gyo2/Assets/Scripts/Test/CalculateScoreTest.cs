using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fish;

public class CalculateScoreTest : MonoBehaviour {

	GameManager gm;
	bool once = true;

	void Start () {
		gm = GameManager.Instance;
	}
	
	void Update () {
		if(gm.RemainingTime < 55 && once){
			var info = new FishInfo(100, 5, EventType.None);
			gm.CalculateScore(info);
			once = false;
		}
	}
}
