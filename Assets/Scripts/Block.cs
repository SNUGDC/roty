using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;

public class Block {
	public readonly Polyomino polymino;
	public readonly List<Tile> tiles;

	public Block(List<Tile> tiles, Polyomino polymino) {
		this.tiles = tiles;
		this.polymino = polymino;
	}


	public void dye(Color color) {
		foreach (var tile in tiles) {
			tile.gameObject.GetComponent<SpriteRenderer>().color = color;
		}
	}

	bool isOutOfBounds() {
		return tiles.All (tile => 
		                   tile.point.x < 0 || tile.point.x >= TileContainer.Instance.size || 
		                   tile.point.y < 0 || tile.point.y >= TileContainer.Instance.size);
	}

	public void transition(Point2 movement) {
		var depth = tiles.First ().depth;
		var moved = from tile in tiles select (Point2)tile.transform.position + movement;
		moveTiles (moved);
	}

	// clockwise
	public void rotateQuarter(Point2 pivot) {
		var depth = tiles.First ().depth;
		var rotated = from tile in tiles 
			let delta = tile.point - pivot
			select new Point2(-delta.y, delta.x) + pivot;
		moveTiles (rotated);
	}

	public void moveTiles(IEnumerable<Point2> points) {
		var tileSets = tiles.Zip (
			points, 
			(tile, point) =>  new { target=tile, point=point }
		);
		foreach (var tileSet in tileSets) {
			TileContainer.Instance.moveTile(tileSet.target, tileSet.point);
		}
	}
	public override string ToString() {
		var result = "Block{";
		foreach (var tile in tiles) {
			result += tile.point;
		}
		return result + "}";
	}
}
