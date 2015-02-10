using UnityEngine;
using System.Collections;

public class rockBehaviour : MonoBehaviour {
	Player rockVictim;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Destroy () {
		Destroy (this.gameObject);
	}

	// Called when a 2D object collides with another 2D object
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			rockVictim = (Player)other.gameObject.GetComponent(typeof(Player));
			rockVictim.playerHealth.damage(1000); // TEMP: Rocks deal 1000DMG just to show Denis they work.
			Destroy();
		}
		/* //Rocks breaking eachother?
		if (other.gameObject.tag == "Rock") {
			Destroy();
		}
		*/
	}
}
