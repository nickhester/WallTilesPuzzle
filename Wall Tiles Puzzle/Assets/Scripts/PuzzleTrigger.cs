using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public abstract class PuzzleTrigger : Triggerable
{
	protected GameObject myPuzzleInstance = null;
	protected bool puzzleIsActive = false;
	public bool isEnabledAtStart = false;
	private bool isEnabled = false;

	void Start()
	{
		if (!isEnabledAtStart)
		{
			Enable(false);
		}
	}

	public abstract void ReceivePlayerActivate();

	public override void Trigger()
	{
		Enable(true);
	}

	public void Enable(bool _enable)
	{
		isEnabled = _enable;
		gameObject.SetActive(_enable);
	}
}
