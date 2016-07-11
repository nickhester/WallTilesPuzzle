using UnityEngine;
using System.Collections;

public class PuzzleTrigger : MonoBehaviour
{
	public GameObject myPuzzlePrefab;
	private GameObject myPuzzleInstance;
	private bool puzzleIsActive = false;
	
	public void ReceivePlayerActivate()
	{
		if (!puzzleIsActive)
		{
			Transform puzzleBaseTransform = transform.GetChild(0);
			myPuzzleInstance = Instantiate(myPuzzlePrefab, puzzleBaseTransform.position, Quaternion.identity) as GameObject;
			myPuzzleInstance.transform.SetParent(puzzleBaseTransform);
			myPuzzleInstance.transform.localRotation = Quaternion.identity;
			myPuzzleInstance.transform.localScale = Vector3.one;

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
}
