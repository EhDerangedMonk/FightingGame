/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of lava hazards.
*/

using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {
	private Player victim;
	private float launchForce = 1000f;
	private AudioSource sound;
	
	// Use this for initialization
	void Start () {
		sound = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Called when a 2D object collides with another 2D object
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			victim = (Player)other.gameObject.GetComponent(typeof(Player));

			if(victim.playerHealth.isDead() == false) {
				sound.Play ();
				victim.playerState.setLaunch (true);
				victim.rigidbody2D.AddForce(new Vector2(0, launchForce));
				victim.playerHealth.damage(100); //Deal 100 to the player.
			}
		}
	}
}
