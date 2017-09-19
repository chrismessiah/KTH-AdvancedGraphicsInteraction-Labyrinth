using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour {

	void Start () {;
		LoadMaze("maze", 300, 100, 0, -300);
	}

	void Update () {

	}

	// make sure the image settings under Advanced are
	// 	Read/Write: 		Yes
	// 	Non Power of 2: 	None
	// Also make sure that the image is placed in the Resources-folder
	void LoadMaze(string path, int height, int width, int offsetX, int offsetZ) {
		Texture2D texture = Resources.Load(path, typeof(Texture2D)) as Texture2D;
		for (int x = 0; x < width; x++) {
			for (int z = 0; z < height; z++) {
				int num = (int)Mathf.Round(texture.GetPixel(x, z).b);
				if (num == 0) {
					Vector3 position = new Vector3(x+offsetX, 3, z+offsetZ);
					CreateWall(position);
				}
			}
		}

	}

	GameObject CreateWall(Vector3 position) {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = position;
		cube.transform.localScale = new Vector3(1, 6, 1);
		return cube;
	}

	GameObject CreateWall(Vector3 position, Vector3 scale) {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = position;
		cube.transform.localScale = scale;
		return cube;
	}

	GameObject CreateWall(Vector3 position, Vector3 scale, Quaternion rotation) {
		GameObject cube = CreateWall(position, scale);
		cube.transform.rotation = rotation;
		return cube;
	}
}
