using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	public Text otherText;
	public Text textForButtonClick;
	public Text nexttext;
	public Button next;
	public GameObject blocks;
	private SpriteRenderer[] srForColors;
	private float fadeinSpeed = 3.0f;
	private bool gotonext = false;
	private float timechecker;
	private float rotationleft = 360;
	private float rotationspeed = 500;
	private int rotationcount = 1;
	
	// Use this for initialization
	void Start () {
		next.onClick.AddListener(() => {
			Application.LoadLevel("tutorial2");
		});
		
		timechecker = Time.time;
	}
	
	
	void TutorialAppears ()
	{
		if (Time.time > timechecker + 1) {
			otherText.color = Color.Lerp (otherText.color, new Color (otherText.color.r, otherText.color.g, otherText.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
		}
		if (Time.time > timechecker + 2) {
			srForColors = blocks.GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer sr in srForColors) {
				sr.color = Color.Lerp (sr.color, new Color (sr.color.r, sr.color.g, sr.color.b, 1.0f), fadeinSpeed * Time.deltaTime);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		TutorialAppears ();

		if(Time.time > timechecker + 2 & rotationcount == 1){
			StartCoroutine(Rotation());
		}
		if(rotationleft == 0 & rotationcount == 2){
			Debug.Log ("coroutine"+rotationcount);
			blocks.transform.position = new Vector3(0,2,0);
			blocks.transform.Find("1").localPosition = new Vector3(0,0,-2);
			blocks.transform.Find("2").localPosition = new Vector3(0,-1,-2);
			blocks.transform.Find("3").localPosition = new Vector3(1,-1,-2);
			rotationleft = 360;
			StartCoroutine(Rotation());
		}
		if(rotationleft == 0 & rotationcount == 3){
			Debug.Log ("coroutine"+rotationcount);
			blocks.transform.position = new Vector3(1,1,0);
			blocks.transform.Find("1").localPosition = new Vector3(0,0,-2);
			blocks.transform.Find("2").localPosition = new Vector3(-1,1,-2);
			blocks.transform.Find("3").localPosition = new Vector3(-1,0,-2);
			rotationleft = 360;
			StartCoroutine(Rotation());
		}

		if(Time.time > timechecker + 7){
				gotonext=true;
		}

		if (gotonext) {
			next.enabled = true;
			nexttext.color = Color.Lerp (nexttext.color, new Color(nexttext.color.r,nexttext.color.g,nexttext.color.b,1.0f), fadeinSpeed*Time.deltaTime);
		}
	}

	IEnumerator Rotation() {

		yield return new WaitForSeconds(1);

		if(rotationleft > rotationspeed*Time.deltaTime){
			rotationleft -= rotationspeed*Time.deltaTime;
		}

		else{
			rotationleft = 0;
			rotationcount ++;
			Debug.Log("good");
			blocks.transform.Rotate (Vector3.zero);
			yield break;
		}

		blocks.transform.Rotate(0,0,rotationspeed*Time.deltaTime);
	}
	
}

