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
	private bool initiate = true;
	private int stagenumber = 1;
	private bool test = false;

	// Use this for initialization
	void Start () {
	}

/*	void DifferentInitializationByStage (int i) {
		stageText.text = "Tutorial " + i;

		textForButtonClick.text = "";
		blocksText.text = "blocks";
		goalText.text = "goal";
		otherText.text = "Can the               be rotated into the          ?";
		goal.transform.position = new Vector3(0,0,0);
		//goal.GetComponentsInChildren<transform> ();
	}

	void InitializeBoolean ()
	{
		gotonext = false;
		possible.enabled = false;
		impossible.enabled = false;
		next.enabled = false;
		clickPossible = false;
		clickImpossible = false;
	}

	void InitializeTransparency ()
	{
		goalText.color = new Color (goalText.color.r, goalText.color.g, goalText.color.b, 0.0f);
		blocksText.color = new Color (blocksText.color.r, blocksText.color.g, blocksText.color.b, 0.0f);
		otherText.color = new Color (otherText.color.r, otherText.color.g, otherText.color.b, 0.0f);
		possibletext.color = new Color (possibletext.color.r, possibletext.color.g, possibletext.color.b, 0.0f);
		impossibletext.color = new Color (impossibletext.color.r, impossibletext.color.g, impossibletext.color.b, 0.0f);
		nexttext.color = new Color (nexttext.color.r, nexttext.color.g, nexttext.color.b, 0.0f);
		textForButtonClick.color = new Color (textForButtonClick.color.r, textForButtonClick.color.g, textForButtonClick.color.b, 0.0f);
		srForColors = goal.GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer sr in srForColors) {
			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, 0.0f);
		}
		srForColors = blocks.GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer sr in srForColors) {
			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, 0.0f);
		}
	}
*/
	void Initializer () {
		//DifferentInitializationByStage(stagenumber);

		timechecker = Time.time;

		//InitializeBoolean ();

		//InitializeTransparency ();

		//possible.transform.localPosition = new Vector3(-170f,-500f,0f);
		//impossible.transform.localPosition = new Vector3(170f,-500f,0f);

		//blocks.transform.rotation = Quaternion.Euler(new Vector3(0.0f,0.0f,0.0f));

		//Debug.Log ("initiate!!");
		//stagenumber++;

		initiate = false;
	}

	void TutorialAppears ()
	{
		if (Time.time > timechecker + 1) {
			srForColors = goal.GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer sr in srForColors) {
				sr.color = Color.Lerp (sr.color, new Color (sr.color.r, sr.color.g, sr.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
			}
		}
		if (Time.time > timechecker + 3) {
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
		if (initiate){
			Initializer();
		}

		TutorialAppears ();

		if (clickPossible){
			otherText.text = " ";
			textForButtonClick.color = Color.Lerp (textForButtonClick.color, new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,1.0f), fadeinSpeed*Time.deltaTime);
			textForButtonClick.text = "Great!!!";
			possible.transform.position = new Vector3(1000f,1000f,1000f);
			impossible.transform.position = new Vector3(1000f,1000f,1000f);
			if(textForButtonClick.color.a > 0.9){
				blocks.transform.rotation = Quaternion.Slerp(blocks.transform.rotation, Quaternion.Euler(new Vector3(0.0f,0.0f,-90.0f)),fadeinSpeed*Time.deltaTime);
				gotonext = true;
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


		possible.onClick.AddListener(() => {
			clickImpossible = false;
			textForButtonClick.color = new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,0.0f);
			clickPossible = true;
		});
		impossible.onClick.AddListener(() => {
			clickImpossible = true;
			textForButtonClick.color = new Color(textForButtonClick.color.r,textForButtonClick.color.g,textForButtonClick.color.b,0.0f);
			clickPossible = false;
		});
		next.onClick.AddListener(() => {
			test = true;
			
		});
		if(test){
			Application.LoadLevel("tutorial3");
			test=false;
		}
	}

}
