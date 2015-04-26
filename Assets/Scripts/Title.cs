using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Title : MonoBehaviour {

	public Button tutorial;
	public Button gameStart;

	// Use this for initialization
	void Start () {
		tutorial.onClick.AddListener(() => {
			Application.LoadLevel("tutorial1");
		});
		
		gameStart.onClick.AddListener(() => {
			Application.LoadLevel("gameplay");
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
