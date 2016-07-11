using UnityEngine;
using System.Collections;

public class PuzzleTrigger : MonoBehaviour
{
	public PuzzleManager myPuzzle;
	
	public void ReceivePlayerActivate()
	{
		myPuzzle.SetPuzzleActive(true);
	}
}
