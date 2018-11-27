using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ship{
public class Shooter : MonoBehaviour {

	public GameObject NetPrefab; //= (GamceObject)Assets.Load("Prefabs/Net");
	
	void Update () {
		//ボタンが押されたらNetを生成する
		if(Input.GetKeyDown(KeyCode.Space)){
			Instantiate(NetPrefab, this.transform.position, transform.rotation);
		}
		//網を発射する方向を示す（Ray？）
		//ダイアルで発射する方向を変更する
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.Rotate(0, 1, 0);
		}
		else if(Input.GetKey(KeyCode.LeftArrow)){
			transform.Rotate(0, -1, 0);
		}
	}
}
}
