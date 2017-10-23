using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour {


	private bool teleporting = false;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float t = 0;
	private float startTime;
	private float length;
	private float speed = 100;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(teleporting){
			if(t >= 1){
				teleporting = false;
			} else {
				float dist = (Time.time - startTime) * speed;
				t = dist / length;
				transform.position = Vector3.Lerp(startPosition, endPosition, t);

			}
		}
	}

	public void lerpTeleport(GameObject destination){
		teleporting = true;
		startPosition = transform.position;
		endPosition = destination.transform.position;
		length = Vector3.Distance(startPosition, endPosition);
		startTime = Time.time;
	}

	public void instantTeleport(GameObject destination){
		transform.position = destination.transform.position;
	}
}
