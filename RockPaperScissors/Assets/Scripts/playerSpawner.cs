using UnityEngine;
using System.Collections;

public class playerSpawner : MonoBehaviour {

	public InitializeStorage gameSettings;

	public int playerNum; // Player this Spawner is responsibile for. Player 1 = 1, Player 2 = 2, etc.

	private Player player;
	private GameObject newPlayer;

	// Use this for initialization
	void Start () {
	
		gameSettings = FindObjectOfType<InitializeStorage>();
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn() {
		if (playerNum == 1) {
			//Assign Character
			if(gameSettings.P1Character == InitializeStorage.CharacterSelection.Noir)
				newPlayer = Resources.Load ("Noir") as GameObject;
			else if(gameSettings.P1Character == InitializeStorage.CharacterSelection.Violet)
				newPlayer = Resources.Load ("Violet") as GameObject;
			else if(gameSettings.P1Character == InitializeStorage.CharacterSelection.Zakir)
				newPlayer = Resources.Load ("Zakir") as GameObject;
			else
				print ("Error: Character Selection Not Found");

			//Assign Controller
			if(gameSettings.P1Controller == InitializeStorage.ControllerSelection.KB1)
				newPlayer.GetComponent<Player>().layout = 1;
			else if(gameSettings.P1Controller == InitializeStorage.ControllerSelection.KB2)
				newPlayer.GetComponent<Player>().layout = 2;
			else if(gameSettings.P1Controller == InitializeStorage.ControllerSelection.C1)
				newPlayer.GetComponent<Player>().layout = 3;
			else if(gameSettings.P1Controller == InitializeStorage.ControllerSelection.C2)
				newPlayer.GetComponent<Player>().layout = 4;
			else if(gameSettings.P1Controller == InitializeStorage.ControllerSelection.C3)
				newPlayer.GetComponent<Player>().layout = 5;
			else if(gameSettings.P1Controller == InitializeStorage.ControllerSelection.C4)
				newPlayer.GetComponent<Player>().layout = 6;
			else
				print ("Error: Controller not found.");

			//Spawn the player.
			Instantiate(newPlayer, transform.position, transform.rotation);
		}

		if (playerNum == 2) {
			//Assign Character
			if(gameSettings.P2Character == InitializeStorage.CharacterSelection.Noir)
				newPlayer = Resources.Load ("Noir") as GameObject;
			else if(gameSettings.P2Character == InitializeStorage.CharacterSelection.Violet)
				newPlayer = Resources.Load ("Violet") as GameObject;
			else if(gameSettings.P2Character == InitializeStorage.CharacterSelection.Zakir)
				newPlayer = Resources.Load ("Zakir") as GameObject;
			else
				print ("Error: Character Selection Not Found");

			//Assign Controller
			if(gameSettings.P2Controller == InitializeStorage.ControllerSelection.KB1)
				newPlayer.GetComponent<Player>().layout = 1;
			else if(gameSettings.P2Controller == InitializeStorage.ControllerSelection.KB2)
				newPlayer.GetComponent<Player>().layout = 2;
			else if(gameSettings.P2Controller == InitializeStorage.ControllerSelection.C1)
				newPlayer.GetComponent<Player>().layout = 3;
			else if(gameSettings.P2Controller == InitializeStorage.ControllerSelection.C2)
				newPlayer.GetComponent<Player>().layout = 4;
			else if(gameSettings.P2Controller == InitializeStorage.ControllerSelection.C3)
				newPlayer.GetComponent<Player>().layout = 5;
			else if(gameSettings.P2Controller == InitializeStorage.ControllerSelection.C4)
				newPlayer.GetComponent<Player>().layout = 6;
			else
				print ("Error: Controller not found.");
			
			//Spawn the player.
			Instantiate(newPlayer, transform.position, transform.rotation);

		}
	}
}
