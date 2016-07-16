using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileObject_LaserB : TileObject_Crate
{
	void Update()
	{
		// detect player
		GameObject intersectingGameObject = CheckAcrossForObject();
		if (intersectingGameObject != null && intersectingGameObject.tag == "Player")
		{
			GetComponentInParent<PuzzleManager>().CancelPuzzle();

			// TODO: trigger buzz pop sizzle effects
		}
		else
		{
			List<TileObject> tileObjectsAcross = GetTileObjectsAcross();
			if (tileObjectsAcross != null)
			{
				foreach (TileObject tiles in tileObjectsAcross)
				{
					tiles.GetHitByEffect(EffectType.LaserB);
				}
			}
		}

		// TODO: also, destroy the tile player
	}
}
