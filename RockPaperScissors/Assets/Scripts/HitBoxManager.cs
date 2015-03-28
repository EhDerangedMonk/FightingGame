/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of player's attack hit boxes.
*/
using UnityEngine;
using System.Collections;

public class HitBoxManager : MonoBehaviour {

	private Collider2D hitBox;
	private Animator anim;

	// Use this for initialization
	void Start () {
		//Find the attack hit box.
		anim = this.gameObject.GetComponent<Animator> ();
		Collider2D[] colliders = this.gameObject.GetComponentsInChildren<Collider2D> ();

		foreach (Collider2D collider in colliders) {
			if(collider.isTrigger)
				hitBox = collider;
		}
		//Disable the hitbox.
		disableHit ();

	}
	
	// Update is called once per frame
	void Update () {
		if (anim.IsInTransition(0))
		    disableHit();
	}

	/*
     * DESCR: Called by an animation event, the hitbox is enabled.
     * PRE: NONE
     * POST: The hitbox is enabled.
     */
	void enableHit() {
		hitBox.enabled = true;
	}

	/*
     * DESCR: Called by an animation event, the hitbox is disabled.
     * PRE: NONE
     * POST: The hitbox is disabled.
     */
	void disableHit() {
		hitBox.enabled = false;
	}
	
}
