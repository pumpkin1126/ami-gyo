using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float countTime = 300;
	public GameObject Time_object = null;

	void Start () {
	}

	void Update () {
	Text Time_text = Time_object.GetComponent<Text>();
	Time_text.text = "Time:" + countTime;
	countTime -= Time.deltaTime;
	}
}
