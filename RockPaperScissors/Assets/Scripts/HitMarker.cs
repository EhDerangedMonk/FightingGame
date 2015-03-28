/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of hit markers.
*/
using UnityEngine;
using System.Collections;

public class HitMarker : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	/*
     * DESCR: Called by an animation event, the hit marker's game object is destroyed.
     * PRE: NONE
     * POST: The hit marker's game object is destroyed.
     */
	void Remove() {
		Destroy (this.gameObject);
	}
}
