using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Amigyo{
	public class SoundController : MonoBehaviour {

		public AudioClip BGM;
		List<AudioSource> sources;

		void Start(){
			sources = GetComponents<AudioSource>().ToList();

			foreach(var source in sources){				
				source.playOnAwake = false;
			}

			var bgmSource = sources[0];
			bgmSource.loop = true;
			bgmSource.clip = BGM;
			bgmSource.Play();
		}

		public void Play(AudioClip clip){

			foreach(var source in sources){
				if(source.isPlaying)	continue;
				else{
					source.clip = clip;
					source.Play();
					break;
				}
			}
			
		}
	}
}