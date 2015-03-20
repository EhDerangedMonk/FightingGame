/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of rock objects, which are hazards to players.
*/

using UnityEngine;
using System.Collections;

public class rockBehaviour : MonoBehaviour {
	Player victim;
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
		bool dealDamage = true;

		if (other.gameObject.tag == "Player") {
			victim = (Player)other.gameObject.GetComponent(typeof(Player));

			if(!hit) {
				//Do not deal damage if the player is blocking.
				if(victim.playerState.isBlock ()) {
					//If the rock collides to the right of the player.
					if(this.transform.position.x > victim.transform.position.x) {
						if(!victim.playerState.isFacingLeft ())
							dealDamage = false;
					}
					//If the rock collides to the left of the player.
					else {
						if(victim.playerState.isFacingLeft())
							dealDamage = false;
					}
				}

				if(dealDamage == true) {
					victim.playerState.setLaunch(true); //Causes a launch to the player.
					victim.playerHealth.damage(100); // Rocks deal 100DMG

					//Apply a force to the player in the corresponding direction.
					if(this.transform.position.x > victim.transform.position.x)
						rigidbody2D.velocity = new Vector2(100, 0);
					else
						rigidbody2D.velocity = new Vector2(-100, 0);
				}
				hit = true;
			}
			playHit(); //The rock 'breaks' when it collides with a player.
		}
		//Rocks break each other.
		if (other.gameObject.tag == "Rock") {
			playHit(); //The rock 'breaks' when it collides with another rock.
		}
	}
}
