/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of pitfall hazards.
*/

using UnityEngine;
using System.Collections;

public class Pitfall : MonoBehaviour {

	public int orientation;

	private Player victim;
	private Vector3 spawnLocation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Called when a 2D object collides with another 2D object
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			victim = (Player)other.gameObject.GetComponent(typeof(Player));
			spawnLocation = victim.transform.position;

			if(victim.playerHealth.isDead () == false) {
				//Create an explosion in a fashion similar to Super Smash Bros.
				GameObject explosion = Resources.Load("Screen Explosion") as GameObject;
				if(orientation == 0) //Spawn from below the screen.
					Instantiate (explosion, spawnLocation, transform.rotation);
				else if(orientation == 1) //Spawn from the left of the screen.
					Instantiate(explosion, spawnLocation, Quaternion.Euler (0,0,270));
				else //Spawn from the right of the screen.
					Instantiate(explosion, spawnLocation, Quaternion.Euler (0,0,90));
			}

			victim.playerHealth.kill ();
		}
	}
}
