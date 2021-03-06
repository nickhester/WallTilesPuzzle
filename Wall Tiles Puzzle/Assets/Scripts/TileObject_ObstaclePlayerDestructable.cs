﻿using UnityEngine;
using System.Collections;

public class TileObject_ObstaclePlayerDestructable : TileObject
{
	public override bool CheckIfObjectCanMoveOnMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		return false;
	}

	public override void GetHitByEffect(EffectType _effectType)
	{
		if (_effectType == EffectType.LaserB || _effectType == EffectType.LaserA)
		{
			DestroyTile();
		}
	}

	public override void GetHitByPlayer()
	{
		DestroyTile();
	}
}
