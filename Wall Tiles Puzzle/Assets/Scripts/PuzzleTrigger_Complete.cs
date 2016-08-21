using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PuzzleTrigger_Complete : PuzzleTrigger
{
	public Triggerable triggerTarget;

	public override void ReceivePlayerActivate()
	{
		triggerTarget.Trigger();
	}
}
