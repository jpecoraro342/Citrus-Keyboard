using UnityEngine;
using System.Collections;

public class PiBridge : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	int angleToZone(float angle, int numZones) {
		return (int)(angle + 360/(numZones*2))*numZones/360;
	}
	
}
