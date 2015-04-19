using UnityEngine;
using System.Collections;

public class StageGenerator : MonoBehaviour {
	public Tile[] source;
	public Tile[] destination;
	public int rotateCount;

	Color destinationColor = new Color32(240,98,146,255);
	Color sourceColor = new Color32(248,187,208,255);

	void dye(Tile[] tiles, Color color) {
		foreach (var tile in tiles) {
			tile.gameObject.GetComponent<Renderer>().material.color = color;
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
		for (int i = 0 ; i < rotateCount ; i ++) {
			rotateRandom(source);
		}
	}

	void rotateRandom(Tile[] block) {
		Tile point = block[Random.Range(0, block.Length)];
		Vector2[] nextPositions = new Vector2[block.Length];
		for (int i = 0; i < block.Length; i++) {
			var tile = block[i];
			Vector2 delta = tile.transform.localPosition - point.transform.localPosition;
			nextPositions[i] = new Vector2(-delta.y, delta.x) + (Vector2)point.transform.localPosition;
			if (nextPositions[i].x < 0 || nextPositions[i].x >= TileContainer.Instance.size || 
			    nextPositions[i].y < 0 || nextPositions[i].y >= TileContainer.Instance.size) {
				rotateRandom(block);
				return;
			}
		}
		for (int i = 0; i < block.Length; i++) {
			TileContainer.Instance.moveTile(block[i], nextPositions[i]);
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
