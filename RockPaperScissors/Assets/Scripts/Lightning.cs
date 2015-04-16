using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

	public bool superLaser;
	public int damage;
	public bool facingLeft;
	private float speed = 15;
	private HitMarkerSpawner hitFactory;

	// Use this for initialization
	void Start () {
		hitFactory = GameObject.FindObjectOfType<HitMarkerSpawner> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (facingLeft) {
			rigidbody2D.velocity = new Vector2 (-speed, 1);
		}
		else {
			rigidbody2D.velocity = new Vector2 (speed, 1);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			bool dealDamage = true;
			Player victim = (Player)other.gameObject.GetComponent (typeof(Player));
			hitFactory.MakeHitMarker (other.gameObject, 3);

			//Do not deal damage if the player is blocking.
			if (victim.playerState.isBlock ()) {
				//If the lightning bolt collides to the right of the player.
				if (this.transform.position.x > victim.transform.position.x) {
					if (!victim.playerState.isFacingLeft ())
						dealDamage = false;
				}
				//If the rock collides to the left of the player.
				else {
					if (victim.playerState.isFacingLeft ())
						dealDamage = false;
				}
			}
			//Otherwise, deal damage.
			if (dealDamage) {
				if (superLaser) {
					victim.playerState.setLaunch (true);
					victim.playerState.environmentDamage(damage); // Rocks deal 100DMG
				
					//Launch the player in the corresponding direction.
					if (this.transform.position.x > victim.transform.position.x)
						victim.playerState.forceLaunch (true, 20);
					else
						victim.playerState.forceLaunch (false, 20);
				}
				else {
					victim.playerState.setFlinch (true);
					victim.playerState.environmentDamage(damage);

					//Launch the player in the corresponding direction.
					if (this.transform.position.x > victim.transform.position.x)
						victim.playerState.sideForcePush (true, 200);
					else
						victim.playerState.sideForcePush (false, 200);
					Destroy (this.gameObject);
				}
			}
		}

		else
			Destroy (this.gameObject);
	}
}
