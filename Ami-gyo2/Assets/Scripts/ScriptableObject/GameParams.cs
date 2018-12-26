using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Fishes;

namespace Amigyo{
	[CreateAssetMenu( menuName = "Scriptable/GameParams", fileName = "GameParams" )]
	public class GameParams : ScriptableObject{

		public int TimeLimit{ get{return timeLimit;}}

		[SerializeField] int timeLimit;
		[SerializeField] int maxAmount_standardFish;

		public int GetMaxAmount(FishEnum enu){
			switch(enu){
				case FishEnum.StandardFish:	return maxAmount_standardFish;
				default: 			throw new System.NullReferenceException();
			}
		}
	}
}