using UnityEngine;
using System.Collections;

public class TileObject_Player : TileObject
{
	private bool isReadyForMovementHorizInput = true;
	private bool isReadyForMovementVertInput = true;
	
	void Update()
	{
		bool isReceivingDirectionalInput = false;
		Direction inputDirection = Direction.UP;

		float horizInput = Input.GetAxis("TileHorizontal");
		float vertInput = Input.GetAxis("TileVertical");

		if (horizInput > 0.0f && isReadyForMovementHorizInput)
		{
			inputDirection = Direction.RIGHT;
			isReceivingDirectionalInput = true;
			isReadyForMovementHorizInput = false;
		}
		else if (horizInput < 0.0f && isReadyForMovementHorizInput)
		{
			inputDirection = Direction.LEFT;
			isReceivingDirectionalInput = true;
			isReadyForMovementHorizInput = false;
		}
		else if (horizInput == 0.0f)
		{
			isReadyForMovementHorizInput = true;
		}

		if (vertInput > 0.0f && isReadyForMovementVertInput)
		{
			inputDirection = Direction.UP;
			isReceivingDirectionalInput = true;
			isReadyForMovementVertInput = false;
		}
		else if (vertInput < 0.0f && isReadyForMovementVertInput)
		{
			inputDirection = Direction.DOWN;
			isReceivingDirectionalInput = true;
			isReadyForMovementVertInput = false;
		}
		else if (vertInput == 0.0f)
		{
			isReadyForMovementVertInput = true;
		}

		if (isReceivingDirectionalInput)
		{
			TryMoveObject((Direction)(((int)inputDirection + ccwRotation) % 4));
		}
	}

	public override bool CheckIfObjectCanMoveOnMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		return false;
	}

	public override void GetHitByEffect(EffectType _effectType)
	{
		if (_effectType == EffectType.LaserA || _effectType == EffectType.LaserB)
		{
			GetComponentInParent<PuzzleManager>().CancelPuzzle();
		}
	}
}
