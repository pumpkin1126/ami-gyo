using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Amigyo{
	namespace UI{
		public class ScoreUI : MonoBehaviour {

			public GameObject ScoreText = null;
			public AudioClip AddScore;

			public int Score{ get{return score;}}

			TextMeshProUGUI scoreText;
			int score;

			void Start () {
				scoreText = ScoreText.GetComponent<TextMeshProUGUI>();
			}

			void Update () {
				//Text score_text = Score_object.GetComponent<Text>();
				scoreText.text = score + " kg";
			}

			public void OnTriggerEnter2D(Collider2D col2D){
				score += col2D.GetComponent<Icon>().Score;	//スコア追加
				GameManager.Instance.PlaySound(AddScore);	//音再生する


				Destroy(col2D.gameObject);
			}
		}
	}
}