using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class TimeContainer : MonoBehaviour {

	#region singleton
	private static TimeContainer _instance;
	public static TimeContainer Instance {
		get {
			if (!_instance) {
				_instance = GameObject.FindObjectOfType(typeof(TimeContainer)) as TimeContainer;
				if (!_instance) {
					GameObject container = new GameObject();
					container.name = "TimeContainer";
					_instance = container.AddComponent(typeof(TimeContainer)) as TimeContainer;
				}
			}
			return _instance;
		}
	}
	#endregion

	public float limit = 3.0f;
	public float due = 3.0f;
	private Vector2 size;
	// Use this for initialization
	void Start () {
		size = GetComponent<RectTransform> ().sizeDelta;
	}

	public void StartLevel() {
		due = limit;
		GetComponent<RectTransform> ().sizeDelta = size;
	}
	// Update is called once per frame
	void Update () {
		due -= Time.deltaTime;
		if (due < 0) {
			StageContainer.Instance.EndLevel();
		}
		GetComponent<RectTransform> ().sizeDelta = new Vector2 (
			due / limit * size.x, 
			size.y
		);
	}
}
