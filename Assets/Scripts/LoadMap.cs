using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour {

	void Start () {;
		LoadMaze("maze2", 300, 100, -50, 0);
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

		float theta = 0f;
		int r = 97;
		float x = 0f, y = 0f, z = 0f;

		float angleStep = 360 / length; // assumes that the image file is less than 360 pixels in length

		bool flatMaze = false;

		for (int p1 = 0; p1 < width+1; p1++) {
			for (int p2 = 0; p2 < length; p2++) {
				bool isWall = ((int)Mathf.Round(texture.GetPixel(p1, p2).b)) == 0;
				if (isWall) {
					if (flatMaze) {
						x = p1 + offsetX;
						y = 0;
						z = p2;
						Vector3 position = new Vector3(x, y, z);
						CreateWall (position);
					} else {
						x = p1 + offsetX;
						z = r * Mathf.Cos (theta);
						y = r * Mathf.Sin (theta);
						Vector3 position = new Vector3(x, y, z);
						CreateWall(position, theta);	
					}
				}
				theta += angleStep;
			}
			theta = 0f;
		}

	}

	GameObject CreateWall(Vector3 position) {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = position;
		cube.transform.localScale = new Vector3(1, 6, 1);
		return cube;
	}
		
	GameObject CreateWall(Vector3 position, float angle) {
		GameObject cube = CreateWall(position);
		//cube.transform.RotateAround (Vector3.zero, Vector3.right, angle);
		cube.transform.rotation = Quaternion.Euler(angle, 0, 0);
		return cube;
	}

	GameObject CreateWall(Vector3 position, float angle, Vector3 scale) {
		GameObject cube = CreateWall(position);
		cube.transform.localScale = scale;
		cube.transform.rotation = Quaternion.Euler(0, 0, 0);
		return cube;
	}
}
