using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	
	char[] chars;
	float displacement;
	int id;
	int displayChar;
	// Use this for initialization
	void Start (char[] chars, float displacement, int id) {
		this.chars = chars;
		this.displacement = displacement;
		this.id = id;
		displayChar = 0;
	}
	
	//this seems redundant
	void changeChar(int i) {
		displayChar = i;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
}
