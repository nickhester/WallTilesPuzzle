using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

				TileBase tb = hit.transform.gameObject.GetComponent<TileBase>();
				if (tb != null)
				{
					List<TileObject> tileObjects = tb.GetAllOccupyingTileObjects();
					for (int i = 0; i < tileObjects.Count; i++)
					{
						tileObjects[i].GetHitByPlayer();
					}
				}
			}
		}
	}
}
