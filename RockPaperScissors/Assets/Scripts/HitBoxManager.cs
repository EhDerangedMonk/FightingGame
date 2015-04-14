/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of player's attack hit boxes.
*/
using UnityEngine;
using System.Collections;

public class HitBoxManager : MonoBehaviour {

	private BoxCollider2D hitBox;
	private Animator anim;

	// Use this for initialization
	void Start () {
		//Find the attack hit box.
		anim = this.gameObject.GetComponent<Animator> ();
		BoxCollider2D[] colliders = this.gameObject.GetComponentsInChildren<BoxCollider2D> ();

		foreach (BoxCollider2D collider in colliders) {
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
     * PRE: A float representing the size of the hitbox is entered.
     * POST: The hitbox is enabled, using the size value as the x dimension.
     */
	void enableHit(float size) {
		hitBox.enabled = true;
		hitBox.size = new Vector2 (size, 1);
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
