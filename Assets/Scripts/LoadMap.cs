using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour {

	void Start () {;
		Vector3 position, scale;

		position = new Vector3(1,5,-20);
		scale = new Vector3(1,10,20);
		CreateWall(position, scale);

		position = new Vector3(1,5,-20);
		scale = new Vector3(20,10,1);
		CreateWall(position, scale);
	}

	void Update () {

	}

	void CreateWall(Vector3 position, Vector3 scale) {
		//Quaternion rotation = Quaternion.Euler(0, 0, 90); // future input, rotate 90 deg around Z-axis

		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = position;
		cube.transform.localScale = scale;
		//cube.transform.rotation = rotation;
	}
}
