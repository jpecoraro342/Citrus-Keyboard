using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PiBridge : MonoBehaviour {

	public InputField InputTextArea;


	int lastLeftSector = -1;
	int lastRightSector = -1;

	float currentLeftJoystickAngle;
	float currentRightJoystickAngle;

	public Text debugText;

	public Pi pi; 

	public int numLeftSectors = 5;
	public int numRightSectors = 6;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDebugText();
	}

	int angleToZone(float angle, int numZones) {
		//int numZones = pi.numSectors;
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
		lastLeftSector = -1;
	}

	//right thumbstick released, add character to textbox
	public void characterSelect() {

		if (lastLeftSector != -1) {
			InputTextArea.text += pi.getChar (lastLeftSector, lastRightSector);
			pi.resetFocus ();
		}

		lastRightSector = -1;
	}

	
	public void updateLeft(float left) {
		currentLeftJoystickAngle = left;
		int sector = angleToZone (left, numLeftSectors);
		if (sector != lastLeftSector) {
			lastLeftSector = sector;
			pi.setFocusDisabled(lastLeftSector);
		}
	}

	public void updateRight(float right) {
		currentRightJoystickAngle = right;
		int sector = angleToZone (right, numRightSectors);
		if (sector != lastRightSector) {
			lastRightSector = sector;
			pi.setFocusActive(lastLeftSector, lastRightSector);
		}
	}

	void UpdateDebugText() {
		debugText.text = "Left Joystick:\n\tAngle: " + currentLeftJoystickAngle + "\n\tSector: " + lastLeftSector;
		debugText.text += "\nRight Joystick:\n\tAngle: " + currentRightJoystickAngle + "\n\tSector: " + lastRightSector;
	}
}
