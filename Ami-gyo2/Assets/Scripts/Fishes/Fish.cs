﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Amigyo.Spawners;
using System.Linq;
using UniRx;
using UniRx.Triggers;

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
			List<IFishBehavior> behaveList;
			float currentSpeed;

			void Start () {
				rigid = GetComponent<Rigidbody>();
				transform.localScale = Vector3.one * Scale;
				InitializeSpeed();
				
				//配列をListに変換してるだけ
				behaveList = GetComponents<IFishBehavior>().ToList();

                //  毎フレームbehaviorListから取得した速度ベクトルを返すストリーム。
                var behaviorVelocityStream = this.FixedUpdateAsObservable().Select(_ => this.GetVelocity()).Share();

                //  behaviorListから取得した速度を魚に適用する。
                behaviorVelocityStream.Subscribe(velocity => this.rigid.velocity = velocity * this.currentSpeed).AddTo(this);

                //  4フレーム前の地点からの変位ベクトルを魚の向きとする。
                this.FixedUpdateAsObservable()
                    .Select(_ => this.transform.position)
                    .Buffer(4, 1)
                    .Select(positions => positions.Last() - positions.First())
                    .Select(delta => Quaternion.LookRotation(delta.normalized))
                    .Subscribe(rot => this.transform.rotation = rot)
                    .AddTo(this);
            }

			public void SetIdAndDieMethod(int id, Action<int> method){
				this.id = id;
				decreaseFishCount = method;
			}

			Vector3 GetVelocity(){

				Vector3 velocity = Vector3.zero;
				foreach(var behaveScript in behaveList){
					velocity += behaveScript.GetVelocity(rigid.velocity);
				}

				velocity = velocity.normalized;
				return velocity;
			}

			void Die(){
				var groupScript = GetComponent<Group>();
				
				if(groupScript != null){
					groupScript.Die(decreaseFishCount, id);		//群れの魚の場合、処理が異なるので、委譲
				}else{
					decreaseFishCount(id);
				}

				Destroy(this.gameObject);
			}

			public void InitializeSpeed(){
				currentSpeed = Speed;
			}

			public void MultipleSpeed(float magnif){
				InitializeSpeed();
				currentSpeed *= magnif;
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
			[SerializeField] bool isBig = false;

			public int Weight{ get{return weight;} }
			public int BonusSecond{ get{return bonusSecond;} }
			public EventType EventName{ get{return eventName;} }
			public int MaxAmount{get{return maxAmount;}}
			public bool IsBig{get{return isBig;}}
		}

	}
}
