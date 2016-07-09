using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TileBase : MonoBehaviour
{
	public TileBase neighborUp;
	public TileBase neighborRight;
	public TileBase neighborDown;
	public TileBase neighborLeft;
	
	public int neighborUpRotation;
	public int neighborRightRotation;
	public int neighborDownRotation;
	public int neighborLeftRotation;

	public List<TileBase> TilesOnMe = new List<TileBase>();

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}
}
