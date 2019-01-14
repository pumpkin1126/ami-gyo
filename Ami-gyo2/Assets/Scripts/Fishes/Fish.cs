using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Amigyo.Spawners;

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
			Vector3 AreaCenterPos;
			float a, b, c;		//楕円の方程式準拠の名前

			void Start () {
				rigid = GetComponent<Rigidbody>();
				transform.localScale = Vector3.one * Scale;
				
				var Area = GameManager.Instance.GetComponent<SpawnerHolder>().Area;
				AreaCenterPos = Area.transform.position;
				var extents = Area.GetComponent<BoxCollider>().bounds.extents;
				a = extents.x*3/4f;
				b = a*1/2f;
				c = Mathf.Sqrt(a*a - b*b);
			}
			
			void FixedUpdate(){
				Vector3 velocity = GetVelocity();
				transform.rotation = Quaternion.LookRotation(velocity);
				//rigid.AddForce(GetVelocity()*Speed*Time.deltaTime, ForceMode.VelocityChange);
				rigid.velocity = GetVelocity()*Speed;
			}

			void Update () {
				
			}

			public void SetIdAndDieMethod(int id, Action<int> method){
				this.id = id;
				decreaseFishCount = method;
			}

			Vector3 GetVelocity(){
				
				float sign = Mathf.Sign(Vector3.Cross(Vector3.right, transform.position - AreaCenterPos).y);
				float t = Vector3.Angle(Vector3.right, transform.position - AreaCenterPos)*Mathf.Deg2Rad;
				if(sign == 1)	t = 2*Mathf.PI - t;		//angleでは、2つのベクトル間の小さいほうの角度しかとれないので、360度にスケーリングする
				
				Vector3 v = new Vector3(-a*Mathf.Sin(t), 0, b*Mathf.Cos(t));
				float a_doubled = Vector3.Distance(transform.position, AreaCenterPos + Vector3.right*c) + Vector3.Distance(transform.position, AreaCenterPos + Vector3.right*(-c));
				
				float a_dist = Mathf.Abs(2*a - a_doubled);
				Vector3 addV = (AreaCenterPos - transform.position).normalized * (v.magnitude*0.4f*a_dist);
				if(a_doubled > 2*a)		v += addV;
				else					v -= addV;
				v = v.normalized;

				return v;
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
//					Die();
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
