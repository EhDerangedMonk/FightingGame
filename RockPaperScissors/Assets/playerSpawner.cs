using UnityEngine;
using System.Collections;

public class playerSpawner : MonoBehaviour {

	public InitializeStorage gameSettings;

	public int playerNum; // Player this Spawner is responsibile for. Player 1 = 1, Player 2 = 2, etc.

	private Player player;
	private int layout;
	private Controls controller;

	// Use this for initialization
	void Start () {
		gameSettings = FindObjectOfType<InitializeStorage> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn() {
		if (playerNum == 1) {
			layout = 1;

			//Assign Controller
			switch(gameSettings.P1Controller) {
				case 1;
			}

			//Assign Character
			switch(gameSettings.P1Character) {
			
			}

			GameObject newPlayer = Resources.Load ("Player") as GameObject;

		}
	}
}
