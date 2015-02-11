using UnityEngine;
using System.Collections;

public class Health {
	public int health; //Current players health out of 1000

	// Constructor
	public Health(int hp) {
		health = hp;
	}

	/*
	 * DESCR: Damages player health
	 * PRE: Give int amount of damage to be taken from the player health
	 * POST: Returns bool if True player is dead/ False player is alive
	 */
	public bool damage(int damage) {
		health = health - damage;
		
		if (health <= 0) {
			Debug.Log("Player is dead!");
			health = 0;
			return true;
		} else {
				Debug.Log("Player has taken damage!: " + damage + " - Remaining health is: " + health);
		}
		return false;
	}

	/*
	 * DESCR: Heals player health
	 * PRE: Give int amount of health to add to the current player health
	 * POST: NONE
	 */
	public void heal(int heal) {
		health = health + heal;
		Debug.Log("Player is healing for " + health + " - Remaining health is: " + health);
	}


	/*
	 * DESCR: Tells you if the player is dead or not
	 * PRE: None
	 * POST: True is dead / False is alive
	 */
	public bool dead() {
		if (health <= 0)
			return true;
		return false;
	}

}