using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Seal : Triggerable
{
	public List<SealPiece> m_sealPieces = new List<SealPiece>();
	private bool isUnsealed = false;
	public Triggerable triggerTarget;

	void Start()
	{
		foreach (SealPiece sealPiece in m_sealPieces)
		{
			sealPiece.SetSealPiece(this);
		}
	}

	public void CheckSealedStatus()
	{
		foreach (SealPiece sealPiece in m_sealPieces)
		{
			if (!sealPiece.GetIsTriggered())
			{
				return;
			}
		}

		// if didn't return above, then unseal
		Trigger();
	}

	public override void Trigger()
	{
		if (!isUnsealed)
		{
			isUnsealed = true;
			triggerTarget.Trigger();
		}
	}

	public override bool GetIsTriggered()
	{
		return isUnsealed;
	}
}
