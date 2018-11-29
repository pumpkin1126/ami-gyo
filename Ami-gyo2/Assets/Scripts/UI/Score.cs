using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public GameObject Score_object = null;
	public int score_num = 0;

	void Start () {
	}
	
	void Update () {
	Text score_text = Score_object.GetComponent<Text>();
	score_text.text = "Score:" + score_num;	
	score_num += 1;
	}
}
