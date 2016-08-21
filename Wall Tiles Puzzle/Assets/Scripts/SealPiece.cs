using UnityEngine;
using System.Collections;

public class SealPiece : Triggerable
{
	private bool isTriggered = false;
	private Seal m_seal;

	public override void Trigger()
	{
		isTriggered = true;
		GetComponent<Renderer>().enabled = false;
		m_seal.CheckSealedStatus();
	}

	public override bool GetIsTriggered()
	{
		return isTriggered;
	}

	public void SetSealPiece(Seal s)
	{
		m_seal = s;
	}
}
