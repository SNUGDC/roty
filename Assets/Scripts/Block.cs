using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;

public class Block {
	public readonly Polyomino polymino;
	public readonly IEnumerable<Tile> tiles;

	public Block(IEnumerable<Tile> tiles, Polyomino polymino) {
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

	public Block transition(Point2 movement) {
		var depth = tiles.First ().depth;
		var moved = from tile in tiles select (Point2)tile.transform.position + movement;
		return new Block (from point in moved select TileContainer.Instance.getTile(point, depth), polymino);
	}

	// clockwise
	public Block rotateQuarter(Point2 pivot) {
		var depth = tiles.First ().depth;
		var rotated = from tile in tiles 
			let delta = (Point2)tile.transform.localPosition - pivot
			select new Point2(-delta.y, delta.x) + pivot;
		return new Block (from point in rotated select TileContainer.Instance.getTile(point, depth), polymino);
	}

	public void move(Block block) {
		var tileSets = tiles.Zip (
			block.tiles, 
			(oldTile, newTile) =>  new { OldTile=oldTile, NewTile=newTile }
		);

		foreach (var tileSet in tileSets) {
			TileContainer.Instance.moveTile (tileSet.OldTile, tileSet.NewTile.point);
		}
	}
}
