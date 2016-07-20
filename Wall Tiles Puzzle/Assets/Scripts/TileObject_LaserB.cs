using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileObject_LaserB : TileObject_Laser
{
	void Start()
	{
		effectType = EffectType.LaserB;

		base.Start();
	}

	void Update()
	{
		base.Update();

		// detect player
		GameObject intersectingGameObject = CheckAcrossForObject();
		if (intersectingGameObject != null && intersectingGameObject.tag == "Player")
		{
			GetComponentInParent<PuzzleManager>().CancelPuzzle();

			// TODO: trigger buzz pop sizzle effects
		}
	}
}
