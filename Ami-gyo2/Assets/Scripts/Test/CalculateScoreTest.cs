using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Fishes;

namespace Amigyo{
	public class CalculateScoreTest : MonoBehaviour {

		public FishInfo info;

		GameManager gm;
		bool once = true;

		void Start () {
			gm = GameManager.Instance;
		}
		
		void Update () {
			
			if(gm.RemainingTime < 55 && once){
				
				gm.CalculateScore(info);
				once = false;
			}
			
		}
	}
}