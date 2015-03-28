/*
	Authored By: Nigel Martinez
	Purpose: Creates hit markers, indicating that attacks or hazards made contact.
*/
using UnityEngine;
using System.Collections;

public class HitMarkerSpawner : MonoBehaviour {

	public AudioClip punch, sword, fire, lightning, rock;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	/*
     * DESCR: Creates a hit marker at a location on the map.
     * PRE: The game object of the player getting hit and the integer representing the hit marker type.
     * 0 - Punch, 1 - Sword, 2 - Fire
     * 3 - Lightning, 4 - Rock
     * POST: A hit marker is created on the map, playing a sound akin to its type.
     */
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
