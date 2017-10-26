using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour {
	public GameObject wallElementTemplate;
	Texture2D mazeMap;

	void Awake () {;
		string path = "maps/maze-999";
		//string path = "maps/generated_maze";

		mazeMap = Resources.Load(path, typeof(Texture2D)) as Texture2D;

		/* Input parameters
		 * 	  1st - map length in pixels, must be less or equal to 360
		 * 	  2nd - map width in pixels, must be less or equal to 100
		 */
		LoadMaze(220, 37, -50, 0); // maze-999 has dimensions  
		//LoadMaze(220, 40, -50, 0);
	}

	// walls are black in the images
	bool isWall(Color color) {
		int blue = ((int)Mathf.Round(color.b));
		int red = ((int)Mathf.Round(color.r));
		int green = ((int)Mathf.Round(color.g));
		if (blue == 0 && red == 0 && green == 0) {
			return true;
		}
		return false;
	}

	// player is blue in the image
	bool isPlayer(Color color) {
		int blue = ((int)Mathf.Round(color.b));
		int red = ((int)Mathf.Round(color.r));
		int green = ((int)Mathf.Round(color.g));
		if (blue == 1 && red == 0 && green == 0) {
			return true;
		}
		return false;
	}

	/* 
	 * Make sure the image settings under Advanced are
	 * 	  Read/Write: 		Yes
	 * 	  Non Power of 2: 	None
	 * 
	 * Also make sure that the image is placed in the Resources-folder
	 * Height and width is the dimentions of the image.
	 */

	void LoadMaze(int length, int width, int offsetX, int offsetZ) {

		const int R = 99;
		float angleStep = 360f/length; // just to clarify that the input image is expected to be of length 360
		float wallElementDepth = angleStep;
		float wallElementWidth = 100f/width; // divide cylinder thickness with map pixel width;
		
		float x, y, z, theta = 0f, thetaRadians;

		bool flatMaze = false;

		GameObject wallElement;
		Vector3 position;
		Vector3 scale = new Vector3 (wallElementDepth*2.5f, wallElementWidth, 2); // depth, width, height

		for (int p1 = 0; p1 < length; p1++) { // loop over long edge
			for (int p2 = 0; p2 < width; p2++) { // loop over short edge

				Color pixel = mazeMap.GetPixel(p2, p1);

				if (isWall (pixel)) {

					x = p2 * wallElementWidth + offsetX;

					if (flatMaze) {
						y = 0;
						z = p2;
					} else {
						thetaRadians = Mathf.Deg2Rad * theta;
						z = R * Mathf.Cos (thetaRadians);
						y = R * Mathf.Sin (thetaRadians);
					}

					position = new Vector3 (x, y, z);
					wallElement = CreateWall (position, scale);

					if (!flatMaze) {
						RotateWall (wallElement);
					}
				} else if (isPlayer (pixel)) {
					x = p2 * wallElementWidth + offsetX;
					thetaRadians = Mathf.Deg2Rad * theta;
					z = R * Mathf.Cos (thetaRadians);
					y = R * Mathf.Sin (thetaRadians);
					position = new Vector3 (x, y, z);
					GameObject controller;
					controller = GameObject.Find("MouseKeyboardPlayer");
					if (controller) {
						controller.transform.position = position;
					}
					controller = GameObject.Find("VRPlayerController");
					if (controller) {
						controller.transform.position = position;
					}
				}
					
			}
			theta += angleStep;
		}

	}

	void RotateWall(GameObject go) {
		Vector3 origin = new Vector3 (0, 0, 0);

		Vector3 normal = go.transform.position - origin;
		Vector3 planeNormal = new Vector3 (1, 0, 0);

		normal = Vector3.ProjectOnPlane (normal, planeNormal); // remove any x cord
		go.transform.rotation = Quaternion.FromToRotation(go.transform.up, normal) * go.transform.rotation;
		go.transform.Rotate(transform.rotation.x+90,transform.rotation.y,transform.rotation.z+90);
	}

	GameObject CreateWall(Vector3 position) {
		GameObject wall = Instantiate(wallElementTemplate) as GameObject;
		//GetComponent<MeshRenderer>().material = new Material(shader);
		wall.transform.position = position;
		wall.transform.localScale = new Vector3(1, 1, 2);
		return wall;
	}

	GameObject CreateWall(Vector3 position, Vector3 scale) {
		GameObject cube = CreateWall(position);
		cube.transform.localScale = scale;
		return cube;
	}
}
