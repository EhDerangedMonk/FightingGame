using UnityEngine;
using System.Collections;

public class HitMarkerSpawner : MonoBehaviour {

	public AudioClip punch, sword, fire, lightning, rock;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
;
	}
	
	public void MakeHitMarker (GameObject victim, int type) {
		GameObject newHit = Resources.Load ("Hit Marker") as GameObject;
		AudioSource sound = newHit.GetComponent<AudioSource> ();

		switch (type) {
			case 0: //Punch
			sound.clip = punch;
				break;
			case 1:	//Sword
				sound.clip = sword;
				break;
			case 2: //Fire
				sound.clip = fire;
				break;
			case 3: //Lightning
				sound.clip = lightning;
				break;
			case 4: //Rock
				sound.clip = rock;
				break;
			default:
				sound.clip = punch;
				break;
		}

		Instantiate (newHit, victim.transform.position, victim.transform.rotation);
	}
}
