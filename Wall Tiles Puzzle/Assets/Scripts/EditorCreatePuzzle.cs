using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CreatePuzzle))]
public class EditorCreatePuzzle : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		CreatePuzzle createTileSetScript = (CreatePuzzle)target;
		
		if (GUILayout.Button("Create New Tile"))
		{
			createTileSetScript.CreateNewTile();
		}

		if (GUILayout.Button("Validate Tiles"))
		{
			createTileSetScript.ValidatePuzzle();
		}
	}
}
