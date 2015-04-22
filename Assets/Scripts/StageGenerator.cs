using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using ExtensionMethods;


public class StageGenerator : MonoBehaviour {
	public int rotationCount;
	public double mutationRate;

	void createStage() {
		Block destination = BlockFactory.generate (Depth.DESTINATION_DEPTH);
		Block source = BlockFactory.generate (Depth.SOURCE_DEPTH, destination.polymino);
		destination.dye(new Color(0.969f, 0.737f, 0.816f));
		source.dye(new Color(0.918f, 0.263f, 0.482f));

		var rotationCount = Random.Range (0, 3);
		foreach (var count in Enumerable.Range(0, 3)) {
			Point2 point = source.tiles.Sample().First().point;
			try {
				source.moveTiles(source.rotateQuarter(point));
			} catch(OutOfBoundException) {
				continue;
			}
		}
		isPossible (source, destination);
	}

	bool isPossible(Block source, Block destination) {
		Queue<Block> queue = new Queue<Block> ();
		queue.Enqueue (source);
		while (queue.Count > 0) {
			Block block = queue.Dequeue();
		}
		return true;
	}

	// Use this for initialization
	void Start () {
		TileContainer.Instance.createMap ();
		createStage ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
