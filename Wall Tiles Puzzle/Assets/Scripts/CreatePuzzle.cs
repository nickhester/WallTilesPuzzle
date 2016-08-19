using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class CreatePuzzle : MonoBehaviour
{

	public GameObject tileBasePrefab;

	public void CreateNewTile()
	{
		GameObject go = PrefabUtility.InstantiatePrefab(tileBasePrefab, gameObject.scene) as GameObject;
		go.transform.SetParent(transform);
		go.transform.localPosition = Vector3.zero;
		go.transform.rotation = transform.rotation;
		go.transform.localScale = Vector3.one;
	}

	public void ValidatePuzzle()
	{
		List<SetUpBaseTile> setUpBaseTiles = new List<SetUpBaseTile>();
		setUpBaseTiles.AddRange(transform.GetComponentsInChildren<SetUpBaseTile>());

		// make sure no base tiles occupy the same space
		for (int i = 0; i < setUpBaseTiles.Count; i++)
		{
			for (int j = i + 1; j < setUpBaseTiles.Count; j++)
			{
				if(setUpBaseTiles[i].GetComponent<SetUpTile>().indexX == setUpBaseTiles[j].GetComponent<SetUpTile>().indexX
					&& setUpBaseTiles[i].GetComponent<SetUpTile>().indexY == setUpBaseTiles[j].GetComponent<SetUpTile>().indexY
					&& setUpBaseTiles[i].GetComponent<SetUpTile>().indexZ == setUpBaseTiles[j].GetComponent<SetUpTile>().indexZ)
				{
					Debug.LogWarning("2 Base Tiles at same location: " + setUpBaseTiles[i].gameObject.name + " and " + setUpBaseTiles[j].gameObject.name);
				}
			}
		}

		// find neighbors for each tile
		foreach (SetUpBaseTile setUpBaseTile in setUpBaseTiles)
		{
			TileBase tileBase = setUpBaseTile.GetComponent<TileBase>();

			SerializedObject serializedObject = new SerializedObject(tileBase);
			SerializedProperty sp_neighborUp = serializedObject.FindProperty("neighborUp");
			SerializedProperty sp_neighborDown = serializedObject.FindProperty("neighborDown");
			SerializedProperty sp_neighborRight = serializedObject.FindProperty("neighborRight");
			SerializedProperty sp_neighborLeft = serializedObject.FindProperty("neighborLeft");

			// get a clean slate
			sp_neighborUp.objectReferenceValue = null;
			sp_neighborDown.objectReferenceValue = null;
			sp_neighborLeft.objectReferenceValue = null;
			sp_neighborRight.objectReferenceValue = null;

			foreach (SetUpBaseTile otherSetUpBaseTiles in setUpBaseTiles)
			{
				if (setUpBaseTile.gameObject == otherSetUpBaseTiles.gameObject)
				{
					continue;
				}

				float scaleX = setUpBaseTile.transform.lossyScale.x;
				float scaleY = setUpBaseTile.transform.lossyScale.y;
				float scaleZ = setUpBaseTile.transform.lossyScale.z;

				// find neighbors
				Vector3 upPosConcave =		setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleY * setUpBaseTile.transform.up) / 2.0f) +		(transform.InverseTransformVector(scaleZ * setUpBaseTile.transform.forward) / 2.0f);
				Vector3 upPosFlat =			setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleY * setUpBaseTile.transform.up));
				Vector3 upPosConvex =		setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleY * setUpBaseTile.transform.up) / 2.0f) +		(transform.InverseTransformVector(scaleZ * -setUpBaseTile.transform.forward) / 2.0f);
				Vector3 downPosConcave =	setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleY * -setUpBaseTile.transform.up) / 2.0f) +		(transform.InverseTransformVector(scaleZ * setUpBaseTile.transform.forward) / 2.0f);
				Vector3 downPosFlat =		setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleY * -setUpBaseTile.transform.up));
				Vector3 downPosConvex =		setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleY * -setUpBaseTile.transform.up) / 2.0f) +		(transform.InverseTransformVector(scaleZ * -setUpBaseTile.transform.forward) / 2.0f);
				Vector3 leftPosConcave =	setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleX * -setUpBaseTile.transform.right) / 2.0f) +	(transform.InverseTransformVector(scaleZ * setUpBaseTile.transform.forward) / 2.0f);
				Vector3 leftPosFlat =		setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleX * -setUpBaseTile.transform.right));
				Vector3 leftPosConvex =		setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleX * -setUpBaseTile.transform.right) / 2.0f) +	(transform.InverseTransformVector(scaleZ * -setUpBaseTile.transform.forward) / 2.0f);
				Vector3 rightPosConcave =	setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleX * setUpBaseTile.transform.right) / 2.0f) +		(transform.InverseTransformVector(scaleZ * setUpBaseTile.transform.forward) / 2.0f);
				Vector3 rightPosFlat =		setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleX * setUpBaseTile.transform.right));
				Vector3 rightPosConvex =	setUpBaseTile.transform.localPosition + (transform.InverseTransformVector(scaleX * setUpBaseTile.transform.right) / 2.0f) +		(transform.InverseTransformVector(scaleZ * -setUpBaseTile.transform.forward) / 2.0f);

				if (Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, upPosConcave) < Vector3.kEpsilon
					|| Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, upPosFlat) < Vector3.kEpsilon
					|| Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, upPosConvex) < Vector3.kEpsilon)
				{
					Debug.DrawLine(setUpBaseTile.transform.position, otherSetUpBaseTiles.transform.position, Color.green, 5.0f);
					sp_neighborUp.objectReferenceValue = otherSetUpBaseTiles.GetComponent<TileBase>();
				}
				else if (Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, downPosConcave) < Vector3.kEpsilon
					|| Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, downPosFlat) < Vector3.kEpsilon
					|| Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, downPosConvex) < Vector3.kEpsilon)
				{
					Debug.DrawLine(setUpBaseTile.transform.position, otherSetUpBaseTiles.transform.position, Color.green, 5.0f);
					sp_neighborDown.objectReferenceValue = otherSetUpBaseTiles.GetComponent<TileBase>();
				}
				else if (Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, leftPosConcave) < Vector3.kEpsilon
					|| Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, leftPosFlat) < Vector3.kEpsilon
					|| Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, leftPosConvex) < Vector3.kEpsilon)
				{
					Debug.DrawLine(setUpBaseTile.transform.position, otherSetUpBaseTiles.transform.position, Color.green, 5.0f);
					sp_neighborLeft.objectReferenceValue = otherSetUpBaseTiles.GetComponent<TileBase>();
				}
				else if (Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, rightPosConcave) < Vector3.kEpsilon
					|| Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, rightPosFlat) < Vector3.kEpsilon
					|| Vector3.Distance(otherSetUpBaseTiles.transform.localPosition, rightPosConvex) < Vector3.kEpsilon)
				{
					Debug.DrawLine(setUpBaseTile.transform.position, otherSetUpBaseTiles.transform.position, Color.green, 5.0f);
					sp_neighborRight.objectReferenceValue = otherSetUpBaseTiles.GetComponent<TileBase>();
				}
			}
			serializedObject.ApplyModifiedProperties();
		}

		// TODO: validate positions and traversability
	}
}
