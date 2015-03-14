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
	}

	void drawPi() {
		lr.SetVertexCount (460+2*numSectors);
		lr.SetWidth (0.1f,0.1f);

		int sectorSize = 360/numSectors;
		for (int i = 0, startAngle = 0; i < 360/sectorSize; i++, startAngle += sectorSize) {
			drawSector ((sectorSize+2)*i,0,0,startAngle,startAngle+sectorSize);
		}
	}

	void drawSector(int index,int centerX, int centerY, int startDegree, int endDegree) {
		int points = endDegree - startDegree;
		lr.SetPosition (index++, new Vector3 (centerX, centerY, 0f));

		float x, y, z = 0f, angle = startDegree;
		for (int i = 0; i < (points + 1); i++) {
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * 4;
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * 4;
			lr.SetPosition (index++,new Vector3(x,y,z) );
			angle += 1;
		}

		lr.SetPosition (index++, new Vector3 (centerX, centerY, 0f));
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
