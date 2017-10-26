using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour {
	public GameController gameController {
		get {return GameController.instance;}
	}

	void onTriggerEnter(Collider other){
		if(other.transform.gameObject.tag == "Player"){
			print("end");
			gameController.onFinishMap();
		}
	}
}
