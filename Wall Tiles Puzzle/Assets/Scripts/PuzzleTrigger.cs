using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PuzzleTrigger : MonoBehaviour
{
	public string myPuzzleSceneName;
	private GameObject myPuzzleInstance = null;
	private bool puzzleIsActive = false;
	
	public void ReceivePlayerActivate()
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
