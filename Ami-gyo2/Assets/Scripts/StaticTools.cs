using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticTools{
	
	//外積を用いて2つのベクトル間の角度を0～360の値で返す
	public static float GetAngleFromVector(Vector3 to, Vector3 from){
		float sign = Mathf.Sign(Vector3.Cross(from, to).y);
		float rad = Vector3.Angle(from, to)*Mathf.Deg2Rad;

		//cross > 1となるとき、外積の回る向きとradの角度の正の方向は同じ
		if(sign == -1)
			rad = 2*Mathf.PI - rad;		//angleでは、2つのベクトル間の小さいほうの角度しかとれないので、360度にスケーリングする
	
		return rad;
	}

	public static float Gaussian(float mu, float sigma){
		float z = Mathf.Sqrt(-2f * Mathf.Log(Random.Range(0f, 1f))) * Mathf.Sin(2f * Mathf.PI * Random.Range(0f, 1f));
		return mu + sigma * z;
	}

}
