using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Slug : MonoBehaviour {
	
	public string[] chars;
	public float displacement;
	public int id;
	public int displayChar;
	public Text textbox;

	// Use this for initialization
	void Start () {


	}

	public void slugMaker(string[] chars, float x, float y, int id) {
		this.chars = chars;
		this.id = id;
		displayChar = 0;

		textbox.rectTransform.anchoredPosition = new Vector2 (x, y);
		textbox.text = chars [0];
	}

	void drawSlug() {

	}
	
	//this seems redundant
	void changeChar(int i) {
		displayChar = i;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
}
