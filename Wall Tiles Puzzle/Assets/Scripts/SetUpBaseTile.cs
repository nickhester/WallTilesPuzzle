using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SetUpBaseTile : MonoBehaviour
{
	public List<GameObject> TilePrefabs = new List<GameObject>();

	public void CreateTileContent(int tileIndex)
	{
		GameObject go = PrefabUtility.InstantiatePrefab(TilePrefabs[tileIndex], gameObject.scene) as GameObject;
		go.transform.SetParent(transform.parent);
		go.transform.localPosition = transform.localPosition;
		go.transform.localRotation = transform.localRotation;
		go.transform.localScale = transform.localScale;
	}
}
