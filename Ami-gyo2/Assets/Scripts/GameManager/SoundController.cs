using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	AudioSource source;

	void Start(){
		source = GetComponent<AudioSource>();
	}

	public void Play(AudioClip clip, bool isBGM){
		source.clip = clip;
		source.Play();
	}
}
