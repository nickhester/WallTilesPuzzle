using UnityEngine;
using System.Collections;

public class TileObject : MonoBehaviour
{
	public enum TileType
	{
		None,
		PlayerStart,
		Exit,
		Obstacle
	}

	public TileType myTileType;

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}

	public TileType GetMyTileType()
	{
		return myTileType;
	}

	public void MoveToBaseTile(TileBase tileBase)
	{
		Transform _transform = tileBase.transform;

		transform.SetParent(_transform);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
	}

	public TileBase GetBaseTile()
	{
		return GetComponentInParent<TileBase>();
	}
}
