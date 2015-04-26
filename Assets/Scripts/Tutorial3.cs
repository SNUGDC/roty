using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial3 : MonoBehaviour {

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
			Application.LoadLevel("gameplay");
		});
		
		possible.onClick.AddListener(() => {
			textForButtonClick.color = new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,0.0f);
			clickPossible = true;
		});
		impossible.onClick.AddListener(() => {
			clickPossible = false;
			clickImpossible = true;
			textForButtonClick.color = new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,0.0f);
			timechecker = Time.time;
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
			textForButtonClick.text = "Try Again!";
		}
		
		
		if (clickImpossible){
			otherText.text = " ";
			textForButtonClick.color = Color.Lerp (textForButtonClick.color, new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			textForButtonClick.text = "Great!!!";
			possible.transform.position = new Vector3(10000f,10000f,1000f);
			impossible.transform.position = new Vector3(10000f,10000f,1000f);
			gotonext = true;
		}
		
		if (gotonext && Time.time > timechecker + 1) {
			next.enabled = true;
			nexttext.color = Color.Lerp (nexttext.color, new Color(nexttext.color.r,nexttext.color.g,nexttext.color.b,1.0f), fadeinSpeed*Time.deltaTime);
		}
	}
	
}