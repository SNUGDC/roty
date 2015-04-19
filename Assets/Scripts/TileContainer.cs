using UnityEngine;
using System.Collections;
using System;

public class TileContainer : MonoBehaviour {
	#region singleton
	private static TileContainer _instance;
	public static TileContainer Instance {
		get {
			if (!_instance) {
				_instance = GameObject.FindObjectOfType(typeof(TileContainer)) as TileContainer;
				if (!_instance) {
					GameObject container = new GameObject();
					container.name = "TileContainer";
					_instance = container.AddComponent(typeof(TileContainer)) as TileContainer;
				}
			}
			return _instance;
		}
	}
	#endregion
	public Tile[][] tiles;
	public Tile baseTile;
	public int size = 5;

	public const int MAX_DEPTH = 3;
	public const int FLOOR_DEPTH = 0;
	public const int SOURCE_DEPTH = 2;
	public const int DESTINATION_DEPTH = 1;

	public void createMap() {
		tiles = new Tile[MAX_DEPTH][];
		for (int i = 0; i < MAX_DEPTH; i ++) {
			tiles[i] = new Tile[size * size];
		}
		for (int y = 0 ; y < size ; y++) {
			for (int x = 0 ; x < size ; x++) {
				Tile tile = createTile(x, y);
				tiles[FLOOR_DEPTH][y * size + x] = tile;
			}
		}
	}

	public Tile createTile(int x, int y, int depth = FLOOR_DEPTH) {
		Tile tile = Instantiate(baseTile);
		tile.transform.parent = transform;
		tile.name = "Tile (" + x.ToString() + ", " + y.ToString() + ")";
		tile.transform.position = new Vector3(x, y, -depth);
		return tile;
	}

	public Tile createTile(Vector2 v, int depth = FLOOR_DEPTH) {
		return this.createTile((int)v.x, (int)v.y, depth);
	}

	public Tile getTile(int x, int y, int depth = FLOOR_DEPTH) {
		return tiles[depth][size * y + x];
	}

	public Tile getTile(Vector2 v, int depth = FLOOR_DEPTH) {
		return this.getTile((int)v.x, (int)v.y, depth);
	}

	public void moveTile(Tile tile, Vector2 v) {
		int depth = (int)-tile.transform.position.z;
		Vector2 original = new Vector2 (tile.transform.position.x, tile.transform.position.y);
		tiles[depth][(int)original.y * size + (int)original.x] = null;
		tiles[depth][(int)v.y * size + (int)v.x] = tile;
		tile.transform.position = new Vector3(v.x, v.y, -depth);
	}
}
