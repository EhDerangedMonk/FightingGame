/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of one-sided platforms.
*/

using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	private InitializeStorage gameSettings;
	private PlayerOverseer overlord;
	private Transform location; //The location of the platform;
	private BoxCollider2D myCollider; //The trigger collider that checks if players are standing on top.
	private BoxCollider2D[] colliders; //The colliders of the platform.

	// Use this for initialization
	void Start () {
		location = this.GetComponentInParent<Transform> ();
		myCollider = this.GetComponent<BoxCollider2D> ();
		colliders = this.GetComponentsInParent<BoxCollider2D> ();

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		foreach (GameObject player in players) {
			Collider2D[] playerColliders = player.GetComponents<Collider2D>();

			foreach(BoxCollider2D collider in colliders) {
				if(collider != myCollider) {
					foreach(Collider2D playerCollider in playerColliders)
						Physics2D.IgnoreCollision(collider, playerCollider, true);
				}
			}
		}

		gameSettings = GameObject.FindObjectOfType<InitializeStorage> ();
		overlord = GameObject.FindObjectOfType<PlayerOverseer> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject player in overlord.allPlayers)
			checkPhaseDown (player);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Collider2D[] playerColliders = other.gameObject.GetComponents<Collider2D>();
			Transform feet = other.transform.FindChild ("groundCheck");
			
			if (feet.position.y > location.position.y) {
				foreach(BoxCollider2D collider in colliders) {
					if(collider != myCollider) {
						foreach(Collider2D playerCollider in playerColliders)
							Physics2D.IgnoreCollision(collider, playerCollider, false);
					}
				}
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Collider2D[] playerColliders = other.gameObject.GetComponents<Collider2D>();
			Transform feet = other.transform.FindChild ("groundCheck");

			if (feet.position.y > location.position.y) {
				foreach(BoxCollider2D collider in colliders) {
					if(collider != myCollider) {
						foreach(Collider2D playerCollider in playerColliders)
							Physics2D.IgnoreCollision(collider, playerCollider, false);
					}
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		Collider2D[] playerColliders = other.gameObject.GetComponents<Collider2D> ();
			foreach(BoxCollider2D collider in colliders) {
				if(collider != myCollider) {
					foreach(Collider2D playerCollider in playerColliders)
						Physics2D.IgnoreCollision(collider, playerCollider, true);
				}
			}
	}

	void checkPhaseDown(GameObject other) {
		Player player = (Player)other.gameObject.GetComponent (typeof(Player));

		if (Input.GetKeyDown (player.controller.getPhaseDownKey ()))
			other.layer = LayerMask.NameToLayer ("Phase");
		else
			other.layer = LayerMask.NameToLayer ("Player");

	}
}
