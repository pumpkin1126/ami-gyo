using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amigyo{
	namespace UI{
		public class WaterTank : MonoBehaviour {
		
			public ScoreUI scoreUI;

			void OnTriggerEnter2D(Collider2D col2D){
			
				scoreUI.OnTriggerEnter2D(col2D);

			}
		}
	}
}
