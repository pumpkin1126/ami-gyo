using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Amigyo{
	namespace Fishes{
		[RequireComponent(typeof(Rigidbody))]
		public class Fish : MonoBehaviour {
			
			public FishInfo info;
			public float Speed;
			public float Scale = 1;

			Action<FishEnum> decreaseFishCount;
			Rigidbody rigid;

			void Start () {
				rigid = GetComponent<Rigidbody>();
				transform.localScale = Vector3.one * Scale;
			}
			
			void FixedUpdate(){
				rigid.AddForce(transform.forward*Speed*Time.deltaTime);
			}

			void Update () {
				
			}

			public void SetDecreaseCountMethod(Action<FishEnum> method){
				decreaseFishCount = method;
			}

			void Die(){
				decreaseFishCount(FishEnum.StandardFish);
				Destroy(this.gameObject);
			}

			void OnCollisionEnter(Collision c){
				if(c.gameObject.GetComponent<Net>() != null){
					Die();
				}
			}

			void OnTriggerExit(Collider c){
				if(c.gameObject.tag == "Area"){
					Die();
				}
			}
		}

		public enum FishEnum{
			BigFish, GroupFish, StandardFish
		}
	}
}
