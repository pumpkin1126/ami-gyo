using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amigyo{
	[CreateAssetMenu( menuName = "Scriptable/GameParams", fileName = "GameParams" )]
	public class GameParams : ScriptableObject{

		public int TimeLimit{ get{return timeLimit;}}
		public Vector3 UpperLeft{ get{return upperLeft; }}
		public Vector3 LowerRight{ get{return lowerRight; }}

		[SerializeField] int timeLimit;
		[SerializeField] Vector3 upperLeft;
		[SerializeField] Vector3 lowerRight;
	}
}