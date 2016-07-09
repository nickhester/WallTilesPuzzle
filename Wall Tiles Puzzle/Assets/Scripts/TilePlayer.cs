using UnityEngine;
using System.Collections;

public class TilePlayer : MonoBehaviour
{
	private int ccwRotation = 0;

	public enum Direction
	{
		UP,
		RIGHT,
		DOWN,
		LEFT
	}

	void Start ()
	{
		
	}
	
	void Update ()
	{
		bool isReceivingDirectionalInput = false;
		Direction inputDirection = Direction.UP;
		if (Input.GetButtonDown("TileRight"))
		{
			inputDirection = Direction.RIGHT;
			isReceivingDirectionalInput = true;
		}
		else if (Input.GetButtonDown("TileLeft"))
		{
			inputDirection = Direction.LEFT;
			isReceivingDirectionalInput = true;
		}
		if (Input.GetButtonDown("TileUp"))
		{
			inputDirection = Direction.UP;
			isReceivingDirectionalInput = true;
		}
		else if (Input.GetButtonDown("TileDown"))
		{
			inputDirection = Direction.DOWN;
			isReceivingDirectionalInput = true;
		}

		if (isReceivingDirectionalInput)
		{
			MovePlayer((Direction)(((int)inputDirection + ccwRotation) % 4));
		}
	}

	bool MovePlayer(Direction d)
	{
		TileObject myTileObject = GetComponent<TileObject>();
		TileBase myTileBase = myTileObject.GetBaseTile();
		TileBase tileToMoveTo = null;
		switch (d)
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
			if (mySetUpTile.facingDirection == destinationSetUpTile.facingDirection)
			{
				// stay the same
			}
			else if (destinationSetUpTile.facingDirection == Vector3.up)
			{
				if (mySetUpTile.facingDirection == Vector3.left)
				{
					ccwRotation = 1;
				}
				else if (mySetUpTile.facingDirection == Vector3.right)
				{
					ccwRotation = 3;
				}
				else if (mySetUpTile.facingDirection == Vector3.back)
				{
					ccwRotation = 2;
				}
				else if (mySetUpTile.facingDirection == Vector3.forward)
				{
					ccwRotation = 0;
				}
			}
			else if (destinationSetUpTile.facingDirection == Vector3.down)
			{
				if (mySetUpTile.facingDirection == Vector3.left)
				{
					ccwRotation = 3;
				}
				else if (mySetUpTile.facingDirection == Vector3.right)
				{
					ccwRotation = 1;
				}
				else if (mySetUpTile.facingDirection == Vector3.back)
				{
					ccwRotation = 2;
				}
				else if (mySetUpTile.facingDirection == Vector3.forward)
				{
					ccwRotation = 0;
				}
			}
			else
			{
				ccwRotation = 0;
			}

			myTileObject.MoveToBaseTile(tileToMoveTo, ccwRotation);
			return true;
		}
		else
		{
			return false;
		}
	}
}
