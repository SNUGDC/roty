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
	public Tile[] baseTiles;
	public int size = 5;

	public void createMap() {
		tiles = new Tile[Depth.MAX_DEPTH][];
		for (int i = 0; i < Depth.MAX_DEPTH; i ++) {
			tiles[i] = new Tile[size * size];
		}
		for (int z = 0; z < Depth.MAX_DEPTH; z++) {
			for (int y = 0 ; y < size ; y++) {
				for (int x = 0 ; x < size ; x++) {
					Tile tile = createTile(x, y, z);
					tiles[z][y * size + x] = tile;
				}
			}
		}
		transform.localScale = new Vector3 (5.0f / size, 5.0f / size, 1);
	}

	public Tile createTile(int x, int y, int depth = Depth.FLOOR_DEPTH) {
		Tile tile = Instantiate(baseTiles[depth]);
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

	public void moveTile(Tile srcTile, Point2 v) {
		int depth = srcTile.depth;
		var dstTile = getTile (v, depth);
		tiles [depth] [dstTile.point.y * size + dstTile.point.x] = srcTile;
		tiles [depth] [srcTile.point.y * size + srcTile.point.x] = dstTile;
		// swap
		var temp = srcTile.transform.position;
		srcTile.transform.position = dstTile.transform.position;
		dstTile.transform.position = temp;
	}
}
