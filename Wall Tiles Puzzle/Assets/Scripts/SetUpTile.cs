using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SetUpTile : MonoBehaviour
{
	public int indexX = 0;
	public int indexY = 0;
	public int indexZ = 0;

	public Vector3 facingDirection;

	public List<GameObject> TilePrefabs = new List<GameObject>();

	public void TranslateTile(int moveX, int moveY, int moveZ)
	{
		UpdateInEditor(indexX + moveX, indexY + moveY, indexZ + moveZ, facingDirection);
		UpdateTransform();
	}

	public void UpdateInEditor(int posX, int posY, int posZ, Vector3 dir)
	{
		SerializedObject serializedObject = new SerializedObject(this);
		SerializedProperty sp_indexX = serializedObject.FindProperty("indexX");
		SerializedProperty sp_indexY = serializedObject.FindProperty("indexY");
		SerializedProperty sp_indexZ = serializedObject.FindProperty("indexZ");
		SerializedProperty sp_facingDirection = serializedObject.FindProperty("facingDirection");

		sp_indexX.intValue = posX;
		sp_indexY.intValue = posY;
		sp_indexZ.intValue = posZ;
		sp_facingDirection.vector3Value = dir;

		serializedObject.ApplyModifiedProperties();
	}

	public void OrientTile(Vector3 _facingDirection)
	{
		SerializedObject serializedObject = new SerializedObject(this);
		SerializedProperty sp_facingDirection = serializedObject.FindProperty("facingDirection");

		sp_facingDirection.vector3Value = _facingDirection;
		serializedObject.ApplyModifiedProperties();

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
