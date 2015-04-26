using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial1 : MonoBehaviour {

	public Text stageText;
	public Text otherText;
	public Text goalText;
	public Text blocksText;
	public Text textForButtonClick;
	public Text possibletext;
	public Text impossibletext;
	public Text nexttext;
	public Button possible;
	public Button impossible;
	public Button next;
	public GameObject goal;
	public GameObject blocks;
	private bool clickPossible = false;
	private bool clickImpossible = false;
	private SpriteRenderer[] srForColors;
	private float fadeinSpeed = 3.0f;
	private bool gotonext = false;
	private float timechecker;

	// Use this for initialization
	void Start () {
		next.enabled = false;
		possible.enabled = false;
		impossible.enabled = false;

		next.onClick.AddListener(() => {
			Application.LoadLevel("tutorial2");
		});

		possible.onClick.AddListener(() => {
			textForButtonClick.color = new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,0.0f);
			clickPossible = true;
			timechecker = Time.time;
		});

		impossible.onClick.AddListener(() => {
			clickImpossible = true;
			textForButtonClick.color = new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,0.0f);
		});
	
		timechecker = Time.time;
	}

	void TutorialAppears ()
	{
		if (Time.time > timechecker + 1) {
			goalText.color = Color.Lerp (goalText.color, new Color (goalText.color.r, goalText.color.g, goalText.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
			srForColors = goal.GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer sr in srForColors) {
				sr.color = Color.Lerp (sr.color, new Color (sr.color.r, sr.color.g, sr.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
			}
		}
		if (Time.time > timechecker + 3) {
			blocksText.color = Color.Lerp (blocksText.color, new Color (blocksText.color.r, blocksText.color.g, blocksText.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
			srForColors = blocks.GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer sr in srForColors) {
				sr.color = Color.Lerp (sr.color, new Color (sr.color.r, sr.color.g, sr.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
			}
		}
		if (Time.time > timechecker + 5) {
			otherText.color = Color.Lerp (otherText.color, new Color (otherText.color.r, otherText.color.g, otherText.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
		}
		if (Time.time > timechecker + 6) {
			possible.enabled = true;
			impossible.enabled = true;
			possibletext.color = Color.Lerp (possibletext.color, new Color (possibletext.color.r, possibletext.color.g, possibletext.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
			impossibletext.color = Color.Lerp (impossibletext.color, new Color (impossibletext.color.r, impossibletext.color.g, impossibletext.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
		}
	}

	// Update is called once per frame
	void Update () {

		TutorialAppears ();

		if (clickImpossible){
			blocksText.text = " ";
			goalText.text = " ";
			otherText.text = " ";
			textForButtonClick.color = Color.Lerp (textForButtonClick.color, new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			textForButtonClick.text = "Try again!";
		}

		if (clickPossible){
			blocksText.text = " ";
			goalText.text = " ";
			otherText.text = " ";
			textForButtonClick.color = Color.Lerp (textForButtonClick.color, new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			textForButtonClick.text = "Great!!!";
			
			possible.transform.position = new Vector3(10000f,10000f,1000f);
			impossible.transform.position = new Vector3(10000f,10000f,1000f);
			
			if(Time.time > timechecker + 1){
				blocks.transform.rotation = Quaternion.Slerp(blocks.transform.rotation, Quaternion.Euler(new Vector3(0.0f,0.0f,-90.0f)),fadeinSpeed*Time.deltaTime);
				gotonext = true;
			}
		}

		if (gotonext) {
			next.enabled = true;
			nexttext.color = Color.Lerp (nexttext.color, new Color(nexttext.color.r,nexttext.color.g,nexttext.color.b,1.0f), fadeinSpeed*Time.deltaTime);
		}

	}

}
