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
	private bool clickPossible = false;
	private bool clickImossible = false;
	private SpriteRenderer[] srForColors;
	private float fadeinSpeed = 3.0f;


	// Use this for initialization
	void Start () {
		goal.SetActive(false);
		blocks.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > 1){
			goal.SetActive(true);
			goalText.color = Color.Lerp(goalText.color, new Color(goalText.color.r,goalText.color.g,goalText.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			srForColors = goal.GetComponentsInChildren<SpriteRenderer>();
			foreach (SpriteRenderer sr in srForColors){
				sr.color = Color.Lerp (sr.color, new Color(sr.color.r,sr.color.g,sr.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			}
		}

		if (Time.time > 3){
			blocks.SetActive(true);
			blocksText.color = Color.Lerp(blocksText.color, new Color(blocksText.color.r,blocksText.color.g,blocksText.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			srForColors = blocks.GetComponentsInChildren<SpriteRenderer>();
			foreach (SpriteRenderer sr in srForColors){
				sr.color = Color.Lerp (sr.color, new Color(sr.color.r,sr.color.g,sr.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			}
		}
		if (Time.time >5){
			otherText.color = Color.Lerp(otherText.color, new Color(otherText.color.r,otherText.color.g,otherText.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			//buttons "possible" and "impossible" should appear from here 
		}
		if (clickPossible){
			blocksText.text = " ";
			goalText.text = " ";
			otherText.text = " ";
			great.color = Color.Lerp (great.color, new Color(great.color.r,great.color.g,great.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			great.text = "Great";
			if(great.color.a > 0.9){
				blocks.transform.rotation = Quaternion.Slerp(blocks.transform.rotation, Quaternion.Euler(new Vector3(0.0f,0.0f,-90.0f)),fadeinSpeed*Time.deltaTime);
			}
		}
		if (clickImossible){
			blocksText.text = " ";
			goalText.text = " ";
			otherText.text = " ";
			great.color = Color.Lerp (great.color, new Color(great.color.r,great.color.g,great.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			great.text = "Try again!";
		}

		if (true) {
			//After clicking possible, both buttons should disappear and new button "Next" should appear
		}


		possible.onClick.AddListener(() => {
			clickImossible = false;
			great.color = new Color(great.color.r,great.color.g,great.color.b,0.0f);
			clickPossible = true;
		});
		impossible.onClick.AddListener(() => {
			clickImossible = true;
			great.color = new Color(great.color.r,great.color.g,great.color.b,0.0f);
			clickPossible = false;
		});
	}

}
