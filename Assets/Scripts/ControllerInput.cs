using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControllerInput : MonoBehaviour {

	public Image LeftJoystick;
	public Image RightJoystick;

	public Text DebugText;

	private static string LeftTrigger = "LeftTrigger";
	private static string RightTrigger = "RightTrigger";

	private static string LeftBumper = "LeftBumper";
	private static string RightBumper = "RightBumper";

	Vector2 LeftInput;
	Vector2 RightInput;
	
	void Start () {
	
	}

	void Update () {
		float leftX = Input.GetAxisRaw("Horizontal");
		float leftY = Input.GetAxisRaw("Vertical");

		float rightX = Input.GetAxisRaw("RightHorizontal");
		float rightY = -1*Input.GetAxisRaw("RightVertical");

		LeftInput = new Vector2(leftX, leftY);
		RightInput = new Vector2(rightX, rightY);

		UpdateDebugText();
	}

	void UpdateDebugText () {
		Quaternion leftJoystickAngle = Quaternion.Euler(LeftInput);
		Quaternion rightJoystickAngle = Quaternion.Euler(RightInput);

		DebugText.text = "Left Joystick: " + LeftInput + "\nAngle: " + leftJoystickAngle + "\n\nRight Joystick: " + RightInput + "\nAngle: " + rightJoystickAngle;
	}
}
