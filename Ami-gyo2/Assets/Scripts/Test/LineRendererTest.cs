using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTest : MonoBehaviour {

	public LineRenderer renderer;

	// Use this for initialization
	void Start () {
		if(renderer != null){
			renderer.SetPosition(0, this.transform.position);
			renderer.SetPosition(1, this.transform.position + transform.forward*5);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
