using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;

public class Block {
	public readonly Polyomino polymino;
	public readonly IEnumerable<Tile> tiles;
	public readonly Color color;

	public Block(IEnumerable<Tile> tiles, Polyomino polymino) {
		this.tiles = tiles;
		this.polymino = polymino;
	}

	public int depth {
		get {
			return tiles.First().depth;
		}
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
		/*
		var moved = from tile in tiles select (Point2)tile.transform.position + movement;
		from point in moved select TileContainer.Instance.getTile (point, depth),
		polymino
			);
		*/
		var moved = new List<Point2>();
		foreach (var tile in tiles) {
			moved.Add((Point2)tile.transform.position + movement);
		}
		var movedTiles = new List<Tile> ();
		foreach (var point in moved) {
			movedTiles.Add(TileContainer.Instance.getTile (point, depth));
		}
		return new Block (movedTiles, polymino);
	}

	// clockwise
	public Block rotateQuarter(Point2 pivot, int quadrant = 1) {
		var depth = tiles.First ().depth;
		/*
		var rotated = from tile in tiles 
			let delta = tile.point - pivot
			select new Point2(-delta.y, delta.x) + pivot;
		return new Block (
			from point in rotated select TileContainer.Instance.getTile (point, depth),
			polymino
		);
		*/
		var rotated = new List<Point2>();
		foreach(var tile in tiles) {
			var deltaPoint = tile.point - pivot;
			/*
			switch(quadrant) {
			case 1: deltaPoint = new Point2(-deltaPoint.y, deltaPoint.x); break;
			case 2: deltaPoint = new Point2(-deltaPoint.x, -deltaPoint.y); break;
			case 3: deltaPoint = new Point2(deltaPoint.y, -deltaPoint.x); break;
			}
			*/
			rotated.Add(new Point2(-deltaPoint.y, deltaPoint.x) + pivot);
		}

		var rotatedTiles = new List<Tile> ();
		foreach (var point in rotated) {
			rotatedTiles.Add(TileContainer.Instance.getTile (point, depth));
		}
		return new Block (rotatedTiles, polymino);
	}

	public void moveTiles(Block newBlock) {
		TileContainer.Instance.moveBlock (this, newBlock);
	}

	public override bool Equals(object obj) {
		return Equals(obj as Block);
	}
	
	public bool Equals(Block obj) {
		var idx1List = from t1 in tiles orderby t1.point.idx select t1.point.idx;
		var idx2List = from t2 in obj.tiles orderby t2.point.idx select t2.point.idx;
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

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}

	public override string ToString() {
		var result = "Block{";
		foreach (var tile in tiles) {
			result += tile.point;
		}
		return result + "}";
	}
}
