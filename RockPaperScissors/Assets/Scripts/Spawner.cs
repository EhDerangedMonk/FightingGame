// Spawner.cs
// Nigel Martinez
// GMMA Studios
//February 10th, 2015

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

	void Spawn () {
		GameObject newRock = Resources.Load("Rock") as GameObject;
		float randomSide = Random.Range (1,10);

		if (randomSide >=5) { // Spawn on the left if randomSize is less than or equal to 5.
			Instantiate (newRock, transform.position, transform.rotation); //Spawn a rock on the left.
		}
		else { // Otherwise, spawn on the right.
			Vector3 spawnLocation = transform.position;
			spawnLocation.x *= -1;
			Instantiate (newRock, spawnLocation, transform.rotation); //Spawn a rock on the left.
		}
	}
}
