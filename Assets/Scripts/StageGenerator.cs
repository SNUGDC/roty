using UnityEngine;
using System.Collections;

public class StageGenerator : MonoBehaviour {
	public Tile[] source;
	public Tile[] destination;

	void dye(Tile[] tiles, Color color) {
		foreach (var tile in tiles) {
			tile.gameObject.GetComponent<Renderer>().material.color = color;
		}
	}
	void createStage() {
		destination = BlockFactory.generate ();
		Debug.Log (destination);
		dye (destination, Color.red);
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
