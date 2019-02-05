using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconCreator : MonoBehaviour {

	public GameObject waterTank;
	public GameObject WeightIcon_prefab;

	public void CreateWeightIcon(Vector2 position, int score){
		var icon = Instantiate(WeightIcon_prefab, position, Quaternion.identity);
		icon.GetComponent<Icon>().Initialize(waterTank, score);
		icon.transform.SetParent(this.gameObject.transform);
		icon.transform.localScale = Vector3.one;
	}
}
