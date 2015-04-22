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
		return new Block (
			from point in moved select TileContainer.Instance.getTile(point, depth),
			polymino
		);
	}

	// clockwise
	public Block rotateQuarter(Point2 pivot) {
		var depth = tiles.First ().depth;
		var rotated = from tile in tiles 
			let delta = tile.point - pivot
			select new Point2(-delta.y, delta.x) + pivot;
		return new Block (
			from point in rotated select TileContainer.Instance.getTile(point, depth),
			polymino
		);
	}

	public void moveTiles(Block newBlock) {
		var tileSets = tiles.Zip (
			newBlock.tiles,
			(oldTile, newTile) =>  new { oldTile=oldTile, newTile=newTile }
		);
		foreach (var tileSet in tileSets) {
			TileContainer.Instance.moveTile(tileSet.oldTile, tileSet.newTile.point);
		}
	}

	public static bool operator !=(Block b1, Block b2) {
		var idx1List = from t1 in b1.tiles orderby t1.point.idx select t1.point.idx;
		var idx2List = from t2 in b2.tiles orderby t2.point.idx select t2.point.idx;
		var idxSets = idx1List.Zip(
			idx2List,
			(idx1, idx2) => new { idx1, idx2 }
		);
		foreach (var idxSet in idxSets) {
			if (idxSet.idx1 != idxSet.idx2) {
				return true;
			}
		}
		return false;
	}

	public static bool operator ==(Block b1, Block b2) {
		var idx1List = from t1 in b1.tiles orderby t1.point.idx select t1.point.idx;
		var idx2List = from t2 in b2.tiles orderby t2.point.idx select t2.point.idx;
		var idxSets = idx1List.Zip(
			idx2List,
			(idx1, idx2) => new { idx1, idx2 }
		);
		foreach (var idxSet in idxSets) {
			if (idxSet.idx1 != idxSet.idx2) {
				return false;
			}
		}
		return true;
	}

	public override string ToString() {
		var result = "Block{";
		foreach (var tile in tiles) {
			result += tile.point;
		}
		return result + "}";
	}
}
