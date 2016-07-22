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
		if (acrossHitObject.transform != null && acrossHitObject.transform.tag == "Player")
		{
			GetComponentInParent<PuzzleManager>().CancelPuzzle();
		}
	}
}
