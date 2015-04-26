using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using ExtensionMethods;

public class StageContainer : MonoBehaviour {
	#region singleton
	private static StageContainer _instance;
	public static StageContainer Instance {
		get {
			if (!_instance) {
				_instance = GameObject.FindObjectOfType(typeof(StageContainer)) as StageContainer;
				if (!_instance) {
					GameObject container = new GameObject();
					container.name = "StageContainer";
					_instance = container.AddComponent(typeof(StageContainer)) as StageContainer;
				}
			}
			return _instance;
		}
	}
	#endregion

	#region stage
	private int count = 0;
	public void gotToNextStage() {
		count++;
		GetComponent<Text>().text = "Stage " + count.ToString ();
	}
	#endregion

	#region generator
	private bool isPossible;

	private void createStage() {
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
	
	private bool validate(Block source, Block destination) {
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
	#endregion

	#region process
	// Use this for initialization
	void Start () {
		StartLevel ();
	}
	
	public void StartLevel() {
		gotToNextStage ();
		TileContainer.Instance.createMap ();
		createStage ();
		(FindObjectOfType (typeof(TimeContainer)) as TimeContainer).StartLevel ();
	}

	public void OnClickStage() {
		if (Time.timeScale == 0) {
			Time.timeScale = 1.0f;
			StartLevel();
		}
	}

	public void EndLevel() {
		GetComponent<Text>().text = "Try again!";
		Time.timeScale = 0;
		count = 0;
	}

	public void OnPressPossible() {
		if (isPossible) {
			StartLevel ();
		} else {
			EndLevel();
		}
	}
	
	public void OnPressImpossible() {
		if (!isPossible) {
			StartLevel ();
		} else {
			EndLevel();
		}
	}
	#endregion
}
