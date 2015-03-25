using UnityEngine;
using System.Collections;

public class playerOverseer : MonoBehaviour {

	private int numPlayers;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		numPlayers = 0;
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		foreach (GameObject player in players) {
			Player subject = (Player)player.gameObject.GetComponent(typeof(Player));
			
			if(subject.playerHealth.isDead() == false)
				numPlayers++;
		}

		print (numPlayers);

		//Once all but one of the players are dead, return to the character selection menu.
		if (numPlayers <= 1)
			Invoke ("EndGame", 5);
	}

	void EndGame() {
		Application.LoadLevel ("CharacterSelectionMenu");
		print ("Game over!");
	}
}
