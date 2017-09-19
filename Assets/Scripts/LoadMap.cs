using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour {
	
	void Start () {;
		LoadMaze("maze3", 360, 100, -50, 0);
	}

	void Update () {

	}

	// make sure the image settings under Advanced are
	// 	Read/Write: 		Yes
	// 	Non Power of 2: 	None
	// Also make sure that the image is placed in the Resources-folder
	// Height and width is the dimentions of the image.
	void LoadMaze(string path, int length, int width, int offsetX, int offsetZ) {
		
		Texture2D texture = Resources.Load(path, typeof(Texture2D)) as Texture2D;

		const int R = 97;
		const float angleStep = 360/360; // just to clarify that the input image is expected to be of length 360

		float x, y, z, theta = 0f;

		bool flatMaze = false;

		GameObject wallElement;
		Vector3 position;

		for (int p1 = 0; p1 < width; p1++) {
			theta = 0f;
			for (int p2 = 0; p2 < length; p2++) {
				bool isWall = ((int)Mathf.Round(texture.GetPixel(p1, p2).b)) == 0;
				if (isWall) {

					x = p1 + offsetX;

					if (flatMaze) {
						y = 0;
						z = p2;
					} else {
						z = R * Mathf.Cos (theta);
						y = R * Mathf.Sin (theta);
					}

					position = new Vector3(x, y, z);
					wallElement = CreateWall(position);

					if (!flatMaze) {
						RotateWall(wallElement);
					}
				}
				theta += angleStep;
			}
		}

	}

	void RotateWall(GameObject go) {
		Vector3 origin = new Vector3 (0, 0, 0);

		Vector3 normal = go.transform.position - origin;
		Vector3 planeNormal = new Vector3 (1, 0, 0);

		normal = Vector3.ProjectOnPlane (normal, planeNormal); // remove any x cord
		go.transform.rotation = Quaternion.FromToRotation(go.transform.up, normal) * go.transform.rotation;
	}

	GameObject CreateWall(Vector3 position) {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = position;
		cube.transform.localScale = new Vector3(1, 6, 1);
		return cube;
	}

	GameObject CreateWall(Vector3 position, Vector3 scale) {
		GameObject cube = CreateWall(position);
		cube.transform.localScale = scale;
		return cube;
	}
}
