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
		//this.slug = new Slug[this.numSlugs];
	}

	void drawPi() {
		//GameObject cylinder = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		//cylinder.transform.rotation = Quaternion.AngleAxis (90, Vector3.left);
		//cylinder.transform.localScale = new Vector3 (3,(float) 0.1,3);
		lr.SetVertexCount (460+2*numSectors);
		lr.SetWidth (0.2f,0.2f);

		int sectorSize = 30;
		for (int i = 0, startAngle = 0; i < 360/30; i++, startAngle += sectorSize) {
			drawSector (31*i,0,0,startAngle,startAngle+sectorSize);
		}
	}

	void drawSector(int index,int centerX, int centerY, int startDegree, int endDegree) {
		int points = endDegree - startDegree;
		lr.SetPosition (index++, new Vector3 (centerX, centerY, 0f));

		float x, y, z = 0f, angle = startDegree;
		for (int i = 0; i < points; i++) {
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
