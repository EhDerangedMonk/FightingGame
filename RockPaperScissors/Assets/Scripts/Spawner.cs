using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public float spawnTime = 5f; // The amount of time between each spawn.
	public float spawnDelay = 3f; // The amount of time before spawning starts.
	public bool onLeft = true; // The location of the spawner in relation to the map.

	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", spawnDelay, spawnTime); // Call the Spawn function between spawn times.
	}

	// Update is called once per frame
	void Update () {
	
	}

	void Spawn () {
		GameObject newRock = Resources.Load("Rock") as GameObject;

		if (onLeft) {
			onLeft = false;
			Instantiate (newRock, transform.position, transform.rotation); //Spawn a rock on the left.
		}
		else {
			Vector3 spawnLocation = transform.position;
			spawnLocation.x *= -1;

			onLeft = true;
			Instantiate (newRock, spawnLocation, transform.rotation); //Spawn a rock on the left.
		}
	}
}
