using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour {

	void Start () {;
		CreateWall();
	}

	void Update () {

	}

	void CreateWall() {
		// future inputs
		Vector3 position = new Vector3(1,0,-20);
		Vector3 scale = new Vector3(10,2,40);
		//Quaternion rotation = Quaternion.Euler(0, 0, 90); // rotate 90 deg around Z-axis

		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = position;
		cube.transform.localScale = scale;
		//cube.transform.rotation = rotation;
	}
}
