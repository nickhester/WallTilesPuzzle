using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
	private List<TileBase> baseTiles = new List<TileBase>();
	private List<TileObject> objectTiles = new List<TileObject>();
	public GameObject playerPrefab;
	private TilePlayer player;

	void Start ()
	{
		baseTiles.AddRange(transform.GetComponentsInChildren<TileBase>());
		objectTiles.AddRange(transform.GetComponentsInChildren<TileObject>());

		TileObject playerStartTile = null;

		foreach (TileObject tile in objectTiles)
		{
			// find player start, but don't spawn player there yet
			if (tile.GetMyTileType() == TileObject.TileType.PlayerStart)
			{
				playerStartTile = tile;
			}

			// match it to its base tile to set as parent
			foreach (TileBase tileBase in baseTiles)
			{
				if (tileBase.transform.localPosition == tile.transform.localPosition)
				{
					tile.MoveToBaseTile(tileBase);
				}
			}
		}

		// spawn player on player start
		if (playerStartTile != null)
		{
			GameObject playerGo = (Instantiate(playerPrefab) as GameObject);
			player = playerGo.GetComponent<TilePlayer>();
			TileBase _tileBase = playerStartTile.GetComponentInParent<TileBase>();
			player.transform.SetParent(_tileBase.transform);
			playerGo.GetComponent<TileObject>().MoveToBaseTile(_tileBase);
		}
		else
		{
			Debug.LogError("Puzzle found no player start tile");
		}
	}
	
	void Update ()
	{
		
	}
}
