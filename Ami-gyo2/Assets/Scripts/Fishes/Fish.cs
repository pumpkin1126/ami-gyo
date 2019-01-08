using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Amigyo{
	namespace Fishes{
		[RequireComponent(typeof(Rigidbody))]
		public class Fish : MonoBehaviour {
			
			[SerializeField] public FishInfo info;
			public float Speed;
			public float Scale = 1;
			
			int id;
			public int Id{ get{return id;}}

			Action<int> decreaseFishCount;
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

			public void SetIdAndDieMethod(int id, Action<int> method){
				this.id = id;
				decreaseFishCount = method;
			}

			void Die(){
				decreaseFishCount(id);
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

		[Serializable]
		public class FishInfo{
			[SerializeField] int weight;
			[SerializeField] int bonusSecond;
			[SerializeField] EventType eventName;
			[SerializeField] int maxAmount;
			[SerializeField] bool isBig = false, isGroup = false;

			public int Weight{ get{return weight;} }
			public int BonusSecond{ get{return bonusSecond;} }
			public EventType EventName{ get{return eventName;} }
			public int MaxAmount{get{return maxAmount;}}
			public bool IsBig{get{return isBig;}}
			public bool IsGroup{get{return isGroup;}}
		}

	}
}
