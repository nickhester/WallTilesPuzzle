using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TileObject_PressurePlate : TileObject
{
	public TileObject activationTarget;

	public override void OnObjectMoveOn(TileObject _tileObjectMovingOn, Direction _movingDirection)
	{
		activationTarget.Activate(true);
	}

	public override void OnObjectMoveOff(TileObject _tileObjectMovingOff, Direction _movingDirection)
	{
		activationTarget.Activate(false);
	}
}
