// lavaBehaviour.cs
// Nigel Martinez
// GMMA Studios
//February 18th, 2015

using UnityEngine;
using System.Collections;

public class lavaBehaviour : MonoBehaviour {
	Player victim;
	float launchForce = 1000f;
	
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

			if(victim.playerHealth.isDead() == false) {
				victim.rigidbody2D.AddForce(new Vector2(0, launchForce));
				victim.playerHealth.damage(100); //Deal 100 to the player.
			}
		}
	}
}
