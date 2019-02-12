using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class Icon : MonoBehaviour {

	public float Speed;

	public int Score{
		get{
			return score;
		}
	}

	public TextMeshProUGUI IconText;

	int score = -1;

	GameObject waterTank;
	Rigidbody2D rigid;

	float realSpeed;

	public void FixedUpdate(){
		rigid.velocity = (waterTank.transform.position - this.transform.position).normalized * realSpeed;
	}

	public void Initialize(GameObject _waterTank, int _score){
		waterTank = _waterTank;
		score = _score;
		IconText.text = (score + "kg");


		rigid = GetComponent<Rigidbody2D>();

		Observable.TimerFrame(1).Subscribe(_ => {
			var canvas = transform.parent.GetComponent<Canvas>();
			var canvasWidth = canvas.pixelRect.width * canvas.referencePixelsPerUnit;

			realSpeed = Speed * canvasWidth / 10000;
		}).AddTo(this);
	}
}
