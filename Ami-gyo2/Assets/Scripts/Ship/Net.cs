using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour {

	bool isFirst = true; //1度だけ加速したいのでbool型の変数を使いたい

	//shooterによって生成された後、1度だけ加速する
	void FixedUpdate(){
		if(isFirst == true){
			isFirst = false;
			Rigidbody rb = this.GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * 5, ForceMode.Impulse);
		}
	}
}
