using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using System;

[CustomEditor(typeof(SetUpBaseTile))]
public class EditorSetUpBaseTile : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		SetUpBaseTile createTileSetScript = (SetUpBaseTile)target;

		GUILayout.Label("Add Tile Type:");

		List<TileObject.TileType> tileTypes = new List<TileObject.TileType>();
		foreach (TileObject.TileType tileType in Enum.GetValues(typeof(TileObject.TileType)))
		{
			tileTypes.Add(tileType);
		}

		// display buttons for each tile type
		int numItemsPerRow = 6;
		GUILayout.BeginHorizontal("box");
		for (int i = 0; i < tileTypes.Count; i++)
		{
			bool isNewLine = false;
			if (i % numItemsPerRow == 0)
			{
				isNewLine = true;
			}

			if (i > 0 && isNewLine)
			{
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal("box");
			}

			string typeName = Enum.GetName(typeof(TileObject.TileType), tileTypes[i]);
			if (i != 0 && GUILayout.Button(typeName))
			{
				createTileSetScript.CreateTileContent((int)(tileTypes[i]));
			}
		}
		GUILayout.EndHorizontal();
	}
}
