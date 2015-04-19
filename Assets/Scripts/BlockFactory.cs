using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockFactory {
	private static Vector2[][] candidates = new Vector2[][]{
		new Vector2[]{new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1)},
		new Vector2[]{new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0)}
	};
	public static Tile[] generate() {
		var electedBlock = candidates[Random.Range (0, candidates.Length)];
		var constraints = new Vector2 ();
		foreach (var v in electedBlock) {
			constraints.x = Mathf.Max(v.x, constraints.x);
			constraints.y = Mathf.Max(v.y, constraints.y);
		}
		var offset = new Vector2 (Random.Range (0, TileContainer.Instance.size - (int)constraints.x),
		                          Random.Range (0, TileContainer.Instance.size - (int)constraints.y));
		var result = new List<Tile> ();
		foreach (var coordinate in electedBlock) {
			result.Add(
				TileContainer.Instance.createTile(coordinate + offset, TileContainer.DESTINATION_DEPTH)
			);
		}
		return result.ToArray ();
	}
}
