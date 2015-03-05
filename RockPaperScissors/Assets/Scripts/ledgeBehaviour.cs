/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of "hanging state" triggers on maps.
*/

using UnityEngine;
using System.Collections;

public class ledgeBehaviour : MonoBehaviour {

	private bool rightLedge = false;
	private bool hang = false;

	// Use this for initialization
	void Start () {
		if (this.name == "leftHangCheck")
			rightLedge = false;
		else
			rightLedge = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (hang)
			print ("Hang triggered.");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player")
			hang = true;

	}
	
	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.tag == "Player")
			hang = true;
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "Player")
			hang = false;
	}

}
