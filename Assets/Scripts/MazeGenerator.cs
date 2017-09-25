using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MazeGenerator : MonoBehaviour{
	
	public int width;
	public int height;
	public bool wrap;

	private int node_width;
	private int node_height;

	public void generate_maze(){

		Texture2D labyrinth_map;
		MazeNode[,] maze;

		// Check conditions
		if (width < 3) {
			Debug.Log ("width need to be larger than 3");
			return;
		}
		if (height < 3) {
			Debug.Log ("height need to be larger than 3");
			return;
		}

		// Node Size
		if (width % 2 == 0)
			node_width = (width / 2) - 1;
		else
			node_width = (width - 1) / 2;

		if (height % 2 == 0) {
			if (wrap) {
				node_height = (height / 2);
			} else {
				node_height = (height / 2) - 1;
			}
		} else {
			if (wrap) {
				node_height = (height + 1) / 2;
			} else {
				node_height = (height - 1) / 2;
			}
		}
		
		// Setup Maze
		maze = new MazeNode[node_width, node_height];

		for (int x = 0; x < node_width; x++) {
			for (int y = 0; y < node_height; y++){
				maze [x, y] = new MazeNode ();
			}
		}

		// Start digging
		//Debug.Log("Digging Maze");
		maze_digger(maze, new int[]{0, 0});

		// Fill texture with result
		labyrinth_map = new Texture2D(width, height);

		int height_iter;
		if (wrap) {
			height_iter = height;
		} else {
			height_iter = height - 1;
		}

		//Debug.Log("Filling up texture");
		for (int y = 0; y < height_iter; y++) {
			for (int x = 0; x < width-1; x++) {
				if ((x % 2 != 0) & (y % 2 != 0)) {
					int node_x = (x - 1) / 2;
					int node_y = (y - 1) / 2;
					MazeNode m = maze [node_x, node_y];

					Color[] walls = new Color[4];
					walls [0] = m.walls [0] ? Color.black : Color.white;
					walls [1] = m.walls [1] ? Color.black : Color.white;
					walls [2] = m.walls [2] ? Color.black : Color.white;
					walls [3] = m.walls [3] ? Color.black : Color.white;

					//Debug.Log("node: ( " + node_x + "," + node_y + " )\n" + "north: " + m.walls[0] + "\nwest: " + m.walls[1] + "\nsouth: " + m.walls[2] + "\neast: " + m.walls[3]);

					// Middle
					labyrinth_map.SetPixel( x, y, Color.white);

					// Corners
					labyrinth_map.SetPixel( x-1, y-1, Color.black);
					labyrinth_map.SetPixel( x+1, y-1, Color.black);
					if (y + 1 < height) {
						labyrinth_map.SetPixel (x - 1, y + 1, Color.black);
						labyrinth_map.SetPixel (x + 1, y + 1, Color.black);

						// Walls
						labyrinth_map.SetPixel (x, y + 1, walls [0]);
					}
					labyrinth_map.SetPixel (x - 1, 	y,	 	walls[1]);
					labyrinth_map.SetPixel (x, 		y - 1, 	walls[2]);
					labyrinth_map.SetPixel (x + 1, 	y,	 	walls[3]);

				}
			}
		}
		// Add Padding
		if(labyrinth_map.GetPixel(width-1, 0) != Color.black){
			for (int y = 0; y < height; y++)
				labyrinth_map.SetPixel (width - 1, y, Color.black);
		}
		if(labyrinth_map.GetPixel(0, height-1) != Color.black){
			for (int x = 0; x < width; x++)
				labyrinth_map.SetPixel (x, height - 1, Color.black);
		}
		// Make the labyrinth wrap at the short edges
		if (wrap) {
			int connections = width / 8;
			for (int x = 0; x < connections; x++) {
				int index = Random.Range (0, node_width) * 2 + 1;
				labyrinth_map.SetPixel (index, 0, Color.white);
			}
		}
			
		string path = Application.dataPath + "/Resources/maps/generated_maze.png";
		Debug.Log ("Saving maze to: " + path);
		System.IO.File.WriteAllBytes (path, labyrinth_map.EncodeToPNG ());
	}

	void maze_digger(MazeNode[,] maze, int[] point){

		MazeNode this_node = maze [point [0], point [1]];
		this_node.visited = true;

		// [north west south east]
		List<int> nearby = new List<int>{0,1,2,3};
		int[] path = new int[2];

		// Check boundaries
		// East
		if (point [0] + 1 >= node_width) {
			nearby.RemoveAt (3);
		}
		// South
		if (point [1] - 1 < 0) {
			nearby.RemoveAt(2);
		}
		// West
		if (point [0] - 1 < 0) {
			nearby.RemoveAt(1);
		}
		// North
		if (point [1] + 1 >= node_height) {
			nearby.RemoveAt(0);
		}

		string temp = "";

		for (int i = 0; i < nearby.Count; i++) {
			temp += nearby [i].ToString() + " ";
		}
		/*
		Debug.Log ("node: (" + point[0] + " " + point[1] + ")");
		Debug.Log ("nearby: " + temp);
		*/
		while (nearby.Count > 0) {
			int index = Random.Range (0, nearby.Count);
			switch (nearby[index]) {
			case 0:
				path [0] = point [0];
				path [1] = point [1]+1;
				break;
			case 1:
				path [0] = point [0]-1;
				path [1] = point [1];
				break;
			case 2:
				path [0] = point [0];
				path [1] = point [1]-1;
				break;
			case 3:
				path [0] = point [0]+1;
				path [1] = point [1];
				break;
			}
			if (!maze [path [0], path [1]].visited) {
				int opposite = ((nearby [index] & 0x2) == 0x2) ? nearby [index] - 0x2 : nearby [index] + 0x2;
				maze [path [0], path [1]].walls [opposite] = false;
				this_node.walls [nearby[index]] = false;
				maze_digger (maze, path);
			}
			nearby.RemoveAt (index);
		}
	}
}

public class MazeNode{
	// Intact Walls
	// [north west south east]
	public bool[] walls;
	public bool visited;
	public MazeNode(){
		visited = false;
		walls = new bool[4];
		for (int i = 0; i < 4; i++) {
			walls [i] = true;
		}
	}
}
	

