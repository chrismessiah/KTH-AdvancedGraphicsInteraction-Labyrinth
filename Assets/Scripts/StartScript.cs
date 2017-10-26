using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour {
	public GameController gameController {
		get {return GameController.instance;}
	}

	void OnTriggerEnter(Collider other){
		if(other.transform.gameObject.tag == "Player"){
			print("startmap");
			gameController.onStartMap();
		}
	}
}
