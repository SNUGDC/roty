using UnityEngine;
using System.Collections;
using System.Linq;

public class StageGenerator : MonoBehaviour {
	public Tile[] source;
	public Tile[] destination;
	public int transformCount;
	public double mutationRate;

	public Color destinationColor = new Color32(240,98,146,255);
	public Color sourceColor = new Color32(248,187,208,255);

	void dye(Tile[] tiles, Color color) {
		foreach (var tile in tiles) {
			tile.gameObject.GetComponent<SpriteRenderer>().color = color;
		}
	}
	void createStage() {
		destination = BlockFactory.generate ();
		source = new Tile[destination.Length];
		dye (destination, destinationColor);
		for (int i = 0; i < destination.Length; i ++) {
			var tile = destination[i];
			source[i] = TileContainer.Instance.createTile((Vector2)tile.transform.localPosition,
			                                              TileContainer.SOURCE_DEPTH);
		}
		dye (source, sourceColor);
		for (int i = 0 ; i < transformCount ; i ++) {
			if (Random.value < mutationRate) {
				transitionRandom(source);
			} else {
				rotateRandom(source);
			}
		}
	}

	bool isValid(Vector2[] positions) {
		foreach (var position in positions) {
			if (position.x < 0 || position.x >= TileContainer.Instance.size || 
			    position.y < 0 || position.y >= TileContainer.Instance.size) {
				return false;
			}
		}
		return true;
	}

	void transitionRandom(Tile[] block) {
		var movement = new Vector2 (Random.value > 0.5 ? 1 : -1,
		                           Random.value > 0.5 ? 1 : -1);
		Vector2[] nextPositions = new Vector2[block.Length];
		for (int i = 0; i < block.Length; i++) {
			nextPositions[i] = new Vector2(block[i].transform.position.x + movement.x,
			                               block[i].transform.position.y + movement.y);
		}
		if (isValid (nextPositions)) {
			for (int i = 0; i < block.Length; i++) {
				TileContainer.Instance.moveTile (block [i], nextPositions [i]);
			}
		} else {
			transitionRandom(block);
		}
	}

	void rotateRandom(Tile[] block) {
		Tile point = block[Random.Range(0, block.Length)];
		Vector2[] nextPositions = new Vector2[block.Length];
		for (int i = 0; i < block.Length; i++) {
			var tile = block[i];
			Vector2 delta = tile.transform.localPosition - point.transform.localPosition;
			nextPositions[i] = new Vector2(-delta.y, delta.x) + (Vector2)point.transform.localPosition;
		}
		if (isValid (nextPositions)) {
			for (int i = 0; i < block.Length; i++) {
				TileContainer.Instance.moveTile (block [i], nextPositions [i]);
			}
		} else {
			rotateRandom(block);
		}
	}

	// Use this for initialization
	void Start () {
		TileContainer.Instance.createMap ();
		createStage ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
