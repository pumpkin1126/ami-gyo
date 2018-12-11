using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Amigyo;

public class Timer : MonoBehaviour {

	public GameObject Time_object = null;

	void Start () {
	}

	void Update () {
	Text Time_text = Time_object.GetComponent<Text>();
	Time_text.text = "Time:" + GameManager.Instance.RemainingTime;
	}
}
