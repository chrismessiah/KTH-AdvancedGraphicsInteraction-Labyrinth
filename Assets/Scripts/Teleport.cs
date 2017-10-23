using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
	
	public GameObject destination;
	public bool LerpTeleport;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			if(LerpTeleport){
				other.GetComponent<PlayerTeleport>().lerpTeleport(destination);
			} else {
				other.GetComponent<PlayerTeleport>().instantTeleport(destination);
			}
			
		}
	}
}
