using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(SetUpBaseTile))]
public class EditorSetUpBaseTile : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		SetUpBaseTile createTileSetScript = (SetUpBaseTile)target;

		GUILayout.Label("Add Tile Type:");
		GUILayout.BeginHorizontal("box");
		foreach (TileObject.TileType tileType in Enum.GetValues(typeof(TileObject.TileType)))
		{
			string typeName = Enum.GetName(typeof(TileObject.TileType), tileType);
			if (GUILayout.Button(typeName))
			{
				createTileSetScript.CreateTileContent((int)tileType);
			}
		}
		GUILayout.EndHorizontal();
	}
}
