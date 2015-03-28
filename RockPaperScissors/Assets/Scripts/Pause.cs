/*************************************************************************\
*	Author:		Braden Gibson				  					 		  *
*	Purpose:	One instance in each map. Waits for pause button input to *
*				stop and resume the game.								  *	
\*************************************************************************/
using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	/***********\
	* VARIABLES *
	\***********/
	private bool isPaused; // keeps track of if the game is paused
	
	private KeyCode P1Pause; // stores the pause button for each player
	private KeyCode P2Pause;
	private KeyCode P3Pause;
	private KeyCode P4Pause;
	
	private bool P3Playing; // keeps track of whether P3 and P4 are playing
	private bool P4Playing;
	
	
	
	/*******\
	* START *
	\*******/
	void Start () {
		
		// Initialize the variables
		isPaused = false;
		P3Playing = true;
		P4Playing = true;
		
		// Get controller information
		InitializeStorage settings = (InitializeStorage) GameObject.Find("VariableStorage").GetComponent(typeof(InitializeStorage));
		
		switch((int)settings.P1Controller)
		{
			case 1:
				P1Pause = KeyCode.Escape;
				break;
			case 2:
				P1Pause = KeyCode.Escape;
				break;
			case 3:
				P1Pause = KeyCode.Joystick1Button7;
				break;
			case 4:
				P1Pause = KeyCode.Joystick2Button7;
				break;
			case 5:
				P1Pause = KeyCode.Joystick3Button7;
				break;
			case 6:
				P1Pause = KeyCode.Joystick4Button7;
				break;
		}
		switch((int)settings.P1Controller)
		{
			case 1:
				P2Pause = KeyCode.Escape;
				break;
			case 2:
				P2Pause = KeyCode.Escape;
				break;
			case 3:
				P2Pause = KeyCode.Joystick1Button7;
				break;
			case 4:
				P2Pause = KeyCode.Joystick2Button7;
				break;
			case 5:
				P2Pause = KeyCode.Joystick3Button7;
				break;
			case 6:
				P2Pause = KeyCode.Joystick4Button7;
				break;
		}
		switch((int)settings.P1Controller)
		{
			case 0:
				P3Playing = false;
				break;
			case 1:
				P3Pause = KeyCode.Escape;
				break;
			case 2:
				P3Pause = KeyCode.Escape;
				break;
			case 3:
				P3Pause = KeyCode.Joystick1Button7;
				break;
			case 4:
				P3Pause = KeyCode.Joystick2Button7;
				break;
			case 5:
				P3Pause = KeyCode.Joystick3Button7;
				break;
			case 6:
				P3Pause = KeyCode.Joystick4Button7;
				break;
		}
		switch((int)settings.P1Controller)
		{
			case 0:
				P4Playing = false;
				break;
			case 1:
				P4Pause = KeyCode.Escape;
				break;
			case 2:
				P4Pause = KeyCode.Escape;
				break;
			case 3:
				P4Pause = KeyCode.Joystick1Button7;
				break;
			case 4:
				P4Pause = KeyCode.Joystick2Button7;
				break;
			case 5:
				P4Pause = KeyCode.Joystick3Button7;
				break;
			case 6:
				P4Pause = KeyCode.Joystick4Button7;
				break;
		}
	}
	
	
	
	/********\
	* UPDATE *
	\********/
	void Update () {
		
		// Check to see if a pause button was pressed
		if (Input.GetKeyUp(P1Pause) || Input.GetKeyUp(P2Pause) || (P3Playing && Input.GetKeyUp(P3Pause)) || (P4Playing && Input.GetKeyUp(P4Pause)))
		{
			// Toggle pause and adjust the game speed
			if (isPaused)
			{
				isPaused = false;
				Time.timeScale = 1f;
			}
			else
			{
				isPaused = true;
				Time.timeScale = 0f;
			}
		}
	}
}
