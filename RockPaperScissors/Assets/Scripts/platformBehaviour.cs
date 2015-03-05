/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of one-sided platforms.
*/

using UnityEngine;
using System.Collections;

public class platformBehaviour : MonoBehaviour {
	
	public BoxCollider2D mainCollider;
	private bool oneWay = false;
	private Transform feet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		mainCollider.enabled = oneWay;
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			feet = other.transform.FindChild ("groundCheck");
			
			if (feet.position.y > mainCollider.transform.position.y)
				oneWay = true;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			feet = other.transform.FindChild ("groundCheck");

			if (feet.position.y > mainCollider.transform.position.y)
				oneWay = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		oneWay = false;
	}
	
}
