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
		Obstacle
	}

	public enum Direction
	{
		UP,
		RIGHT,
		DOWN,
		LEFT
	}

	protected int ccwRotation = 0;

	public abstract bool CanObjectMoveOntoMe(TileObject _tileObject, Direction _fromDirection);

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
				if (!_tileObject.CanObjectMoveOntoMe(this, _comingFromDirection))
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

	public bool MoveObject(Direction _direction)
	{
		TileObject myTileObject = GetComponent<TileObject>();
		TileBase myTileBase = myTileObject.GetBaseTile();
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

			// if move is allowed by destination objects
			if (myTileObject.MoveToBaseTile(tileToMoveTo.WhichDirectionIsNeighbor(myTileBase), tileToMoveTo, _ccwRotation))
			{
				ccwRotation = _ccwRotation;
				return true;
			}
		}
		return false;
	}
}
