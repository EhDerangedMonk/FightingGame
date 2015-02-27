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
	public KeyCode jump;
	public KeyCode left;
	public KeyCode right;
	public KeyCode block;
	public KeyCode grapple;

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
				break;
			default:
				Debug.Log("Unkown player! " + layout);
				break;
		}
	}
}