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
	//Where is jump, left and right referenced?
	public KeyCode jump;
	public KeyCode left;
	public KeyCode right;
	public KeyCode block;
	public KeyCode grapple;
	public string XAxis;
	public string YAxis;

	// Constructor
	public Controls(int layout) {
		changeControls(layout);
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
				jump = KeyCode.W;
				left = KeyCode.A;
				right = KeyCode.D;
				block = KeyCode.F;
				grapple = KeyCode.LeftAlt;
				XAxis = "KeyboardlX";
				YAxis = "KeyboardlY";
				break;
			case 2:
				light = KeyCode.O;
				heavy = KeyCode.Quote;
				special = KeyCode.Semicolon;
				jump = KeyCode.I;
				left = KeyCode.J;
				right = KeyCode.L;
				block = KeyCode.Slash;
				grapple = KeyCode.P;
				XAxis = "Keyboard2X";
				YAxis = "Keyboard2Y";
				break;
			case 3:
				light = KeyCode.Joystick1Button0;
				heavy = KeyCode.Joystick1Button1;
				special = KeyCode.Joystick1Button2;
				jump = KeyCode.Joystick1Button3;
				left = KeyCode.E;
				right = KeyCode.R;
				block = KeyCode.Joystick1Button4;
				grapple = KeyCode.Joystick1Button5;
				XAxis = "LeftJoystick1X";
				YAxis = "LeftJoystick1Y";
				break;
			case 4:
				light = KeyCode.Joystick2Button0;
				heavy = KeyCode.Joystick2Button1;
				special = KeyCode.Joystick2Button2;
				jump = KeyCode.Joystick2Button3;
				left = KeyCode.E;
				right = KeyCode.R;
				block = KeyCode.Joystick2Button4;
				grapple = KeyCode.Joystick2Button5;
				XAxis = "LeftJoystick2X";
				YAxis = "LeftJoystick2Y";
				break;
			default:
				Debug.Log("Unkown player! " + layout);
				break;
		}
	}
}