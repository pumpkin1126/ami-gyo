using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Amigyo.Fishes;

namespace Amigyo{
	public class GameManager : MonoBehaviour {

		public static GameManager Instance = null;
		
		public GameParams gameParams;
		public Canvas canvas;

		public SoundController soundController;


		void Awake(){
			if(GameManager.Instance == null)
				Instance = this;
		}


		void Start () {
			soundController = GetComponentInChildren<SoundController>();
			/*
			score = 0;
			startTime = Time.time;
			additionalTime = 0;

			remainingTime = gameParams.TimeLimit;
			*/
		}

		/*
		void Update () {
			setTime();
		}

		void setTime(){
			remainingTime = (int)(gameParams.TimeLimit - (Time.time - startTime)) + additionalTime;
		}*/

		public void CalculateScore(FishInfo info, Vector3 collidePosition){
			/* score += info.Weight;
			additionalTime += info.BonusSecond;
			*/

			//UI表示
			var uiPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, collidePosition);
			canvas.GetComponent<IconCreator>().CreateWeightIcon(uiPosition, info.Weight);
		}

		public void PlaySound(AudioClip clip){
			soundController.Play(clip);
		}
	}
}