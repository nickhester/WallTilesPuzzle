using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
	private List<TileBase> baseTiles = new List<TileBase>();
	private List<TileObject> objectTiles = new List<TileObject>();
	public GameObject tilePlayerPrefab;
	private TileObject_Player tilePlayer;
	private bool isPuzzleActive = true;

	private float tileAcrossCheckRayMaxDistance = 100.0f;

	void Start ()
	{
		List<PuzzleTrigger> puzzleTriggers = new List<PuzzleTrigger>();
		puzzleTriggers.AddRange(GameObject.FindObjectsOfType<PuzzleTrigger>());
		foreach(PuzzleTrigger pt in puzzleTriggers)
		{
			pt.PuzzleReportIn(gameObject.scene.name);
		}

		baseTiles.AddRange(transform.GetComponentsInChildren<TileBase>());
		objectTiles.AddRange(transform.GetComponentsInChildren<TileObject>());

		TileObject playerStartTile = null;

		foreach (TileObject tile in objectTiles)
		{
			// find player start, but don't spawn player there yet
			if (tile as TileObject_PlayerStart)
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
			GameObject playerGo = (Instantiate(tilePlayerPrefab) as GameObject);
			tilePlayer = playerGo.GetComponent<TileObject_Player>();
			TileBase _tileBase = playerStartTile.GetComponentInParent<TileBase>();
			tilePlayer.transform.SetParent(_tileBase.transform);
			playerGo.GetComponent<TileObject>().MoveToBaseTile(_tileBase);
		}
		else
		{
			Debug.LogError("Puzzle found no player start tile");
		}

		// SetPuzzleActive(false);
	}

	public void SetPuzzleActive(bool _isActive)
	{
		isPuzzleActive = _isActive;
		foreach (TileBase tileBase in baseTiles)
		{
			tileBase.gameObject.SetActive(_isActive);
		}
	}

	public void CompletePuzzle()
	{
		print("You Reached The Exit!");
		GetComponentInParent<PuzzleTrigger>().ReceivePuzzleComplete();
	}

	public TileBase GetBaseTileAcross(TileBase _tileOrigin)
	{
		Ray ray = new Ray(_tileOrigin.transform.position, -_tileOrigin.transform.forward);
		RaycastHit hit;

		Debug.DrawRay(ray.origin, ray.direction * tileAcrossCheckRayMaxDistance, Color.red, 5.0f);

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
}
