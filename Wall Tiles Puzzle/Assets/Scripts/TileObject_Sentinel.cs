using UnityEngine;
using System.Collections;

public class TileObject_Sentinel : TileObject
{
	public enum SentinelPattern
	{
		Horizontal,
		Vertical
	}
	public SentinelPattern pattern;
	public float tileMoveInterval = 0.4f;
	private float tileMoveCounter = 0.0f;
	Direction inputDirection;

	void Start()
	{
		if (pattern == SentinelPattern.Horizontal)
		{
			inputDirection = Direction.RIGHT;
		}
		else if (pattern == SentinelPattern.Vertical)
		{
			inputDirection = Direction.DOWN;
		}
	}

	void Update()
	{
		tileMoveCounter += Time.deltaTime;

		if (tileMoveCounter > tileMoveInterval)
		{
			tileMoveCounter = 0.0f;

			if (TryMoveObject((Direction)(((int)inputDirection + ccwRotation) % 4)) == false)
			{
				inputDirection += 2;
				inputDirection = (Direction)(((int)inputDirection) % 4);
			}
		}
	}

	public override bool CheckIfObjectCanMoveOnMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		TileObject_Player _tileObjectAsPlayer = _tileObject as TileObject_Player;
		if (_tileObjectAsPlayer != null)
		{
			_tileObjectAsPlayer.DestroyTile();
		}

		return false;
	}

	public override void GetHitByEffect(EffectType _effectType)
	{
		if (_effectType == EffectType.LaserA || _effectType == EffectType.LaserB)
		{
			DestroyTile();
		}
	}
}
