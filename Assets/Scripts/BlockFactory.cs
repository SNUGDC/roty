using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockFactory {
	private const Vector2[,] candidates = new Vector2[,]{{new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1)},
														 {new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0)}};
	public static Tile[] generate() {
		var offset = new Vector2 (Random.Range (0, TileContainer.Instance.size),
		                          Random.Range (0, TileContainer.Instance.size));
		var elected = candidates [Random.Range (0, candidates.Length)];
		var result = new List<Tile> ();
		foreach (var coordinate in elected) {
			result.Add(TileContainer.Instance.getTile (coordinate + offset));
		}
	}
}
