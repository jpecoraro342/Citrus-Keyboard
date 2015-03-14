using UnityEngine;
using System.Collections;


public class Pi : MonoBehaviour {
	//int radius;
	//int numSlugs;
	int numSectors;
	//Slug[] slug;
	LineRenderer lr;

	// Use this for initialization
	void Start () {
		this.numSectors = 6;
		lr = gameObject.AddComponent<LineRenderer> ();
		drawPi ();
		makeSlugs ();
		//Slug s1 = new Slug (new char['A','a','?'],4,0);
		//Slug s2 = new Slug (new char['B','b','?'],4,0);
	}

	void drawPi() {

		lr.SetVertexCount (460+2*numSectors);
		lr.SetWidth (0.1f,0.1f);

		float x, y, z = 0f, angle = 0;
		for (int i = 0; i <= 360; i++) {
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * 4;
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * 4;
			lr.SetPosition (i+1,new Vector3(x,y,z) );
			angle += 1;
		}

		int sectorSize = 360/numSectors;
		for (int i = 0, startAngle = sectorSize; i < 360/sectorSize; i++, startAngle += sectorSize) {
			drawSector (362+(i*2),0,0,startAngle);
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
