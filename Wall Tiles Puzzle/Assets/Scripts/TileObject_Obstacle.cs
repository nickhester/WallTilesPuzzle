using UnityEngine;
using System.Collections;

public class TileObject_Obstacle : TileObject
{
	public override bool CheckIfObjectCanMoveOnMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		return false;
	}
}
