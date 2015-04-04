/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of rock hazards.
*/

using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	private Player victim;
	private Animator anim;
	private bool hit = false;
	private HitMarkerSpawner hitFactory;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		hitFactory = GameObject.FindObjectOfType<HitMarkerSpawner> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void playHit() {
		anim.SetTrigger ("Hit");
	}

	/*
     * DESCR: Called by an animation event, the rock's game object is destroyed.
     * PRE: NONE
     * POST: The rock's game object is destroyed.
     */
	void Remove () {
		Destroy (this.gameObject);
	}

	// Called when a 2D object collides with another 2D object
	void OnCollisionEnter2D(Collision2D other) {
		bool dealDamage = true;
		bool inMotion = true;
		float horizVelocity = this.gameObject.rigidbody2D.velocity.x;

		if (horizVelocity > 3 || horizVelocity < -3)
			inMotion = true;
		else
			inMotion = false;

		if (other.gameObject.tag == "Player" && inMotion) {
			hitFactory.MakeHitMarker (other.gameObject, 4);
			victim = (Player)other.gameObject.GetComponent(typeof(Player));
			this.gameObject.rigidbody2D.velocity = new Vector3(0,0,0); //Kill momentum.

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
				//Otherwise, deal damage.
				if(dealDamage == true) {
					victim.playerState.setLaunch (true);
					victim.playerState.environmentDamage(100); // Rocks deal 100DMG

					//Launch the player in the corresponding direction.
					if(this.transform.position.x > victim.transform.position.x)
						victim.playerState.forceLaunch (false, 200);
					else
						victim.playerState.forceLaunch (true, 200);
				}
				hit = true;
			}

			playHit(); //The rock 'breaks' when it collides with a player.
		}
		//Rocks break each other.
		if (other.gameObject.tag == "Rock" && inMotion) {
			this.gameObject.rigidbody2D.velocity = new Vector3(0,0,0); //Kill momentum.
			hitFactory.MakeHitMarker (other.gameObject, 4); //Create a hit marker at the player's location.
			playHit(); //The rock 'breaks' when it collides with another rock.
		}
	}
}
