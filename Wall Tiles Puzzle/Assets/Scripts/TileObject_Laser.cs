using UnityEngine;
using System.Collections;

public class TileObject_Laser : TileObject_Crate
{
	public GameObject laserEffectPrefab;
	protected GameObject laserEffectInstance;
	protected EffectType effectType;

	protected void Start()
	{
		laserEffectInstance = Instantiate(laserEffectPrefab, transform.position, Quaternion.identity) as GameObject;
		laserEffectInstance.transform.right = -transform.forward;
		laserEffectInstance.transform.SetParent(transform);
		laserEffectInstance.transform.localScale = Vector3.one;
	}

	protected void Update()
	{
		TileSetInfo tileSetAcross = GetTileObjectsAcross();

		if (tileSetAcross.tileBase != null)
		{
			SetLaserPosition(tileSetAcross.tileBase.transform.position);
		}
		else
		{
			SetLaserPosition();
		}

		if (tileSetAcross.tileObjects != null)
		{
			foreach (TileObject tiles in tileSetAcross.tileObjects)
			{
				tiles.GetHitByEffect(effectType);
			}
		}
	}

	protected void SetLaserPosition(Vector3 _targetPosition)
	{
		float distanceBetween = Vector3.Distance(_targetPosition, transform.position);

		ParticleSystem particleSystem = laserEffectInstance.GetComponent<ParticleSystem>();
		ParticleSystem.ShapeModule shape = particleSystem.shape;
		shape.radius = distanceBetween / 2.0f;

		laserEffectInstance.transform.position = transform.position + (-transform.forward * distanceBetween / 2.0f);
	}

	protected void SetLaserPosition()	// default for not hitting anything
	{
		SetLaserPosition(transform.position + (-transform.forward * 4.0f));
	}
}
