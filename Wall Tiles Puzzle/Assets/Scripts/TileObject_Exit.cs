using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TileObject_Exit : TileObject
{
	public override bool ObjectAttemptsToMoveOnToMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		TileObject_Player _player = _tileObject as TileObject_Player;
		if (_player != null)
		{
			GetComponentInParent<PuzzleManager>().CompletePuzzle();
			return true;
		}
		return false;
	}
}
