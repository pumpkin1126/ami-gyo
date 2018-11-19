using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fish{
	public class FishInfo : MonoBehaviour {

		int weight;
		int bonusTime;
		EventType eventName;

		public int Weight{ get{return weight;} }
		public int BonusTime{ get{return bonusTime;} }
		public EventType EventName{ get{return eventName;} }

		public FishInfo(int _weight, int _bonusTime, EventType _eventName){
			weight = _weight;
			bonusTime = _bonusTime;
			eventName = _eventName;
		}
	}
}
