using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PuzzleTrigger_Start : PuzzleTrigger
{
	public string myPuzzleSceneName;
	public PuzzleTrigger puzzleTriggerTarget;
	
	public override void ReceivePlayerActivate()
	{
		if (!puzzleIsActive)
		{
			SceneManager.LoadScene(myPuzzleSceneName, LoadSceneMode.Additive);
			puzzleIsActive = true;
		}
		else
		{
			Destroy(myPuzzleInstance);
			myPuzzleInstance = null;
			puzzleIsActive = false;
		}
	}

	public void ReceivePuzzleComplete()
	{
		// end puzzle
		Destroy(myPuzzleInstance);
		myPuzzleInstance = null;

		// activate next trigger
		if (puzzleTriggerTarget != null)
		{
			puzzleTriggerTarget.Enable(true);
		}
	}

	public void ReceivePuzzleCancel()
	{
		Destroy(myPuzzleInstance);
		myPuzzleInstance = null;
	}

	public void PuzzleReportIn(string _sceneName)
	{
		if (_sceneName == myPuzzleSceneName)
		{
			Scene s = SceneManager.GetSceneByName(myPuzzleSceneName);
			myPuzzleInstance = s.GetRootGameObjects()[0];
			SceneManager.MoveGameObjectToScene(myPuzzleInstance, gameObject.scene);
			SceneManager.UnloadScene(myPuzzleSceneName);

			myPuzzleInstance.transform.SetParent(transform);
		}
	}
}
