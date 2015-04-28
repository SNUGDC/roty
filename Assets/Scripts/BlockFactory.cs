using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;

public class BlockFactory {

	private static List<Polyomino> candidates = new List<Polyomino>() {
		new Polyomino(new Point2(0, 0), new Point2(1, 0), new Point2(1, 1)),
		new Polyomino(new Point2(0, 0), new Point2(1, 0), new Point2(2, 0))
	};

	public static Block generate(int depth, Polyomino frame = null) {
		frame = frame ?? candidates.Sample ().Single ();
		var offset = new Point2 (Random.Range (0, TileContainer.Instance.size - frame.constraints.x),
		                         Random.Range (0, TileContainer.Instance.size - frame.constraints.y));

		List<Tile> tiles = new List<Tile> ();
		foreach (var point in frame.points) {
			tiles.Add(TileContainer.Instance.getTile (point + offset, depth));
		}
		return new Block(tiles, frame);
	}
}