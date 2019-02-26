using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Amigyo{
	namespace UI{
		public class Timer : MonoBehaviour {
		
			//public GameObject Time_object = null;

			int time;
			Text TimerText;

			void Start () {
				TimerText = GetComponent<Text>();
			}

			void Update () {
				TimerText.text = "Time:" + time;
			}
		}
	}
}