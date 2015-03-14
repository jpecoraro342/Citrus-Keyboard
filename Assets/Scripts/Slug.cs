using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Slug : MonoBehaviour {
	
	public string[] chars;
	public float displacement;
	public int id;
	public int displayChar;
	public Text textbox;
	public int focus = 0;

	// Use this for initialization
	void Start () {


	}

	public void slugMaker(string[] chars, float x, float y, int id) {
		this.chars = chars;
		this.id = id;
		displayChar = 0;

		textbox.transform.position = Camera.main.WorldToScreenPoint(new Vector2 (x, y));
		textbox.text = chars [0];
	}

	void drawSlug() {

	}
	
	//this seems redundant
	void changeChar(int i) {
		displayChar = i;
	}

	public void setFocus(int sector) {

	}

	// Update is called once per frame
	void Update () {
		
	}
	
	
}
