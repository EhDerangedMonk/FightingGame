using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

	public bool superLaser;
	private int speed = 20;
	private int damage;
	private bool facingLeft;
	private Collider2D collider;
	private HitMarkerSpawner hitFactory;

	public Lightning(int dmg, bool left) {
		damage = dmg;
		facingLeft = left;
	}

	// Use this for initialization
	void Start () {
		collider = this.gameObject.GetComponent<Collider2D> ();

		if (!facingLeft) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (facingLeft) {
			Vector2 pos = transform.position;
			pos.x = speed;
			transform.position = pos;
		}
		else {
			Vector2 pos = transform.position;
			pos.x = speed*-1;
			transform.position = pos;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			bool dealDamage = false;
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
						victim.playerState.forceLaunch (false, 200);
					else
						victim.playerState.forceLaunch (true, 200);
				}
				else {
					victim.playerState.setFlinch (true);
					victim.playerState.environmentDamage(damage);

					//Launch the player in the corresponding direction.
					if (this.transform.position.x > victim.transform.position.x)
						victim.playerState.sideForcePush (false, 200);
					else
						victim.playerState.sideForcePush (true, 200);
				}
			}
		}

		else
			Destroy (this.gameObject);
	}
}
