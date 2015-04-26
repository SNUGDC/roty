using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using ExtensionMethods;

public class StageContainer : MonoBehaviour {
	private int count = 0;
	private bool isPossible;

	public void gotToNextStage() {
		count++;
		(GetComponent (typeof(Text)) as Text).text = "Stage " + count.ToString ();
	}
	
	void createStage() {
		Block destination = BlockFactory.generate (Depth.DESTINATION_DEPTH);
		Block source = BlockFactory.generate (Depth.SOURCE_DEPTH, destination.polymino);
		destination.dye(new Color(0.969f, 0.737f, 0.816f));
		source.dye(new Color(0.918f, 0.263f, 0.482f));
		
		foreach (var count in Enumerable.Range(0, Random.Range(0, 3))) {
			Point2 point = source.tiles.Sample().First().point;
			try {
				source.moveTiles(source.rotateQuarter(point));
			} catch(OutOfBoundException) {
				continue;
			}
		}
		//Debug.Log ("Validate Start");
		isPossible = validate(source, destination);
		Debug.Log (isPossible);
	}
	
	bool validate(Block source, Block destination) {
		Queue<Block> Q = new Queue<Block> ();
		HashSet<Block> H = new HashSet<Block> ();
		Q.Enqueue (source);
		H.Add (source);
		while (Q.Count > 0) {
			Block vertice = Q.Dequeue();
			if (vertice.Equals(destination)) {
				return true;
			}
			// Traverse
			foreach (var tile in vertice.tiles) {
				var next = vertice;
				foreach (var i in Enumerable.Range(0, 3)) {
					try {
						next = vertice.rotateQuarter(tile.point);
						if (!H.Contains(next)) {
							Q.Enqueue(next);
							H.Add(next);
						}
					} catch (OutOfBoundException) {
						continue;
					}
				}
			}
		}
		return false;
	}
	
	// Use this for initialization
	void Start () {
		StartLevel ();
	}
	
	void StartLevel() {
		gotToNextStage ();
		TileContainer.Instance.createMap ();
		createStage ();
		
	}
	
	public void onPressPossible() {
		if (isPossible) {
			Debug.Log ("You are right!");
		} else {
			Debug.Log ("You are wrong!");
		}
		StartLevel ();
	}
	
	public void onPressImpossible() {
		if (!isPossible) {
			Debug.Log ("You are right!");
		} else {
			Debug.Log ("You are wrong!");
		}
		StartLevel ();
	}
}
