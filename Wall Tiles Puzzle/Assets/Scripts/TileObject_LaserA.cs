using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileObject_LaserA : TileObject_Laser
{
	void Start()
	{
		effectType = EffectType.LaserA;

		base.Start();
	}
}
