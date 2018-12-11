using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amigyo{
	namespace Fishes{
		[CreateAssetMenu( menuName = "Scriptable/FishInfo")]
		public class FishInfo : ScriptableObject{

			[SerializeField] int weight;
			[SerializeField] int bonusSecond;
			[SerializeField] EventType eventName;

			public int Weight{ get{return weight;} }
			public int BonusSecond{ get{return bonusSecond;} }
			public EventType EventName{ get{return eventName;} }
		}
	}
}