using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileObject_LaserA : TileObject_Crate
{
	void Update()
	{
		List<TileObject> tileObjectsAcross = GetTileObjectsAcross();
		if (tileObjectsAcross != null)
		{
			foreach (TileObject tiles in tileObjectsAcross)
			{
				tiles.GetHitByEffect(EffectType.LaserA);
			}
		}

		// TODO: also, destroy the tile player
	}
}
