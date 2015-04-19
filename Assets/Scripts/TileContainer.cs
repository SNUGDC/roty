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

	public void createMap() {
		tiles = new Tile[Depth.MAX_DEPTH][];
		for (int i = 0; i < Depth.MAX_DEPTH; i ++) {
			tiles[i] = new Tile[size * size];
		}
		for (int y = 0 ; y < size ; y++) {
			for (int x = 0 ; x < size ; x++) {
				Tile tile = createTile(x, y);
				tiles[Depth.FLOOR_DEPTH][y * size + x] = tile;
			}
		}
		transform.localScale = new Vector3 (5.0f / size, 5.0f / size, 1);
	}

	public Tile createTile(int x, int y, int depth = Depth.FLOOR_DEPTH) {
		Tile tile = Instantiate(baseTile);
		tile.transform.parent = transform;
		tile.name = "Tile (" + x.ToString() + ", " + y.ToString() + ")";
		tile.transform.localPosition = new Vector3(x, y, -depth);
		tile.transform.localScale = Vector3.one;
		return tile;
	}

	public Tile createTile(Point2 v, int depth = Depth.FLOOR_DEPTH) {
		return this.createTile(v.x, v.y, depth);
	}

	public Tile getTile(int x, int y, int depth = Depth.FLOOR_DEPTH) {
		return tiles[depth][size * y + x];
	}

	public Tile getTile(Point2 v, int depth = Depth.FLOOR_DEPTH) {
		return this.getTile(v.x, v.y, depth);
	}

	public void moveTile(Tile tile, Point2 v) {
		int depth = (int)-tile.transform.position.z;
		Point2 original = (Point2)tile.transform.position;
		tiles[depth][(int)original.y * size + (int)original.x] = null;
		tiles[depth][(int)v.y * size + (int)v.x] = tile;
		tile.transform.localPosition = new Vector3(v.x, v.y, -depth);
	}
}
