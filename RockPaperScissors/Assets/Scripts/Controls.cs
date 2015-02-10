using UnityEngine;
using System.Collections;

public class Controls {

	public KeyCode light;
	public KeyCode heavy;
	public KeyCode special;
	public KeyCode jump;
	public KeyCode left;
	public KeyCode right;


	public Controls(int layout) {
		switch(layout) {
			case 1:
				light = KeyCode.Space;
				heavy = KeyCode.LeftControl;
				special = KeyCode.LeftShift;
				jump = KeyCode.W;
				left = KeyCode.A;
				right = KeyCode.D;
				break;
			case 2:
				light = KeyCode.O;
				heavy = KeyCode.Quote;
				special = KeyCode.Semicolon;
				jump = KeyCode.I;
				left = KeyCode.J;
				right = KeyCode.L;
				break;
			default:
				Debug.Log("Unkown player! " + layout);
				break;
		}
	}
}