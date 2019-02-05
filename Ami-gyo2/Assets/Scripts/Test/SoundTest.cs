using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Amigyo{
	public class SoundTest : MonoBehaviour {

		public float interval1 = 2f;
		public float interval2 = 3f;

		public AudioClip clip1;
		public AudioClip clip2;

		void Start () {
			Observable.Interval(TimeSpan.FromSeconds(interval1)).Subscribe(_ => GameManager.Instance.PlaySound(clip1));
			Observable.Interval(TimeSpan.FromSeconds(interval2)).Subscribe(_ => GameManager.Instance.PlaySound(clip2));
		}

	}
}
