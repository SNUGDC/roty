using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial2 : MonoBehaviour {

	public Text stageText;
	public Text otherText;
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
	private int i = 2;

	// Use this for initialization
	void Start () {
		next.onClick.AddListener(() => {
			Application.LoadLevel("tutorial3");
		});

		possible.onClick.AddListener(() => {
			textForButtonClick.color = new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,0.0f);
			clickImpossible = false;
			clickPossible = true;
			timechecker=Time.time;
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
			otherText.color = Color.Lerp (otherText.color, new Color (otherText.color.r, otherText.color.g, otherText.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
		}
		if (Time.time > timechecker + 2) {
			srForColors = goal.GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer sr in srForColors) {
				sr.color = Color.Lerp (sr.color, new Color (sr.color.r, sr.color.g, sr.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
			}
		}
		if (Time.time > timechecker + 2.5f) {
			srForColors = blocks.GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer sr in srForColors) {
				sr.color = Color.Lerp (sr.color, new Color (sr.color.r, sr.color.g, sr.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
			}
		}
		if (Time.time > timechecker + 3) {
			possible.enabled = true;
			impossible.enabled = true;
			possibletext.color = Color.Lerp (possibletext.color, new Color (possibletext.color.r, possibletext.color.g, possibletext.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
			impossibletext.color = Color.Lerp (impossibletext.color, new Color (impossibletext.color.r, impossibletext.color.g, impossibletext.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
		}
	}

	// Update is called once per frame
	void Update () {

		TutorialAppears ();

		if (clickPossible){
			otherText.text = " ";
			textForButtonClick.color = Color.Lerp (textForButtonClick.color, new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			textForButtonClick.text = "Great!!!";
			possible.transform.position = new Vector3(10000f,10000f,1000f);
			impossible.transform.position = new Vector3(10000f,10000f,1000f);
			if(Time.time > timechecker + 1){
				blocks.transform.rotation = Quaternion.Slerp(blocks.transform.rotation, Quaternion.Euler(new Vector3(0.0f,0.0f,90.0f)),fadeinSpeed*Time.deltaTime*2);
				if(Time.time > timechecker + 2.3f){
					gotonext=true;
				}
			}
			if(Time.time > timechecker + i){
				blocks.transform.position = new Vector3(0,1,0);
				blocks.transform.Find("1").localPosition = new Vector3(0,1,-2);
				blocks.transform.Find("2").localPosition = new Vector3(0,0,-2);
				blocks.transform.Find("3").localPosition = new Vector3(-1,1,-2);
				blocks.transform.rotation = Quaternion.Euler (new Vector3(0,0,0));
				timechecker = Time.time;
				i = 9999;
			}
		}


		if (clickImpossible){
			otherText.text = " ";
			textForButtonClick.color = Color.Lerp (textForButtonClick.color, new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			textForButtonClick.text = "Try again!";
		}

		if (gotonext) {
			next.enabled = true;
			nexttext.color = Color.Lerp (nexttext.color, new Color(nexttext.color.r,nexttext.color.g,nexttext.color.b,1.0f), fadeinSpeed*Time.deltaTime);
		}
	}

}
