using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public InitializeStorage gameSettings;

	//FUCK!
	public int playerNum; // Player this Spawner is responsibile for. Player 1 = 1, Player 2 = 2, etc.

	private Player player;
	private GameObject newPlayer;

	// Use this for initialization
	void Start () {
		//Find the options.
		gameSettings = FindObjectOfType<InitializeStorage>();

		if (playerNum == 1 && gameSettings.P1Controller != 0)
			Spawn (gameSettings.P1Character, gameSettings.P1Controller);
		else if (playerNum == 2 && gameSettings.P2Controller != 0)
			Spawn (gameSettings.P2Character, gameSettings.P2Controller);
		else if (playerNum == 3 && gameSettings.P3Controller != 0)
			Spawn (gameSettings.P3Character, gameSettings.P3Controller);
		else if (playerNum == 4 && gameSettings.P4Controller != 0)
			Spawn (gameSettings.P4Character, gameSettings.P4Controller);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn(InitializeStorage.CharacterSelection character, InitializeStorage.ControllerSelection controller) {
			//Assign Character
			switch((int)character) {
				case 1: //Violet
					newPlayer = Resources.Load ("Violet") as GameObject;
					break;
				case 2: //Zakir
					newPlayer = Resources.Load ("Zakir") as GameObject;
					break;
				case 3: //Noir
					newPlayer = Resources.Load ("Noir") as GameObject;
					break;
				default:
					print("Error: Character not selected.");
					break;
			}

			//Assign Controller
			if(controller != 0)
				newPlayer.GetComponent<Player>().layout = (int)controller;

			//Spawn the player.
			Instantiate (newPlayer, transform.position, transform.rotation);
	}
}
