using UnityEngine;
using System.Collections;


public class Pi : MonoBehaviour {
	float radius = 4;
	int numSlugs = 30;
	int numSectors = 6;

	public GameObject cylinder;
	public LineRenderer lr;
	Slug[] slugs;

	public GameObject SlugClone;
	string[][] charset = new string[][] {
		new string[] {"A","a","0"}, //0
		new string[] {"B","b","1"}, //1
		new string[] {"C","c","2"}, //2
		new string[] {"D","d","3"}, //3
		new string[] {"E","e","4"}, //4
		new string[] {"F","f","5"}, //5
		new string[] {"G","g","6"}, //6
		new string[] {"H","h","7"}, //7
		new string[] {"I","i","8"}, //8
		new string[] {"J","j","9"}, //9
		new string[] {"K","k","."}, //10
		new string[] {"L","l",","}, //11
		new string[] {"M","m","?"}, //12
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
		new string[] {" "," "," "},
	};

	// Use this for initialization
	void Start () {
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

		cylinder.transform.rotation = Quaternion.AngleAxis (90, Vector3.left);
		cylinder.transform.position = new Vector3 (0, -0.03f, 0.1f);
		cylinder.transform.localScale = new Vector3 (radius * 2 + 0.04f, 0.1f, radius * 2 + 0.04f);
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
		for (int i = 0; i < charset.Length; i ++) {

			GameObject slug = (GameObject)Instantiate (SlugClone, Vector3.zero, Quaternion.identity);
			slug.transform.parent = gameObject.transform;
			slug.transform.localPosition = Vector3.zero;

			Slug slugScript = slug.GetComponent<Slug> ();
			slugScript.slugMaker (new string[] {"A", "a"}, 100.0f, 100.0f, 0);
			slugs[i] = slugScript;
		}

	}

	//sector is the sector selected on the left thumbstick
	void setFocus(int sector) {
		for (int i = 0; i < charset.Length; i++) {
			if (i % 6 != 0) {
				slugs[i].setFocus(1); //1 is grayed
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
