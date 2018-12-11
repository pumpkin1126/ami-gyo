using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Fishes;

namespace Amigyo{
	public class GameManager : MonoBehaviour {

		public static GameManager Instance = null;
		
		public GameParams gameParams;

		public int RemainingTime{ get{return remainingTime;}}
		public int Score{ get{return score;}}

		int remainingTime;
		int score, additionalTime;

		float startTime;

		void Awake(){
			if(GameManager.Instance == null)
				Instance = this;
		}

		void Start () {
			score = 0;
			startTime = Time.time;
			additionalTime = 0;

			remainingTime = gameParams.TimeLimit;
		}
		
		void Update () {
			setTime();
		}

		void setTime(){
			remainingTime = (int)(gameParams.TimeLimit - (Time.time - startTime)) + additionalTime;
		}

		public void CalculateScore(FishInfo info){
			score += info.Weight;
			additionalTime += info.BonusSecond;

			Debug.Log("FishInfo\t weight: "+info.Weight+"  time: "+info.BonusSecond+"  event: "+info.EventName);
		}
	}
}