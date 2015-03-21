using UnityEngine;
using System.Collections;

public class hitMarkerSpawner : MonoBehaviour {

	private Animator anim;
	private bool hit = true;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (anim.IsInTransition (0))
			resetHit ();
	}

	void resetHit() {
		hit = false;
	}

	void makeHitMarker () {
		if (!hit) {
			GameObject newHit = Resources.Load ("Hit Marker") as GameObject;
			Instantiate (newHit, transform.position, transform.rotation);
			hit = true;
		}
	}
}
