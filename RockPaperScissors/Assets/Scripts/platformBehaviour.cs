using UnityEngine;
using System.Collections;

public class platformBehaviour : MonoBehaviour {
	
	public BoxCollider2D mainCollider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player")
			Physics2D.IgnoreCollision(mainCollider, other.gameObject.collider2D, true);

	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if(other.gameObject.transform.position.y > this.gameObject.transform.position.y)
				Physics2D.IgnoreCollision (mainCollider, other.gameObject.collider2D, false);
		}
	}
	
}
