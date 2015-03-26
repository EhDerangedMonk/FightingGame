using UnityEngine;
using System.Collections;

public class playerOverseer : MonoBehaviour {

	private int numPlayers;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		numPlayers = players.Length;

		if (players != null) {
			foreach (GameObject player in players) {
				Player subject = (Player)player.gameObject.GetComponent (typeof(Player));

				if(subject != null) {
					if (subject.playerHealth.isDead())
						numPlayers--;
				}
			}
		}


		//Once all but one of the players are dead, return to the character selection menu.
		if (numPlayers <= 1)
			Invoke ("EndGame", 5);
	}

	void EndGame() {
		Application.LoadLevel ("CharacterSelectionMenu");
		print ("Game over!");
	}
}
