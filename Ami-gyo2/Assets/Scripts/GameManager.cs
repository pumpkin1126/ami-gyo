using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	public int RemainingTime{ get{return remainingTime;}}
	public int Score{ get{return score;}}

	int remainingTime;
	int score;

	void Awake(){
		if(GameManager.Instance == null)
			Instance = this;
	}

	void Start () {
		score = 0;
	}
	
	void Update () {
		
	}

	public void CalculateScore(FishInfo info){
		score += info.Weight;
		time += info.BonusTime;

		Debug.Log("FishInfo\t weight: "+info.Weight+"  time: "+info.BonusTime+"  event: "+info.EventName);
	}
}
