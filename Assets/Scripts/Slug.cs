﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Slug : MonoBehaviour {
	enum focusStates { DEFAULT, FOCUS, DISABLED };

	public string[] chars;
	public float displacement;
	public int id;
	public int displayChar;
	public Text textbox;
	public int focus = 0;
	//public int sector;
	public string activeChar;
	public Color defaultColor = new Color(160/255.0f, 160/255.0f, 160/255.0f);
	public Color fadedColor = new Color(160/255.0f, 160/255.0f, 160/255.0f, 175/255.0f);
	public Color highlightedColor = new Color(80/255.0f, 80/255.0f, 80/255.0f);
	public int lefthandSector;
	public int righthandSector;
	// Use this for initialization
	void Start () {


	}

	public void slugMaker(string[] chars, float x, float y, int id, int left, int right) {
		this.chars = chars;
		this.id = id;
		this.lefthandSector = left;
		this.righthandSector = right;
		displayChar = 0;
		activeChar = chars [displayChar];
		textbox.transform.position = Camera.main.WorldToScreenPoint(new Vector2 (x, y));
		textbox.text = chars [0];
		textbox.alignment = TextAnchor.MiddleCenter;
	}

	void drawSlug() {

	}
	
	//this seems redundant
	public void setChar(int i) {
		if (i != displayChar) {
			textbox.text = chars [i];
			activeChar = chars[i];
			displayChar = i;
		}
	}

	public void setFocus(int focus) {
		if (this.focus != focus) {
			Shadow shadow = textbox.GetComponent<Shadow>();
			Outline outline = textbox.GetComponent<Outline>();
			switch (focus) {
			case (int) focusStates.DEFAULT :
				textbox.color = defaultColor;
				outline.enabled = false;
				shadow.enabled = false;
				break;
			case (int) focusStates.DISABLED :
				textbox.color = fadedColor;
				outline.enabled = false;
				shadow.enabled = false;
				break;
			case (int) focusStates.FOCUS :
				textbox.color = highlightedColor;
				outline.enabled = true;
				shadow.enabled = true;
				break;
			default:
				break;
			}
			this.focus = focus;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
	
	
}
