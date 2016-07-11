using UnityEngine;
using System.Collections;

public class TileObject_PlayerStart : TileObject
{
	public override bool ObjectAttemptsToMoveOnToMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		return true;
	}
}
