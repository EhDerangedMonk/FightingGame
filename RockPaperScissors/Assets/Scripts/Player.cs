using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {

	public int layout; // 1 - player 1 / 2 - player 2 <- Gets assigned in Unity right now
	public Health playerHealth; // Player health representation
	
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float jumpForce = 300f;
	public float maxSpeed = 3f;
	public bool facingLeft = true; // Current direction the player is facing in

	private PlayerState playerState; // Generic state that implements state and behaviour
	private Animator anim; // Stores player animation
	private Controls controller;
	private Player player; //when collided check player collided with
	
	private bool grounded = false;
	private float groundRadius = 0.2f;
	private bool doubleJump = false;
	private float slideSpeed = 200; // Speed characters slide at when pushed
	
	void Start() {
		player = null; // Not colliding with a player by default
		anim = GetComponent<Animator> ();
		controller = new Controls(layout);
		playerHealth = new Health();
		playerState = new noirBehaviour(anim);
	}


	void Update() {

	}

	// Update is called once per frame
	void FixedUpdate ()  {
		float move;

		if (playerHealth.dead() == true) { // Temporary I have no death state?
			anim.SetBool("Death", true);
			return;
		}

		// Generic keyboard input
		if (Input.GetKeyDown (controller.light)) {
			anim.SetTrigger("Light");
		}
		if (Input.GetKeyUp (controller.special)) {
			anim.SetBool("Special", false);
		}
		if (Input.GetKeyDown (controller.special)) {
			anim.SetBool("Special", true);
		}
		if (Input.GetKeyDown (controller.heavy)) {
			anim.SetTrigger("Heavy");
		}


	
		if (playerState.checkState(player)) { //Controls player and their actions
			return; // Currently in attack if return is true so don't allow character movement
		}

		if ((grounded || !doubleJump) && Input.GetKeyDown (controller.jump)) 
		{
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
			
			if(!doubleJump && !grounded)
				doubleJump = true;
		}

		
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		
		if (grounded) { //Can't double jump unless already in air
			doubleJump = false;
		}

		// Keyboard input
		if (Input.GetKey (controller.left)) {
			move = -1;
		} else if (Input.GetKey (controller.right)) {
			move = 1;
		} else {
			move = 0;
		}
		
		
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);	
		anim.SetFloat ("speed", Mathf.Abs (move));
		
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		
		if (move > 0 && facingLeft)
			flip();
		else if (move < 0 && !facingLeft)
			flip();

	}

	void OnTriggerStay2D(Collider2D other) {

		// Vector3 forward = new Vector3 (0, 0, 0);

		// 	if (other.transform.position.x > this.transform.position.x) {
		// 		forward = new Vector3 (1, 0, 0);
		// 	} else {
		// 		forward = new Vector3 (-1, 0, 0);
		// 	}

		// other.attachedRigidbody.AddForce (forward * slideSpeed);

	}

	// Called when a 2D object collides with another 2D object
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			player = (Player)other.gameObject.GetComponent(typeof(Player));
			//Debug.Log("My x is " + this.transform.position.x + ": their x is " + other.transform.position.x + ": I am facingLeft " + facingLeft);
			if (this.transform.position.x < other.transform.position.x && facingLeft) {
				player = null;
			} else if (this.transform.position.x > other.transform.position.x && !facingLeft) {
				player = null;
			}
		}
	}

	// void OnCollisionStay2D(Collision2D other) {
	// 	if (other.gameObject.tag == "Player") {
	// 		player = (Player)other.gameObject.GetComponent(typeof(Player));
	// 		//Debug.Log("My x is " + this.transform.position.x + ": their x is " + other.transform.position.x + ": I am facingLeft " + facingLeft);
	// 		if (this.transform.position.x < other.transform.position.x && facingLeft) {
	// 			player = null;
	// 		} else if (this.transform.position.x > other.transform.position.x && !facingLeft) {
	// 			player = null;
	// 		}
	// 	}
	// }

	// Called when a 2D object stops colliding with another 2D object
	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			player = null;
		}
	}

	// Reverses character direction
	void flip() {
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}