using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TileObject_Exit : TileObject
{
	public override bool CheckIfObjectCanMoveOnMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		TileObject_Player _player = _tileObject as TileObject_Player;
		if (_player != null)
		{
			return true;
		}
		return false;
	}

	public override void OnObjectMoveOn(TileObject _tileObjectMovingOn, Direction _movingDirection)
	{
		TileObject_Player _player = _tileObjectMovingOn as TileObject_Player;
		if (_player != null)
		{
			GetComponentInParent<PuzzleManager>().CompletePuzzle();
		}
	}
}
