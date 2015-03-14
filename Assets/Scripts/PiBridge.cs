using UnityEngine;
using System.Collections;

public class PiBridge : MonoBehaviour {

	int lastLeftSector;
	int lastRightSector;
	public Pi pi; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	int angleToZone(int angle, int numZones) {
		return (int)(angle + 360/(numZones*2))*numZones/360;
	}

	public void changeKeyboard(KeyboardType keyboard) {
		pi.setKeyboard (keyboard);
	}

	//left thumbstick was released before the right was
	public void cancelInput() {
		//might need to reset the lastSectors...
		pi.resetFocus ();
		//....
	}

	//right thumbstick released, add character to textbox
	public void characterSelect() {
		pi.getChar (lastLeftSector, lastRightSector);

		//TODO: write to textbox

		pi.resetFocus ();
	}

	public void updateAngles(float left, float right) {
		if ((int)left != lastLeftSector) {
			lastLeftSector = (int)left;
			pi.setFocusDisabled((int)left);
		}
		if ((int)right != lastRightSector) {
			lastRightSector = (int)right;
			pi.setFocusActive((int)right);
		}
	}
}
