using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    public float moveSpeed;
    public bool controllerMove;

	void Update(){
		if(Input.GetKeyDown("X")){
			if(controllerMove == true){
				controllerMove = false;
			} else {
				controllerMove = true;
			}
		}
	}

}
