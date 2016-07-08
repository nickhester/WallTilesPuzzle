using UnityEngine;
using System.Collections;

public class TilePlayer : MonoBehaviour
{
	public enum Direction
	{
		RIGHT,
		LEFT,
		UP,
		DOWN
	}

	void Start ()
	{
	
	}
	
	void Update ()
	{
		if (Input.GetButtonDown("TileRight"))
		{
			MovePlayer(Direction.RIGHT);
		}
		else if (Input.GetButtonDown("TileLeft"))
		{
			MovePlayer(Direction.LEFT);
		}
		if (Input.GetButtonDown("TileUp"))
		{
			MovePlayer(Direction.UP);
		}
		else if (Input.GetButtonDown("TileDown"))
		{
			MovePlayer(Direction.DOWN);
		}
	}

	bool MovePlayer(Direction d)
	{
		TileObject myTileObject = GetComponent<TileObject>();
		TileBase tileToMoveTo = null;
		switch (d)
		{
			case Direction.RIGHT:
				{
					tileToMoveTo = myTileObject.GetBaseTile().neighborRight;
					break;
				}
			case Direction.LEFT:
				{
					tileToMoveTo = myTileObject.GetBaseTile().neighborLeft;
					break;
				}
			case Direction.UP:
				{
					tileToMoveTo = myTileObject.GetBaseTile().neighborUp;
					break;
				}
			case Direction.DOWN:
				{
					tileToMoveTo = myTileObject.GetBaseTile().neighborDown;
					break;
				}
			default:
				{
					break;
				}
		}

		if (tileToMoveTo != null)
		{
			myTileObject.MoveToBaseTile(tileToMoveTo);
			return true;
		}
		else
		{
			return false;
		}
	}
}
