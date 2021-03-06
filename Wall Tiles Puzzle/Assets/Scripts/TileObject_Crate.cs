﻿using UnityEngine;
using System.Collections;

public class TileObject_Crate : TileObject
{
	public override bool CheckIfObjectCanMoveOnMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		TileObject_Player _player = _tileObject as TileObject_Player;
		if (_player != null)
		{
			Direction d = (Direction)((((int)_fromDirection) + 2) % 4);
			return TryMoveObject(d);
		}
		return false;
	}
}
