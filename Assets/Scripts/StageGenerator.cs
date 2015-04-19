using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using ExtensionMethods;


public class StageGenerator : MonoBehaviour {
	public int transformCount;
	public double mutationRate;

	public Color destinationColor;
	public Color sourceColor;

	void createStage() {
		Block destination = BlockFactory.generate (Depth.DESTINATION_DEPTH);
		Block source = BlockFactory.generate (Depth.SOURCE_DEPTH, destination.polymino);
		destination.dye(new Color(0.969f, 0.737f, 0.816f));
		source.dye(new Color(0.918f, 0.263f, 0.482f));
	}

	Block transitionRandom(Block block) {
		var movement = new Point2 (Random.value > 0.5 ? 1 : -1,
		                           Random.value > 0.5 ? 1 : -1);
		return block.transition (movement);
	}

	Block rotateRandom(Block block) {
		Point2 point = block.tiles.Sample().Single().point;

		return block.rotateQuarter(point);
	}

	// Use this for initialization
	void Start () {
		TileContainer.Instance.createMap ();
		createStage ();
		Debug.Log (destinationColor);
		Debug.Log (sourceColor);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
