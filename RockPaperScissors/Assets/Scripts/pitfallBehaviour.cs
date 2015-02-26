// pitfallBehaviour.cs
// Nigel Martinez
// GMMA Studios
//February 20th, 2015

using UnityEngine;
using System.Collections;

public class pitfallBehaviour : MonoBehaviour {
	Player victim;

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
			victim.playerHealth.damage(99999); //Instakill the player.
		}
	}
}
