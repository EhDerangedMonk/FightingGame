/*
	Authored By: Josiah Menezes & Jerrit Anderson
	Purpose: Contains the control behaviour for the Player
*/
using UnityEngine;
using System.Collections;

public class Controls {

	public KeyCode light;
	public KeyCode heavy;
	public KeyCode special;
	public KeyCode block;
	public KeyCode grapple;
	public KeyCode jump;
	public string XAxis;
	public string YAxis;
	public KeyCode cChangeKey;

	// Constructor
	public Controls(int layout, int changeKey) {
		changeControls(layout);
		if (changeKey == 1){
			cChangeKey = KeyCode.Alpha1;
		}
		else if (changeKey == 2){
			cChangeKey = KeyCode.Alpha2;
		}
		else if (changeKey == 3){
			cChangeKey = KeyCode.Alpha3;
		}
		else if (changeKey == 4){
			cChangeKey = KeyCode.Alpha4;
		}
		else{
			cChangeKey = KeyCode.Alpha0;
		}

	}

	/*
	 * DESCR: Changes the control scheme layout for the current player
	 * PRE: an integer representing the layout(1 - player 1/ 2 - player 2)
	 */
	public void changeControls(int layout) {
		switch(layout) {
			case 1:
				light = KeyCode.Space;
				heavy = KeyCode.LeftControl;
				special = KeyCode.LeftShift;
				block = KeyCode.F;
				jump = KeyCode.W;
				grapple = KeyCode.LeftAlt;
				XAxis = "Keyboard1X";
				YAxis = "Keyboard1Y";
				break;
			case 2:
				light = KeyCode.O;
				heavy = KeyCode.Quote;
				special = KeyCode.Semicolon;
				block = KeyCode.Slash;
				jump = KeyCode.I;
				grapple = KeyCode.P;
				XAxis = "Keyboard2X";
				YAxis = "Keyboard2Y";
				break;
			case 3:
				light = KeyCode.Joystick1Button2;
				heavy = KeyCode.Joystick1Button3;
				special = KeyCode.Joystick1Button1;
				block = KeyCode.Joystick1Button4;
				jump = KeyCode.Joystick1Button0;
				grapple = KeyCode.Joystick1Button5;
				XAxis = "LeftJoystick1X";
				YAxis = "LeftJoystick1Y";
				break;
			case 4:
				light = KeyCode.Joystick2Button2;
				heavy = KeyCode.Joystick2Button3;
				special = KeyCode.Joystick2Button1;
				block = KeyCode.Joystick2Button4;
				jump = KeyCode.Joystick2Button0;
				grapple = KeyCode.Joystick2Button5;
				XAxis = "LeftJoystick2X";
				YAxis = "LeftJoystick2Y";
				break;
			case 5:
				light = KeyCode.Joystick3Button2;
				heavy = KeyCode.Joystick3Button3;
				special = KeyCode.Joystick3Button1;
				block = KeyCode.Joystick3Button4;
				jump = KeyCode.Joystick3Button0;
				grapple = KeyCode.Joystick3Button5;
				XAxis = "LeftJoystick3X";
				YAxis = "LeftJoystick3Y";
				break;
			case 6:
				light = KeyCode.Joystick4Button2;
				heavy = KeyCode.Joystick4Button3;
				special = KeyCode.Joystick4Button1;
				block = KeyCode.Joystick4Button4;
				jump = KeyCode.Joystick4Button0;
				grapple = KeyCode.Joystick4Button5;
				XAxis = "LeftJoystick4X";
				YAxis = "LeftJoystick4Y";
				break;
			default:
				Debug.Log("Unkown player! " + layout);
				break;
		}
	}
}