﻿using UnityEngine;
using System.Collections;

public class TileObject_Player : TileObject
{
	
	void Update()
	{
		bool isReceivingDirectionalInput = false;
		Direction inputDirection = Direction.UP;
		if (Input.GetButtonDown("TileRight"))
		{
			inputDirection = Direction.RIGHT;
			isReceivingDirectionalInput = true;
		}
		else if (Input.GetButtonDown("TileLeft"))
		{
			inputDirection = Direction.LEFT;
			isReceivingDirectionalInput = true;
		}
		if (Input.GetButtonDown("TileUp"))
		{
			inputDirection = Direction.UP;
			isReceivingDirectionalInput = true;
		}
		else if (Input.GetButtonDown("TileDown"))
		{
			inputDirection = Direction.DOWN;
			isReceivingDirectionalInput = true;
		}

		if (isReceivingDirectionalInput)
		{
			MoveObject((Direction)(((int)inputDirection + ccwRotation) % 4));
		}
	}

	public override bool ObjectAttemptsToMoveOnToMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		return false;
	}
}