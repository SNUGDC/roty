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
	public Tile[] tiles;
	public Tile baseTile;
	public int size = 5;

	public void createMap() {
		tiles = new Tile[size * size];
		for (int y = 0 ; y < size ; y++) {
			for (int x = 0 ; x < size ; x++) {
				Tile tile = Instantiate(baseTile);
				tile.transform.parent = transform;
				tile.name = "Tile (" + x.ToString() + ", " + y.ToString() + ")";
				tile.transform.localPosition = new Vector3(x, y, 0);
				tiles[y * size + x] = tile;
			}
		}
	}

	public Tile getTile(int x, int y) {
		return tiles[size * y + x];
	}

	public Tile getTile(Vector2 v) {
		return this.getTile((int)v.x, (int)v.y);
	}
}
