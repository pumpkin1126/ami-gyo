using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Amigyo{
	namespace UI{
		public class ScoreUI : MonoBehaviour {

			public GameObject ScoreText = null;

			public int Score{ get{return score;}}

			Text scoreText;
			int score;

			void Start () {
				scoreText = ScoreText.GetComponent<Text>();
			}

			void Update () {
				//Text score_text = Score_object.GetComponent<Text>();
				scoreText.text = "Score:" + GameManager.Instance.Score;
			}

			void OnTriggerEnter2D(Collider2D col2D){
				Debug.LogWarning("OnTriggerEnter");

				//スコア追加
				//音再生する
				Destroy(col2D.gameObject);

			}
		}
	}
}