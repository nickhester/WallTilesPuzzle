using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetUpTile : MonoBehaviour
{
	public int indexX = 0;
	public int indexY = 0;
	public int indexZ = 0;

	public Vector3 facingDirection;

	public List<GameObject> TilePrefabs = new List<GameObject>();

	public void TranslateTile(int moveX, int moveY, int moveZ)
	{
		indexX += moveX;
		indexY += moveY;
		indexZ += moveZ;

		UpdateTransform();
	}

	public void OrientTile(Vector3 _facingDirection)
	{
		facingDirection = _facingDirection;
		transform.localRotation = Quaternion.LookRotation(facingDirection);

		UpdateTransform();
	}

	private void UpdateTransform()
	{
		float _x = 0.0f;
		float _y = 0.0f;
		float _z = 0.0f;

		if (facingDirection == Vector3.left)
		{
			_x = 0.5f;
			_z = 0.5f;
		}
		else if (facingDirection == Vector3.right)
		{
			_x = -0.5f;
			_z = 0.5f;
		}
		else if (facingDirection == Vector3.down)
		{
			_y = 0.5f;
			_z = 0.5f;
		}
		else if (facingDirection == Vector3.up)
		{
			_y = -0.5f;
			_z = 0.5f;
		}

		transform.localPosition = new Vector3(indexX + _x, indexY + _y, indexZ + _z);
	}
}
