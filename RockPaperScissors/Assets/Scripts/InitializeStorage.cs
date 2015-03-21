/********************************************************************************\
*	Author:		Braden Gibson				  									 *
*	Purpose:	Runs once at the start of the game and stores "global" variables *
*				between scenes. Is not destroyed on scene loads.				 *
\********************************************************************************/

using UnityEngine;
using System.Collections;

public class InitializeStorage : MonoBehaviour {

	/***********\
	* VARIABLES *
	\***********/
	public enum ControllerSelection {None=0, KB1, KB2, C1, C2, C3, C4};
	public enum CharacterSelection {None=0, Violet, Zakir, Noir};
	
	public ControllerSelection P1Controller; // Stores which controller is assigned to which player
	public ControllerSelection P2Controller;
	public ControllerSelection P3Controller;
	public ControllerSelection P4Controller;
	
	public CharacterSelection P1Character; // Stores which character was selected by each player
	public CharacterSelection P2Character;
	public CharacterSelection P3Character;
	public CharacterSelection P4Character;



	/*******\
	* AWAKE *
	\*******/
	void Awake()
	{
		DontDestroyOnLoad(this); // make sure that this script stays active throughout the entire game
	}
	
	
	
	/*******\
	* START *
	\*******/
	void Start()
	{
		Application.LoadLevel("mainmenu"); // transition straight into the main menu
	}
}
