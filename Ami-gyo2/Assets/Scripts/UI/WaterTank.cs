using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col2D){

		//スコア追加
		//音再生する
		Destroy(col2D.gameObject);

	}
}
