using UnityEngine;
using System.Collections;

public class TileObject_ObstacleDestructible : TileObject
{
	public override bool ObjectAttemptsToMoveOnToMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		return false;
	}

	public override void GetHitByEffect(EffectType _effectType)
	{
		if (_effectType == EffectType.LaserB)
		{
			Destroy(gameObject);
		}
	}
}
