using UnityEngine;
using System.Collections;
using System;
using ExtensionMethods;

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
		tile.point = new Point2 (x, y);
		tile.depth = depth;
		tile.name = "Tile (" + x.ToString() + ", " + y.ToString() + ", " + depth.ToString() + ")";
		tile.transform.localPosition = new Vector3(x, y, -depth);
		tile.transform.localScale = Vector3.one;
		return tile;
	}

	public Tile createTile(Point2 v, int depth = Depth.FLOOR_DEPTH) {
		return this.createTile(v.x, v.y, depth);
	}

	private bool isOutOfBound(int x, int y) {
		return x < 0 || x >= TileContainer.Instance.size || 
			y < 0 || y >= TileContainer.Instance.size;
	}

	public Tile getTile(int x, int y, int depth = Depth.FLOOR_DEPTH) {
		if (isOutOfBound (x, y)) {
			throw new OutOfBoundException();
		}
		return tiles[depth][size * y + x];
	}

	public Tile getTile(Point2 v, int depth = Depth.FLOOR_DEPTH) {
		return this.getTile(v.x, v.y, depth);
	}

	public void moveBlock(Block beforeBlock, Block afterBlock) {
		int depth = beforeBlock.depth;

		var tileSets = beforeBlock.tiles.Zip (
			afterBlock.tiles,
			(beforeTile, afterTile) =>  new { beforeTile=beforeTile, afterTile=afterTile }
		);
		foreach (var tileSet in tileSets) {
			tiles[depth][tileSet.beforeTile.point.idx] = tileSet.afterTile;
			tiles[depth][tileSet.afterTile.point.idx] = tileSet.beforeTile;

			tileSet.beforeTile.transform.position = new Vector3(tileSet.afterTile.point.x,
			                                                    tileSet.afterTile.point.y,
			                                                    -depth);

			tileSet.afterTile.transform.position = new Vector3(tileSet.beforeTile.point.x,
			                                                   tileSet.beforeTile.point.y,
			                                                   -depth);
		}
		foreach (var tileSet in tileSets) {
			tileSet.beforeTile.point = (Point2)tileSet.beforeTile.transform.position;
			tileSet.afterTile.point = (Point2)tileSet.afterTile.transform.position;
		}
	}
}
