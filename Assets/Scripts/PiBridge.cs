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

	public int numLeftSectors = 4;
	public int numRightSectors = 5;

	string[] lastTwoChars = new string[2] { "",""};
	bool newSentence = true;
	bool midSentence = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDebugText();
	}

	int rightAngleToZone(float angle, int numZones) {
		//int numZones = pi.numSectors;
		//return (int)(angle + 360/(numZones*2))*numZones/360;
		int sector = 0;
		int buffer = (((lastLeftSector-0)*(2 - (-2))) / (4 - 0)) - 2;
		buffer *= 7;
		if (angle < 30 + buffer) {
			sector = 0;
		} else if (angle < 90 + buffer) {
			sector = 1;
		} else if (angle < 150 + buffer) {
			sector = 2;
		} else if (angle < 210 + buffer) {
			sector = 3;
		} else if (angle < 270 + buffer) {
			sector = 4;
		} else if (angle < 330 + buffer) {
			sector = 5;
		} else {
			sector = 0;
		}
		return sector;
	}

	int leftAngleToZone(float angle, int numZones) {
		//int numZones = pi.numSectors;
		return (int)(angle)*numZones/360;
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

	public void backSpace() {
		if (InputTextArea.text.Length > 0) {
			InputTextArea.text = InputTextArea.text.Substring(0, InputTextArea.text.Length - 1);
		}
	}

	//right thumbstick released, add character to textbox
	public void characterSelect() {
		newSentence = false;
		if (lastLeftSector != -1) {
			string input = pi.getChar (lastLeftSector, lastRightSector);
			InputTextArea.text += input;
			pi.resetFocus ();
			pi.setFocusDisabled(lastLeftSector);

			lastTwoChars[0] = lastTwoChars[1];
			lastTwoChars[1] = input;


			checkSentence();

		}

		lastRightSector = -1;
	}

	public void checkSentence() {
		if ((lastTwoChars[0] + lastTwoChars[1]) == ". ") {
			newSentence = true;
			midSentence = false;
		}
		if (!midSentence) {
			if (newSentence) {
				changeKeyboard(KeyboardType.CAPITAL);
				midSentence = true;
			} else {
				changeKeyboard (KeyboardType.LOWERCASE);
				midSentence = true;
			}
		}
	}
	
	public void updateLeft(float left) {
		currentLeftJoystickAngle = left;
		int sector = leftAngleToZone (left, numLeftSectors);
		if (sector != lastLeftSector) {
			lastLeftSector = sector;
			pi.setFocusDisabled(lastLeftSector);
		}
	}

	public void updateRight(float right) {
		currentRightJoystickAngle = right;
		int sector = rightAngleToZone (right, numRightSectors);
		//if (sector != lastRightSector) {
			lastRightSector = sector;
			if (lastLeftSector != -1) {
				pi.setFocusActive(lastLeftSector, lastRightSector);
			}
		//}
	}

	void UpdateDebugText() {
		debugText.text = "Left Joystick:\n\tAngle: " + currentLeftJoystickAngle + "\n\tSector: " + lastLeftSector;
		debugText.text += "\nRight Joystick:\n\tAngle: " + currentRightJoystickAngle + "\n\tSector: " + lastRightSector;
	}
}
