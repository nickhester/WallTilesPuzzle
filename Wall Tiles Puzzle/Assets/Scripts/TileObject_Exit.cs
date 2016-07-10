using UnityEngine;
using System.Collections;

public class TileObject_Exit : TileObject
{
	public override bool CanObjectMoveOntoMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		return true;
	}
}
