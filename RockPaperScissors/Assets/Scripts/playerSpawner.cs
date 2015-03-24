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

			newPlayer.GetComponent<Player>().layout = 1;
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

			newPlayer.GetComponent<Player>().layout = 2;
			Instantiate(newPlayer, transform.position, transform.rotation);

		}
	}
}
