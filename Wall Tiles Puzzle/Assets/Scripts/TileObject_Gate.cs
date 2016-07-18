using UnityEngine;
using System.Collections;

public class TileObject_Gate : TileObject
{
	public Texture textureGateClosed;
	public Texture textureGateOpen;
	private bool isGateOpen = false;
	private bool isGateActivated = false;

	public override bool CheckIfObjectCanMoveOnMe(TileObject _tileObject, TileObject.Direction _fromDirection)
	{
		return isGateOpen;
	}

	public override void OnObjectMoveOff(TileObject _tileObjectMovingOff, Direction _movingDirection)
	{
		if (CheckIfShouldCloseGate())
		{
			CloseGate();
		}
	}

	public override void Activate(bool _isActivating)
	{
		isGateActivated = _isActivating;

		if (isGateActivated)
		{
			OpenGate();
		}
		else
		{
			if (CheckIfShouldCloseGate())
			{
				CloseGate();
			}
		}
	}

	private void OpenGate()
	{
		isGateOpen = true;
		GetComponent<Renderer>().material.SetTexture("_MainTex", textureGateOpen);
	}

	private void CloseGate()
	{
		isGateOpen = false;
		GetComponent<Renderer>().material.SetTexture("_MainTex", textureGateClosed);
	}

	private bool CheckIfShouldCloseGate()
	{
		if (isGateOpen && !isGateActivated)
		{
			bool containsOtherObject = false;
			foreach (TileObject tileObject in GetBaseTile().GetAllOccupyingTileObjects())
			{
				if (tileObject != this)
				{
					containsOtherObject = true;
				}
			}

			if (!containsOtherObject)
			{
				return true;
			}
		}
		return false;
	}
}
