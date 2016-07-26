using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PuzzleTrigger_Complete : PuzzleTrigger
{
	public Device deviceToTrigger;

	public override void ReceivePlayerActivate()
	{
		deviceToTrigger.Trigger();
	}
}
