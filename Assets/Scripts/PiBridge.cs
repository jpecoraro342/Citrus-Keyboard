using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PiBridge : MonoBehaviour {

	public InputField InputTextArea;

	int lastLeftSector;
	int lastRightSector;

	float currentLeftJoystickAngle;
	float currentRightJoystickAngle;

	public Text debugText;

	public Pi pi; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDebugText();
	}

	int angleToZone(float angle) {
		int numZones = pi.numSectors;
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
		InputTextArea.text += pi.getChar (lastLeftSector, lastRightSector);

		pi.resetFocus ();
	}

	public void updateAngles(float left, float right) {
		currentLeftJoystickAngle = left;
		currentRightJoystickAngle = right;

		if (angleToZone(left) != lastLeftSector) {
			lastLeftSector = angleToZone(left);
			pi.setFocusDisabled(angleToZone(left));
		}
		if (angleToZone(right) != lastRightSector) {
			lastRightSector = angleToZone(right);
			pi.setFocusActive(angleToZone(right));
		}
	}

	void UpdateDebugText() {
		debugText.text = "Left Joystick:\n\tAngle: " + currentLeftJoystickAngle + "\n\tSector: " + lastLeftSector;
		debugText.text += "\nRight Joystick:\n\tAngle: " + currentRightJoystickAngle + "\n\tSector: " + lastRightSector;
	}
}
