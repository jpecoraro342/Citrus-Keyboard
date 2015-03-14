using UnityEngine;
using System.Collections;


public class Pi : MonoBehaviour {
	float radius = 4;
	int numSlugs = 30;
	int numSectors = 6;
	public GameObject cylinder;
	public LineRenderer lr;
	//Slug[] slug;

	// Use this for initialization
	void Start () {
		drawPi ();
		makeSlugs ();
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
		//for (int i = 0; i < this.numSlugs; i++) {
			//slug[i] = new Slug();
		//}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
