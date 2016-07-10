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

	//public List<TileBase> TilesOnMe = new List<TileBase>();	// not using this now

	public List<TileObject> GetAllOccupyingTileObjects()
	{
		List<TileObject> retList = new List<TileObject>();
		foreach (TileObject _tileObject in transform.GetComponentsInChildren<TileObject>())
		{
			retList.Add(_tileObject);
		}
		return retList;
	}

	public TileObject.Direction WhichDirectionIsNeighbor(TileBase _tileBase)
	{
		if (_tileBase == neighborUp)
		{
			return TileObject.Direction.UP;
		}
		else if (_tileBase == neighborRight)
		{
			return TileObject.Direction.RIGHT;
		}
		else if (_tileBase == neighborDown)
		{
			return TileObject.Direction.DOWN;
		}
		else if (_tileBase == neighborLeft)
		{
			return TileObject.Direction.LEFT;
		}
		Debug.LogError("Attempted to find neighbor that didn't exist (TileBase:WhichDirectionIsNeighbor)");
		return TileObject.Direction.UP;
	}
}
