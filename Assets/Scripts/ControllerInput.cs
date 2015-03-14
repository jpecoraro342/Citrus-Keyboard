using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum KeyboardType {
	LOWERCASE,
	CAPITAL,
	NUMBERS,
	SYMBOLS
}

public class ControllerInput : MonoBehaviour {
	public float VisualJoystickMagnitude = 30;
	public float JoystickSmoothing = 20;

	public float CenteredThreshold = .1f;

	public Image LeftJoystick;
	public Image RightJoystick;

	public Text DebugText;

	private static string LeftTrigger = "LeftTrigger";
	private static string RightTrigger = "RightTrigger";

	private static string LeftBumper = "LeftBumper";
	private static string RightBumper = "RightBumper";

	Vector2 LeftInput;
	Vector2 RightInput;

	bool LeftJoystickCentered;
	bool RightJoystickCentered;

	bool LeftTriggerPressed;
	bool RightTriggerPressed;
	
	void Start() {
	
	}

	void Update() {
		float leftX = Input.GetAxisRaw("Horizontal");
		float leftY = Input.GetAxisRaw("Vertical");

		float rightX = Input.GetAxisRaw("RightHorizontal");
		float rightY = -1*Input.GetAxisRaw("RightVertical");

		LeftInput = new Vector2(leftX, leftY);
		RightInput = new Vector2(rightX, rightY);

		CheckJoystickSelections();
		UpdateKeyboard();
		UpdateJoystickPositions();
		UpdateDebugText();
	}

	void UpdateJoystickPositions() {
		Vector2 LeftPosition = LeftInput.normalized * VisualJoystickMagnitude;
		Vector2 RightPosition = RightInput.normalized * VisualJoystickMagnitude;

		LeftJoystick.rectTransform.anchoredPosition = Vector2.Lerp(LeftJoystick.rectTransform.anchoredPosition, LeftPosition, JoystickSmoothing * Time.deltaTime);
		RightJoystick.rectTransform.anchoredPosition = Vector2.Lerp(RightJoystick.rectTransform.anchoredPosition, RightPosition, JoystickSmoothing * Time.deltaTime);
	}

	//Update Keyboard
	void UpdateKeyboard() {
		bool DidPressLeftTrigger = Input.GetButton(LeftTrigger);
		bool DidPressRightTrigger = Input.GetButton(RightTrigger);

		if (DidPressLeftTrigger && !LeftTriggerPressed) {
			LeftTriggerPressed = true;
			ChangeKeyboard(KeyboardType.NUMBERS);
		}

		if (DidPressRightTrigger && !RightTriggerPressed) {
			RightTriggerPressed = true;
			ChangeKeyboard(KeyboardType.CAPITAL);
		}

		if (!DidPressLeftTrigger && LeftTriggerPressed) {
			LeftTriggerPressed = false;
			if (RightTriggerPressed) {
				ChangeKeyboard(KeyboardType.CAPITAL);
			}
			else {
				ChangeKeyboard(KeyboardType.LOWERCASE);
			}
		}
		
		if (!DidPressRightTrigger && RightTriggerPressed) {
			RightTriggerPressed = false;
			if (LeftTriggerPressed) {
				ChangeKeyboard(KeyboardType.NUMBERS);
			}
			else {
				ChangeKeyboard(KeyboardType.LOWERCASE);
			}
		}
	}

	void ChangeKeyboard(KeyboardType Keyboard) {
		//TODO: call change keyboard on manager
	}

	//Check to See if the Left Joystick selected "Cancel" and the Right Joystick "Selected the Character"
	void CheckJoystickSelections() {
		bool CancelSelected = DidLeftJoystickCenter();
		bool CharacterSelected = DidRightJoystickCenter();

		if (CancelSelected) {
			//TODO: Tell the manager that a character was selected
		}

		if (CharacterSelected) {
			//TODO: Tell the manager that a character was selected
		}
	}

	bool DidLeftJoystickCenter() {
		float VectorMagnitude = LeftInput.magnitude;

		if (VectorMagnitude > CenteredThreshold) {
			//Joystick is not centered
			LeftJoystickCentered = false;
			return false;
		}
		else if (LeftJoystickCentered) {
			//Joystick is Centered, and Was Previously
			return false;
		}
		else {
			LeftJoystickCentered = true;
			return true;
		}
	}

	bool DidRightJoystickCenter() {
		float VectorMagnitude = RightInput.magnitude;
		
		if (VectorMagnitude > CenteredThreshold) {
			//Joystick is not centered
			RightJoystickCentered = false;
			return false;
		}
		else if (RightJoystickCentered) {
			//Joystick is Centered, and Was Previously
			return false;
		}
		else {
			RightJoystickCentered = true;
			return true;
		}
	}

	//Debugging
	void UpdateDebugText() {
		float leftJoystickAngle = getVectorAngle(LeftInput);
		float rightJoystickAngle = getVectorAngle(RightInput);

		DebugText.text = "Left Joystick: " + LeftInput + "\nAngle: " + leftJoystickAngle + "\n\nRight Joystick: " + RightInput + "\nAngle: " + rightJoystickAngle;
	}

	//Math Methods
	float getVectorAngle(Vector2 InputVector) {
		if (InputVector.x == 0 && InputVector.y == 0) {
			return 0;
		}

		float angle = Mathf.Atan2(InputVector.x, InputVector.y) * Mathf.Rad2Deg;

		if (angle < 0) {
			angle = 360 + angle;
		}

		return angle;
	}
}
