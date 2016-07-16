using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float rayMaxDistance;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, rayMaxDistance))
			{
				PuzzleTrigger pt = hit.transform.gameObject.GetComponent<PuzzleTrigger>();
				if (pt != null)
				{
					pt.ReceivePlayerActivate();
				}
			}
		}
	}
}
