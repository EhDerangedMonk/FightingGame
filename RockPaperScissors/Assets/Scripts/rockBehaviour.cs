/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of rock objects, which are hazards to players.
*/

using UnityEngine;
using System.Collections;

public class rockBehaviour : MonoBehaviour {
	private Player rockVictim;
	private Animator anim;
	private bool hit = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void playHit() {
		anim.SetTrigger ("Hit");
	}

	void Destroy () {
		Destroy (this.gameObject);
	}

	// Called when a 2D object collides with another 2D object
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			rockVictim = (Player)other.gameObject.GetComponent(typeof(Player));
			if(!hit) {
				rockVictim.playerHealth.damage(100); // Rocks deal 100DMG
				hit = true;
			}
			playHit(); //The rock 'breaks' when it collides with a player.
		}
		//Rocks breaking eachother?
		if (other.gameObject.tag == "Rock") {
			playHit(); //The rock 'breaks' when it collides with another rock.
		}
	}
}
