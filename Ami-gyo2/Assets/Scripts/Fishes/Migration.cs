﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Spawners;

namespace Amigyo{
	namespace Fishes{
		public class Migration : GroupableScript, IFishBehavior {

			const float DecreasingRate = 4/5f;
			Vector3 AreaCenterPos;
			float a, b, c;		//楕円の方程式準拠の名前

			float firstRad = -1;
			bool isBeforeTurning = true;		//周回する前はtrue
			bool isOutProcess = false;
			bool isLeader = false;

			protected override void Initialize(){
				isLeader = true;

				var Area = GameManager.Instance.GetComponent<SpawnerHolder>().Area;
				var extents = Area.GetComponent<BoxCollider>().bounds.extents;
				AreaCenterPos = Area.transform.position + Vector3.forward * extents.z*(1-DecreasingRate);
				extents *= DecreasingRate;

				a = extents.x*3/4f;
				b = a*1/2f;
				c = Mathf.Sqrt(a*a - b*b);
			}

			public Vector3 GetVelocity(Vector3 currentVelocity){
				if(!isLeader)	return Vector3.zero;
				//周回後は干渉しない
				if(isOutProcess)	return currentVelocity;

				//楕円の円周方向の速度を算出
				var centerToThisObj = (transform.position - AreaCenterPos);
				float t = StaticTools.GetAngleFromVector(Vector3.right, centerToThisObj);

				Vector3 v = new Vector3(-a*Mathf.Sin(t), 0, b*Mathf.Cos(t));

				//楕円の円周上を離れた場合に、もとにもどるようにする
				float a_doubled = Vector3.Distance(transform.position, AreaCenterPos + Vector3.right*c) + Vector3.Distance(transform.position, AreaCenterPos + Vector3.right*(-c));

				float a_dist = Mathf.Abs(2*a - a_doubled);
				Vector3 addV = (AreaCenterPos - transform.position).normalized * (v.magnitude*0.4f*a_dist);
				if(a_doubled > 2*a)		v += addV;
				else{
					v -= addV;
					if(firstRad == -1)	firstRad = t;
				}

				//1周したら外に抜ける
				if(firstRad != -1){
					if(Mathf.Abs(Mathf.DeltaAngle(firstRad, t)) > 10*Mathf.Deg2Rad){
						if(isBeforeTurning)	isBeforeTurning = false;
					}else{
						if(!isBeforeTurning)	isOutProcess = true;
					}
				}

				//正規化して強度をかけて返す
				v = v.normalized * currentIntensity;
				return v;
			}


			//抜けるのは楕円状に周回するのが終わった後なので、特に渡す情報はない
			public override void Die(List<Group> groupScripts, Group nextLeaderScript){
				if(nextLeaderScript != null)
					nextLeaderScript.GetComponent<Migration>().InheritLeader();
			}

			public void InheritLeader(){
				isLeader = true;
				isOutProcess = true;
			}
		}
	}
}