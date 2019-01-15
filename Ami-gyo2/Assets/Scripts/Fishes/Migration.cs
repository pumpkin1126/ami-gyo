using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amigyo.Spawners;

namespace Amigyo{
	namespace Fishes{
		public class Migration : MonoBehaviour, IFishBehavior {

			Vector3 AreaCenterPos;
			float a, b, c;		//楕円の方程式準拠の名前
			void Start () {

				var Area = GameManager.Instance.GetComponent<SpawnerHolder>().Area;
				AreaCenterPos = Area.transform.position;
				var extents = Area.GetComponent<BoxCollider>().bounds.extents;
				a = extents.x*3/4f;
				b = a*1/2f;
				c = Mathf.Sqrt(a*a - b*b);
			}

			public Vector3 GetVelocity(Vector3 currentVelocity){

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
		}
	}
}