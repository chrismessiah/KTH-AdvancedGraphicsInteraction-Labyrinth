using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MazeGenerator))]
public class CreateMazeButton : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		MazeGenerator mg = (MazeGenerator)target;
		if(GUILayout.Button("Create Maze Map"))
		{
			mg.generate_maze();
		}
	}
}
