using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileObject_Laser : TileObject_Crate
{
	public GameObject laserEffectPrefab;
	protected GameObject laserEffectInstance;
	protected EffectType effectType;

	private float tileAcrossCheckRayMaxDistance = 100.0f;
	protected RaycastHit acrossHitObject;
	protected TileSetInfo acrossHitTile = null;
	
	protected void Start()
	{
		laserEffectInstance = Instantiate(laserEffectPrefab, transform.position, Quaternion.identity) as GameObject;
		laserEffectInstance.transform.right = -transform.forward;
		laserEffectInstance.transform.SetParent(transform);
		laserEffectInstance.transform.localScale = Vector3.one;
	}

	protected void Update()
	{
		acrossHitTile = null;
		acrossHitObject = CheckAcrossForObject();
		if (acrossHitObject.transform != null)
		{
			TileBase tb = GetBaseTileFromObject(acrossHitObject.transform.gameObject);
			if (tb != null)
			{
				acrossHitTile = GetTileSetFromTileBase(tb);
			}
		}

		if (acrossHitTile != null)
		{
			if (acrossHitTile.tileBase != null)
			{
				SetLaserPosition(acrossHitTile.tileBase.transform.position);
			}

			if (acrossHitTile.tileObjects.Count > 0)
			{
				foreach (TileObject tiles in acrossHitTile.tileObjects)
				{
					tiles.GetHitByEffect(effectType);
				}
			}
		}
		else if (acrossHitObject.transform != null)
		{
			SetLaserPosition(acrossHitObject.point);
		}
		else
		{
			SetLaserPosition();
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

	protected TileSetInfo GetTileSetFromTileBase(TileBase tb)
	{
		List<TileObject> retList = new List<TileObject>();
		retList.AddRange(tb.GetComponentsInChildren<TileObject>());
		return new TileSetInfo(retList, tb);
	}

	public RaycastHit CheckAcrossForObject()
	{
		TileBase _tileOrigin = GetComponentInParent<TileBase>();
		Ray ray = new Ray(_tileOrigin.transform.position, -_tileOrigin.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, tileAcrossCheckRayMaxDistance))
		{
			return hit;
		}
		return hit;
	}

}
