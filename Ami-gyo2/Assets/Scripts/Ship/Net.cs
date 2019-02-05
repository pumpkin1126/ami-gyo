using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo;
using Amigyo.Fishes;

public class Net : MonoBehaviour {

	bool isFirst = true; //1度だけ加速したいのでbool型の変数を使いたい
	public float Speed = 5;

	//shooterによって生成された後、1度だけ加速する
	void FixedUpdate(){
		if(isFirst == true){
			isFirst = false;
			Rigidbody rb = this.GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * Speed, ForceMode.Impulse);
		}
	}

	void OnCollisionEnter(Collision collision){
		var Script = collision.gameObject.GetComponent<Fish>();
		if(Script == null)
			return;

		GameManager.Instance.CalculateScore(Script.info, collision.contacts[0].point);
		Destroy(this.gameObject);
	}

	void OnTriggerExit(Collider c){
		if(c.gameObject.tag == "Area")	Destroy(this.gameObject);
	}
}
