/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of the rock spawner, which creates hazards to players.
*/

using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	// Use this for initialization
	void Start () {

		InvokeRepeating("Spawn", 1, Random.Range (3,8)); // Call the Spawn function between spawn times.
	}

	// Update is called once per frame
	void Update () {
	
	}

	/*
     * DESCR: Spawns objects at randomized intervals and randomized locations.
     * PRE: NONE
     * POST: A rock is spawned on either left or right of the map, above the players.
     */
	void Spawn () {
		GameObject newRock = Resources.Load("Rock") as GameObject;
		float randomSide = Random.Range (1,10);

		// Spawn on the left if randomSize is less than or equal to 5.
		if (randomSide >=5) {
			Instantiate (newRock, transform.position, transform.rotation);
		}
		// Otherwise, spawn on the right.
		else { 
			Vector3 spawnLocation = transform.position;
			spawnLocation.x *= -1;
			Instantiate (newRock, spawnLocation, transform.rotation);
		}
	}
}
