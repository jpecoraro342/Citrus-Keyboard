using UnityEngine;
using System.Collections;


public class Pi : MonoBehaviour {
	enum focusStates { DEFAULT, FOCUS, SELECTED, DISABLED };
	float radius = 4;
	int numSlugs = 30;
	public int numSectors = 6;
	
	public LineRenderer lr;
	public Slug[] slugs;


	public GameObject SlugClone;
	string[][] charset = new string[][] {
		new string[] {"A","a","0"}, //0 0 
		new string[] {"B","b","1"}, //1 1
		new string[] {"C","c","2"}, //2 2
		new string[] {"D","d","3"}, //3 3
		new string[] {"E","e","4"}, //4 4
		new string[] {"F","f","5"}, //5 0
		new string[] {"G","g","6"}, //6 1
		new string[] {"H","h","7"}, //7 2
		new string[] {"I","i","8"}, //8 3 
		new string[] {"J","j","9"}, //9 4
		new string[] {"K","k","."}, //10 0
		new string[] {"L","l",","}, //11 1
		new string[] {"M","m","?"}, //12 2
		new string[] {"N","n","!"},
		new string[] {"O","o","\'"},
		new string[] {"P","p","\""},
		new string[] {"Q","q","/"},
		new string[] {"R","r","@"},
		new string[] {"S","s","#"},
		new string[] {"T","t","$"},
		new string[] {"U","u","%"},
		new string[] {"V","v","&"},
		new string[] {"W","w","*"},
		new string[] {"X","x","("},
		new string[] {"Y","y",")"},
		new string[] {"Z","z","-"},
		new string[] {"\u2423","\u2423","\u2423"},
		new string[] {"\u2423","\u2423","\u2423"},
		new string[] {"\u2423","\u2423","\u2423"},
		new string[] {"\u2423","\u2423","\u2423"}
		//new string[] {" "," "," "},
		//new string[] {" "," "," "},
		//new string[] {" "," "," "}
	};

	// Use this for initialization
	void Start () {
		slugs = new Slug[numSlugs];
		drawPi ();
		makeSlugs ();
		//You need this in here because the canvas was doing some weird thing resetting itself
		gameObject.transform.localPosition = new Vector3(0, 0, 0);
	}

	void makePi(int numSectors, int numSlugs, float radius) {
		this.numSectors = numSectors;
		this.numSlugs = numSlugs;
		this.radius = radius;
	}

	void drawPi() {
		lr.SetVertexCount (460+2*numSectors);
		lr.SetWidth (0.1f,0.1f);
		float gray = 0.9f;
		lr.SetColors (new Color(gray,gray,gray,1), new Color(gray,gray,gray,1));

		int sectorSize = 360/numSectors;
		float x, y, z = 0f, angle = sectorSize/-2;
		for (int i = 0; i < 361; i++) {
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;
			lr.SetPosition (i+1,new Vector3(x,y,z) );
			angle += 1;
		}

		for (int i = 0, startAngle = sectorSize/2; i < 360/sectorSize -1; i++, startAngle += sectorSize) {
			drawSector (363+(i*2),0,0,startAngle);
		}
	}

	void drawSector(int index,int centerX, int centerY, int startDegree) {
		LineRenderer lr = gameObject.GetComponent<LineRenderer> ();
		lr.SetPosition (index++, new Vector3 (centerX, centerY, 0f));
		float x, y, z = 0f, angle = startDegree;
		x = Mathf.Sin (Mathf.Deg2Rad * startDegree) * radius;
		y = Mathf.Cos (Mathf.Deg2Rad * startDegree) * radius;
		lr.SetPosition (index++,new Vector3(x,y,z) );

	}

	void makeSlugs() {
		float sectorLength = 2*3.1415f*3;
		float extraPadding = 1;
		Debug.Log ("Sector length: " + sectorLength);
		float textWidth = 1f;
		Debug.Log ("text Width: " + textWidth);
		float degInSector = 360 / numSectors;
		float padding = sectorLength - textWidth*(numSlugs/numSectors);
		float paddingDeg = ((padding/sectorLength) * degInSector)/((numSlugs/numSectors) + 1) + extraPadding;
		float textDeg = (textWidth/sectorLength)* degInSector;
		float insertTextAtDeg = degInSector / -2 + textDeg + paddingDeg - (extraPadding*((numSlugs/numSectors)))/2;
		Debug.Log("padding Deg: " + paddingDeg);
		Debug.Log("insertTextAtDeg: " + insertTextAtDeg);
		for (int i = 0, rightSector = 0, leftSector = 0; i < charset.Length; i ++, leftSector++) {
			if (leftSector >= numSlugs/numSectors) {
				rightSector++;
				leftSector = 0;
				//this gives sectors padding
				insertTextAtDeg = (degInSector / -2 + textDeg + paddingDeg - (extraPadding*((numSlugs/numSectors)))/2) + rightSector*degInSector;
				Debug.Log("New sector and insert Text at deg: "+ insertTextAtDeg);
			}
			GameObject slug = (GameObject)Instantiate (SlugClone, Vector3.zero, Quaternion.identity);
			slug.transform.parent = gameObject.transform;
			slug.transform.localPosition = Vector3.zero;

			insertTextAtDeg -= 0.7f;
			float x = Mathf.Sin (Mathf.Deg2Rad * insertTextAtDeg) * (radius + 0.6f);
			float y = Mathf.Cos (Mathf.Deg2Rad * insertTextAtDeg) * (radius + 0.6f);
			insertTextAtDeg += paddingDeg + textDeg;

			Slug slugScript = slug.GetComponent<Slug> ();
			slugScript.slugMaker (charset[i], x, y, i, leftSector, rightSector);
			slugs[i] = slugScript;
		}

	}

	//todo: move these terrible crap focus functions to pibridge
	//sector is the sector selected on the <LEFT> thumbstick
	public void setFocusDisabled(int sector) {
		for (int i = 0; i < charset.Length; i++) {
			if (slugs[i].lefthandSector != sector) {
				slugs[i].setFocus((int)focusStates.DISABLED); 
			} else {
				slugs[i].setFocus ((int)focusStates.FOCUS);
			}
		}
	}

	//sector is the sector selected by the <RIGHT> thumbstick
	//sector is the sector selected by the <RIGHT> thumbstick
	//sector is the sector selected by the <RIGHT> thumbstick
	public void setFocusActive(int leftSector, int rightSector) {
		for (int i = 0; i < charset.Length; i++) {
			if (slugs[i].righthandSector == rightSector && slugs[i].lefthandSector == leftSector) {
				slugs[i].setFocus((int)focusStates.SELECTED); 
			} else if (slugs[i].focus != (int) focusStates.DISABLED) {
				slugs[i].setFocus((int)focusStates.FOCUS);
			}

		}
	}

	public void resetFocus() {
		for (int i = 0; i < charset.Length; i++) {
			slugs[i].setFocus((int)focusStates.DEFAULT); 
		}
	}

	public void setKeyboard(KeyboardType keyboard) {
		for (int i = 0; i < charset.Length; i++) {
			slugs[i].setChar((int) keyboard);
		}
	}

	public string getChar(int left, int right) {
		for (int i = 0; i < charset.Length; i++) {
			if (slugs[i].lefthandSector == left && slugs[i].righthandSector == right) {
				string activeChar = slugs[i].activeChar;
				if (activeChar == "\u2423") {
					activeChar = " ";
				}
				return activeChar;
			} 
		}
		return "WTF";

	}
	
	// Update is called once per frame
	void Update () {

	}
}
