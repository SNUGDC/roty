using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	public Text otherText;
	public Text goalText;
	public Text blocksText;
	public Text great;
	public Button possible;
	public Button impossible;
	public GameObject goal;
	public GameObject blocks;


	// Use this for initialization
	void Start () {
		goal.SetActive(false);
		blocks.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > 1){
			goal.SetActive(true);
			goalText.color = new Color(goalText.color.r,goalText.color.g,goalText.color.b,1.0f);
		}
		if (Time.time > 2){
			blocks.SetActive(true);
			blocksText.color = new Color(blocksText.color.r,blocksText.color.g,blocksText.color.b,1.0f);
		}
		if (Time.time >3){
			otherText.color = new Color(otherText.color.r,otherText.color.g,otherText.color.b,1.0f);
		}
		possible.onClick.AddListener(() => {
			blocksText.text = " ";
			goalText.text = " ";
			otherText.text = " ";
			great.color = new Color(great.color.r,great.color.g,great.color.b,1.0f);
			great.text = "Great!";
		});
		impossible.onClick.AddListener(() => {
			great.text = "Try again!";
		});
	}

}
