using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(SetUpTile))]
public class EditorSetUpTile : Editor
{
	public int buttonWidth = 100;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		SetUpTile createTileSetScript = (SetUpTile)target;

		GUILayout.Label("Move Tile:");
		GUILayout.BeginHorizontal("box");
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Up", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.TranslateTile(0, 1, 0);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal("box");
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Left", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.TranslateTile(-1, 0, 0);
		}
		if (GUILayout.Button("Right", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.TranslateTile(1, 0, 0);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		if (GUILayout.Button("Forward", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.TranslateTile(0, 0, -1);
		}
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Down", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.TranslateTile(0, -1, 0);
		}
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Backward", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.TranslateTile(0, 0, 1);
		}
		GUILayout.EndHorizontal();

		GUILayout.Label("Orient Tile:");
		GUILayout.BeginHorizontal("box");
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Up", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.OrientTile(Vector3.down);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal("box");
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Left", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.OrientTile(Vector3.right);
		}
		if (GUILayout.Button("Right", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.OrientTile(Vector3.left);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal("box");
		if (GUILayout.Button("Forward", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.OrientTile(Vector3.forward);
		}
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Down", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.OrientTile(Vector3.up);
		}
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Backward", GUILayout.Width(buttonWidth)))
		{
			createTileSetScript.OrientTile(Vector3.back);
		}
		GUILayout.EndHorizontal();
	}
}
