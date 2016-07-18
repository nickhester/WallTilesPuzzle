using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TileObject : MonoBehaviour
{
	public enum TileType
	{
		None,
		PlayerStart,
		Exit,
		Obstacle,
		ObstacleDestructible,
		Crate,
		LaserA,
		LaserB,
		Gate,
		PressurePlate
	}

	public enum EffectType
	{
		LaserA,
		LaserB
	}

	public enum Direction
	{
		UP,
		RIGHT,
		DOWN,
		LEFT
	}

	protected int ccwRotation = 0;
	private float tileAcrossCheckRayMaxDistance = 100.0f;

	public bool MoveToBaseTile(Direction _comingFromDirection, TileBase _tileBase, int _ccwRotation)
	{
		return MoveToBaseTile(_comingFromDirection, _tileBase, _ccwRotation, false);
	}

	public bool MoveToBaseTile(Direction _comingFromDirection, TileBase _tileBase, int _ccwRotation, bool _isEditor)
	{
		if (!_isEditor)
		{
			foreach (TileObject _tileObject in _tileBase.GetAllOccupyingTileObjects())
			{
				if (!_tileObject.CheckIfObjectCanMoveOnMe(this, _comingFromDirection))
				{
					return false;
				}
			}
		}

		Transform _transform = _tileBase.transform;

		transform.SetParent(_transform);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.Rotate(Vector3.back, (90.0f * _ccwRotation));
		transform.localScale = Vector3.one;
		return true;
	}

	// simple override for editor movement
	public bool MoveToBaseTile(TileBase _tileBase)
	{
		return MoveToBaseTile(Direction.DOWN, _tileBase, 0, true);
	}

	public TileBase GetBaseTile()
	{
		return GetComponentInParent<TileBase>();
	}

	public bool TryMoveObject(Direction _direction)
	{
		TileBase myTileBase = GetBaseTile();
		TileBase tileToMoveTo = null;
		switch (_direction)
		{
			case Direction.RIGHT:
				{
					tileToMoveTo = myTileBase.neighborRight;
					break;
				}
			case Direction.LEFT:
				{
					tileToMoveTo = myTileBase.neighborLeft;
					break;
				}
			case Direction.UP:
				{
					tileToMoveTo = myTileBase.neighborUp;
					break;
				}
			case Direction.DOWN:
				{
					tileToMoveTo = myTileBase.neighborDown;
					break;
				}
			default:
				{
					break;
				}
		}

		if (tileToMoveTo != null)
		{
			// figure out new ccwRotation
			SetUpTile mySetUpTile = myTileBase.GetComponent<SetUpTile>();
			SetUpTile destinationSetUpTile = tileToMoveTo.GetComponent<SetUpTile>();
			int _ccwRotation = ccwRotation;
			if (mySetUpTile.facingDirection == destinationSetUpTile.facingDirection)
			{
				// stay the same
			}
			else if (destinationSetUpTile.facingDirection == Vector3.up)
			{
				if (mySetUpTile.facingDirection == Vector3.left)
				{
					_ccwRotation = 1;
				}
				else if (mySetUpTile.facingDirection == Vector3.right)
				{
					_ccwRotation = 3;
				}
				else if (mySetUpTile.facingDirection == Vector3.back)
				{
					_ccwRotation = 2;
				}
				else if (mySetUpTile.facingDirection == Vector3.forward)
				{
					_ccwRotation = 0;
				}
			}
			else if (destinationSetUpTile.facingDirection == Vector3.down)
			{
				if (mySetUpTile.facingDirection == Vector3.left)
				{
					_ccwRotation = 3;
				}
				else if (mySetUpTile.facingDirection == Vector3.right)
				{
					_ccwRotation = 1;
				}
				else if (mySetUpTile.facingDirection == Vector3.back)
				{
					_ccwRotation = 2;
				}
				else if (mySetUpTile.facingDirection == Vector3.forward)
				{
					_ccwRotation = 0;
				}
			}
			else
			{
				_ccwRotation = 0;
			}

			// First check if current tile allows the move off
			foreach (TileObject _tileObject in myTileBase.GetAllOccupyingTileObjects())
			{
				if (!_tileObject.CheckIfObjectCanMoveOffMe(this, _direction))
				{
					return false;
				}
			}

			// if move is allowed by destination objects
			if (MoveToBaseTile(tileToMoveTo.WhichDirectionIsNeighbor(myTileBase), tileToMoveTo, _ccwRotation))
			{
				// call "on object move" events
				foreach (TileObject targetTileObject in tileToMoveTo.GetAllOccupyingTileObjects())
				{
					if (targetTileObject != this)
					{
						targetTileObject.OnObjectMoveOn(this, _direction);
					}
				}
				foreach (TileObject startTileObject in myTileBase.GetAllOccupyingTileObjects())
				{
					if (startTileObject != this)
					{
						startTileObject.OnObjectMoveOff(startTileObject, _direction);
					}
				}

				ccwRotation = _ccwRotation;
				return true;
			}
		}
		return false;
	}

	protected List<TileObject> GetTileObjectsAcross()
	{
		TileBase tb = GetBaseTileAcross();
		if (tb != null)
		{
			List<TileObject> retList = new List<TileObject>();
			retList.AddRange(tb.GetComponentsInChildren<TileObject>());
			return retList;
		}
		return null;
	}

	public TileBase GetBaseTileAcross()
	{
		TileBase _tileOrigin = GetComponentInParent<TileBase>();
		Ray ray = new Ray(_tileOrigin.transform.position, -_tileOrigin.transform.forward);
		RaycastHit hit;

		TileBase hitTile = null;
		if (Physics.Raycast(ray, out hit, tileAcrossCheckRayMaxDistance))
		{
			hitTile = hit.transform.gameObject.GetComponent<TileBase>();
			if (hitTile == null)
			{
				hitTile = hit.transform.GetComponentInParent<TileBase>();
			}
		}

		return hitTile;
	}

	public GameObject CheckAcrossForObject()
	{
		TileBase _tileOrigin = GetComponentInParent<TileBase>();
		Ray ray = new Ray(_tileOrigin.transform.position, -_tileOrigin.transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, tileAcrossCheckRayMaxDistance))
		{
			return hit.transform.gameObject;
		}
		return null;
	}

	// abstract and virtual classes

	public virtual bool CheckIfObjectCanMoveOnMe(TileObject _tileObject, Direction _fromDirection) { return true; }

	public virtual bool CheckIfObjectCanMoveOffMe(TileObject _tileObject, Direction _toDirection) { return true; }

	public virtual void OnObjectMoveOn(TileObject _tileObjectMovingOn, Direction _movingDirection) { }

	public virtual void OnObjectMoveOff(TileObject _tileObjectMovingOff, Direction _movingDirection) { }

	public virtual void GetHitByEffect(EffectType _effectType) { }

	public virtual void Activate(bool _isActivating) { }
}
