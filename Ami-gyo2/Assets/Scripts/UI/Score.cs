using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Amigyo;

public class Score : MonoBehaviour {

	public GameObject Score_object = null;

	void Start () {
	}
	
	void Update () {
	Text score_text = Score_object.GetComponent<Text>();
	score_text.text = "Score:" + GameManager.Instance.Score;
	}
}
