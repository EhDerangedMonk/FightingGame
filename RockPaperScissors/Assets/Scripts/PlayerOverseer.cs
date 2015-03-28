/*
	Authored By: Nigel Martinez
	Purpose: Observes players, ensuring the match is over when only one player remains.
*/
using UnityEngine;
using System.Collections;

public class PlayerOverseer : MonoBehaviour {

	private int numPlayers ;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		//Find players.
		numPlayers = 0;
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		//Count how many are alive.
		if (players != null) {
			foreach (GameObject player in players) {
				Player subject = (Player)player.gameObject.GetComponent (typeof(Player));

					if (!subject.playerHealth.isDead())
						numPlayers++;
				}
			}

		//Once all but one of the players are dead, return to the character selection menu.
		if (numPlayers <= 1)
			Invoke ("EndGame", 5);
	}

	/*
     * DESCR: Ends the game and returns to the character selection menu.
     * PRE: NONE
     * POST: The scene transitions to the character selection menu.
     */
	void EndGame() {
		Application.LoadLevel ("CharacterSelectionMenu");
		print ("Game over!");
	}
}
