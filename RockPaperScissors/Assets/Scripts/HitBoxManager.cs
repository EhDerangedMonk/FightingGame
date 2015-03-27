using UnityEngine;
using System.Collections;

public class HitBoxManager : MonoBehaviour {

	private Collider2D hitBox;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
		Collider2D[] colliders = this.gameObject.GetComponentsInChildren<Collider2D> ();

		foreach (Collider2D collider in colliders) {
			if(collider.isTrigger)
				hitBox = collider;
		}

		disableHit ();

	}
	
	// Update is called once per frame
	void Update () {
		if (anim.IsInTransition(0))
		    disableHit();
	}

	void enableHit() {
		hitBox.enabled = true;
	}

	void disableHit() {
		hitBox.enabled = false;
	}
	
}
