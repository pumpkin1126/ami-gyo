using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Scriptable/GameParams", fileName = "GameParams" )]
public class GameParams : ScriptableObject{

	public int TimeLimit{ get{return timeLimit;}}

	[SerializeField]
	int timeLimit;
}
