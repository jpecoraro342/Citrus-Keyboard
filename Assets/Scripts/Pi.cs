using UnityEngine;
using System.Collections;


public class Pi : MonoBehaviour {
	//int radius;
	//int numSlugs;
	int numSectors;
	//Slug[] slug;
	LineRenderer lr;

	public GameObject SlugClone;
	string[,] charset = new string[,] {
		{"A","a","0"},
		{"B","b","1"},
		{"C","c","2"},
		{"D","d","3"},
		{"E","e","4"},
		{"F","f","5"},
		{"G","g","6"},
		{"H","h","7"},
		{"I","i","8"},
		{"J","j","9"},
		{"K","k","."},
		{"L","l",","},
		{"M","m","?"},
		{"N","n","!"},
		{"O","o","\'"},
		{"P","p","\""},
		{"Q","q","/"},
		{"R","r","@"},
		{"S","s","#"},
		{"T","t","$"},
		{"U","u","%"},
		{"V","v","&"},
		{"W","w","*"},
		{"X","x","("},
		{"Y","y",")"},
		{"Z","z","-"},
		{" "," "," "},
	};

	// Use this for initialization
	void Start () {
		this.numSectors = 6;
		lr = gameObject.AddComponent<LineRenderer> ();
		lr.transform.position = new Vector3 (0,4,0);

		drawPi ();
		makeSlugs ();

		//You need this in here because the canvas was doing some weird thing resetting itself
		gameObject.transform.localPosition = new Vector3(0, 0, 0);
	}

	void drawPi() {

		lr.SetVertexCount (460+2*numSectors);
		lr.SetWidth (0.1f,0.1f);
		lr.SetPosition (0, new Vector3 (1, 1, 0));
		lr.SetPosition (1, new Vector3 (1, 2, 0));

		float x, y, z = 0f, angle = 0;
		for (int i = 0; i <= 0; i++) {
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * 4;
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * 4;
			//lr.SetPosition (i+1,new Vector3(x,y,z) );
			angle += 1;
		}

		int sectorSize = 360/numSectors;
		for (int i = 0, startAngle = sectorSize; i < 360/sectorSize; i++, startAngle += sectorSize) {
			//drawSector (362+(i*2),0,0,startAngle);
		}
	}

	void drawSector(int index,int centerX, int centerY, int startDegree) {

		lr.SetPosition (index++, new Vector3 (centerX, centerY, 0f));
		float x, y, z = 0f, angle = startDegree;
		x = Mathf.Sin (Mathf.Deg2Rad * startDegree) * 4;
		y = Mathf.Cos (Mathf.Deg2Rad * startDegree) * 4;
		lr.SetPosition (index++,new Vector3(x,y,z) );

	}

	void makeSlugs() {
		//for (int i = 0; i < this.numSlugs; i++) {
			//slug[i] = new Slug();
		//}
		GameObject slug = (GameObject) Instantiate (SlugClone, Vector3.zero, Quaternion.identity);
		slug.transform.parent = gameObject.transform;

		Slug slugScript = slug.GetComponent<Slug> ();

		slugScript.slugMaker(new string[] {"A", "a"}, 100.0f, 100.0f, 0);

	}
	
	// Update is called once per frame
	void Update () {

	}
}
