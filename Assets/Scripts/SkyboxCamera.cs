using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCamera : MonoBehaviour {

	public Transform mainCamera {
		get { return transform.parent.transform; }
	}
	private Vector3 pos;
	void Awake(){
		pos = transform.position;
	}
	// Update is called once per frame
	void LateUpdate () {
		//transform.rotation = mainCamera.rotation;		
		transform.position = pos;
	}
}
