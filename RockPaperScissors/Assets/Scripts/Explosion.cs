/*
	Authored By: Nigel Martinez
	Purpose: Controls the explosions of screen explosions.
*/
using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
     * DESCR: Called by an animation event, the explosion's game object is destroyed.
     * PRE: NONE
     * POST: The explosion's game object is destroyed.
     */
	void Remove() {
		Destroy (this.gameObject);
	}
}
