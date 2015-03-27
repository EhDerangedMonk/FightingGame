/*
	Authored By: Josiah Menezes & Jerrit Anderson
	Purpose: Contains the control behaviour for the Player
*/
using UnityEngine;
using System.Collections;

public class Controls {

	private KeyCode light;
	private KeyCode heavy;
	private KeyCode special;
	private KeyCode block;
	private KeyCode grapple;
	private KeyCode jump;
	private string XAxis;
	private string YAxis;
	private KeyCode cChangeKey;

	// Constructor
	public Controls(int layout) {
		changeControls(layout);
	}

	/*
	 * DESCR: Changes the control scheme layout for the current player
	 * 1 - Keyboard layout (W,A,S,D - left Ctrl, Left Alt, Left Shift, F)
	 * 2 - Keyboard layout (I,J,K,L - Semi quote, Slash, Quote, O)
	 * 3 - 6 Game Controllers
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

	// Getters (Return Keycodes)

	public KeyCode getLightKey() {
		return light;
	}

	public KeyCode getHeavyKey() {
		return heavy;
	}

	public KeyCode getSpecialKey() {
		return special;
	}

	public KeyCode getBlockKey() {
		return block;
	}

	public KeyCode getJumpKey() {
		return jump;
	}

	public KeyCode getGrappleKey() {
		return grapple;
	}

	public string getXAxisKey() {
		return XAxis;
	}

	public string getYAxisKey() {
		return YAxis;
	}
}