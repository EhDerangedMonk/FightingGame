using UnityEngine;
using System.Collections;

public class playerSpawner : MonoBehaviour {

	public InitializeStorage gameSettings;

	public int playerNum; // Player this Spawner is responsibile for. Player 1 = 1, Player 2 = 2, etc.

	private Player player;
	private GameObject newPlayer;
	private int layout;
	private Controls controller;
	private Animator anim;

	// Use this for initialization
	void Start () {
		gameSettings = FindObjectOfType<InitializeStorage> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn() {
		if (playerNum == 1) {

			//Assign Character
			switch ((int)gameSettings.P1Character) {
				case 1:
					newPlayer = Resources.Load ("Violet") as GameObject;
					break;
				case 2:
					newPlayer = Resources.Load ("Zakir") as GameObject;
					break;
				case 3:
					newPlayer = Resources.Load ("Noir") as GameObject;
					break;
				default: //Load Noir by default.
					newPlayer = Resources.Load ("Noir") as GameObject;
					break;
			}
			Instantiate(newPlayer, transform.position, transform.rotation);

			player = (Player)newPlayer.gameObject.GetComponent (typeof(Player));
			player.layout = 1;
		}
	}
}
