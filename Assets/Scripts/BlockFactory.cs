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
		var offset = new Vector2 (Random.Range (0, TileContainer.Instance.size),
		                          Random.Range (0, TileContainer.Instance.size));
		var result = new List<Tile> ();
		foreach (var coordinate in electedBlock) {
			result.Add(TileContainer.Instance.getTile (coordinate + offset));
		}
		return result.ToArray ();
		
	}
}
