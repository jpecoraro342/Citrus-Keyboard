using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum KeyboardType {
	CAPITAL,
	LOWERCASE,
	NUMBERS,
	SYMBOLS
}

public class ControllerInput : MonoBehaviour {
	public float VisualJoystickMagnitude = 30;
	public float JoystickSmoothing = 20;

	private float CenteredThreshold = 0.8f;

	public PiBridge Manager;

	public Image LeftJoystick;
	public Image RightJoystick;

	private static string LeftTrigger = "LeftTrigger";
	private static string RightTrigger = "RightTrigger";

	private static string LeftBumper = "LeftBumper";
	private static string RightBumper = "RightBumper";

	Vector2 LeftInput;
	Vector2 RightInput;

	bool LeftJoystickCentered = true;
	bool RightJoystickCentered = true;

	bool LeftTriggerPressed;
	bool RightTriggerPressed;

	bool LeftBumperPressed;
	bool RightBumperPressed;
	
	void Start() {
	
	}

	void Update() {
		float leftX = Input.GetAxisRaw("Horizontal");
		float leftY = Input.GetAxisRaw("Vertical");

		float rightX = Input.GetAxisRaw("RightHorizontal");
		float rightY = -1*Input.GetAxisRaw("RightVertical");

		LeftInput = new Vector2(leftX, leftY);
		RightInput = new Vector2(rightX, rightY);

		//check joystick must go first.
		CheckJoystickSelections();
		UpdateManagerAngles();
		UpdateKeyboard();
		UpdateBumpers();
		UpdateJoystickPositions();
	}

	void UpdateJoystickPositions() {
		Vector2 LeftPosition = LeftInput.normalized * VisualJoystickMagnitude;
		Vector2 RightPosition = RightInput.normalized * VisualJoystickMagnitude;

		LeftJoystick.rectTransform.anchoredPosition = Vector2.Lerp(LeftJoystick.rectTransform.anchoredPosition, LeftPosition, JoystickSmoothing * Time.deltaTime);
		RightJoystick.rectTransform.anchoredPosition = Vector2.Lerp(RightJoystick.rectTransform.anchoredPosition, RightPosition, JoystickSmoothing * Time.deltaTime);
	}

	//Update Bumpers
	void UpdateBumpers() {
		bool DidPressLeftBumper = Input.GetButton(LeftBumper);
		bool DidPressRightBumper = Input.GetButton(RightBumper);
		
		if (DidPressLeftBumper && !LeftBumperPressed) {
			LeftBumperPressed = true;
			Manager.backSpace();
		}

		if (DidPressRightBumper && !RightBumperPressed) {
			RightBumperPressed = true;
			Manager.backSpace();
		}
		
		if (!DidPressLeftBumper && LeftBumperPressed) {
			LeftBumperPressed = false;
		}
		
		if (!DidPressRightBumper && RightBumperPressed) {
			RightBumperPressed = false;
		}
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
		Manager.changeKeyboard(Keyboard);
	}

	//Check to See if the Left Joystick selected "Cancel" and the Right Joystick "Selected the Character"
	void CheckJoystickSelections() {
		bool CancelSelected = DidLeftJoystickCenter();
		bool CharacterSelected = DidRightJoystickCenter();

		if (CancelSelected) {
			Manager.cancelInput();
		}

		if (CharacterSelected) {
			Manager.characterSelect();
		}
	}

	bool DidLeftJoystickCenter() {
		float VectorMagnitude = LeftInput.magnitude;
	
		if (VectorMagnitude > CenteredThreshold/4) {
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
	
	void UpdateManagerAngles() {
		float leftJoystickAngle = getVectorAngle(LeftInput);
		float rightJoystickAngle = getVectorAngle(RightInput);

		if (!LeftJoystickCentered) {
			Manager.updateLeft(leftJoystickAngle);
		}

		if (!RightJoystickCentered) {
			Manager.updateRight(rightJoystickAngle);
		}
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
