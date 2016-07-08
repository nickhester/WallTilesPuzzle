using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TileBase : MonoBehaviour
{
	public TileBase neighborUp;
	public TileBase neighborDown;
	public TileBase neighborLeft;
	public TileBase neighborRight;

	public List<TileBase> TilesOnMe = new List<TileBase>();

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}
}
