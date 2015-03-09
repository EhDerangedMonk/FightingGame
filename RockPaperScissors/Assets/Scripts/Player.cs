/*
	Authored By: Josiah Menezes & Jerrit Anderson
	Purpose: Generic player class, controls the default functionality given to all players.
*/
using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {

    private const float maxSpeed = 5f; // limit on player run speed


    public int layout; // 1 - player 1 / 2 - player 2 <- Gets assigned in Unity right now
    public Transform groundCheck; // object that is used to check if the player is on the ground
    public LayerMask whatIsGround;//Layer object that indicates that the player themselves and other players are not the ground, everything else is
    public Health playerHealth; // Player health representation - Needs to be public for GUI to read
	public int cChangeKey; //player# indicator

    public PlayerState playerState; // Generic state that implements state and behaviour
    private Animator anim; // Stores player animation
    private Controls controller; // Player keyboard/game controller controls
    private Player player; //when collided check player collided with
    
    private bool grounded = false;
    private float groundRadius = 0.2f;
    private bool doubleJump = false; // If the player is currently allowed to double jump
    private float slideSpeed = 10; // Speed characters slide at when standing on someone's head

    private bool isFlinching; // Stops player from flinching more than one until flinch is done

	float nextJump = 0.0f;
	float jumpDelay = 0.3f;
    
    void Start() {
        player = null; // Not colliding with a player by default
        isFlinching = false;
        anim = GetComponent<Animator>();
        controller = new Controls(layout, cChangeKey);
        playerHealth = new Health(1000);
        playerState = new noirBehaviour(anim);
    }

    // Update is called whenever the processor is free (As fast as possible)
    void Update() {

        // Disable controls if the player is dead/ otherwise accept them
        if (playerHealth.isDead() == true) {
            anim.SetBool("Death", true);
        } else {
			//input for player controls
            if (playerState.isBlock() == false && playerState.isFlinch() == false && Input.GetKeyDown (controller.light)) {
                anim.SetTrigger("Light");
            } else if (Input.GetKeyUp (controller.special)) {
                anim.SetBool("Special", false);
            } else if (playerState.isBlock() == false && playerState.isFlinch() == false && Input.GetKeyDown (controller.special)) {
                anim.SetBool("Special", true);
            } else if (playerState.isBlock() == false && playerState.isFlinch() == false && Input.GetKeyDown (controller.heavy)) {
                anim.SetTrigger("Heavy");
			} else if (Input.GetKeyUp(controller.block)) {
				anim.SetBool ("Block", false);
			} else if (playerState.isBlock() == false && playerState.isFlinch() == false && Input.GetKeyDown (controller.block)) {
				anim.SetBool("Block", true);
			} else if (Input.GetKeyDown (controller.grapple)) {
				anim.SetTrigger("Grapple");
			}

        }

    }

    // Update is called once per frame (Controlled)
    void FixedUpdate ()  {
        float move; // Direction that the player is moving in forward(1) - backwards(-1) - stop(0)

        if (playerHealth.isDead() == true) { // Temporary I have no death state?
            anim.SetBool("Death", true);
            return;
        }  else if (isFlinching == false && playerState.isFlinch() == true) {
            anim.SetBool("Flinch", true);
            //anim.SetBool("Special", false);
            
            if (playerState.isFacingLeft()) {
                rigidbody2D.AddForce(new Vector3(1f, 0f, 0f) * 200); //Test force not final
            } else {
                rigidbody2D.AddForce(new Vector3(-1f, 0f, 0f) * 200); //Test force not final
            }
            isFlinching = true;
            return;
        } else if (isFlinching == true && playerState.isFlinch() == false) {
            isFlinching = false;
        }


		//
		if (Input.GetKeyDown(controller.cChangeKey)){
			layout = ((layout %4)+1); //mod number must always be equal to number of possible control schemes
			controller.changeControls(layout);
		}
        
        //determines if we are presently on the ground
        grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool ("Ground", grounded);
    
        //if we are on the ground, refreshes our double jump
        if (grounded) { //Can't double jump unless already in air
            doubleJump = false;
        }

        if (playerState.checkState(player)) { //Controls player and their actions
            return; // Currently in attack if return is true so don't allow character movement
        } else if (isFlinching == true || playerState.isBlock()) {
            return;
        }

		//checks conditions for jumping, launches player if they can jump
       // if ((grounded || !doubleJump) && Input.GetKeyDown (controller.jump)) 
		  if ((grounded || !doubleJump) && -0.6>Input.GetAxis(controller.YAxis) && Time.time > nextJump)
        {
			nextJump = Time.time + jumpDelay;
            anim.SetBool("Ground", false);
			Vector2 v = rigidbody2D.velocity;
			rigidbody2D.velocity = new Vector2(v.x, 15);


            if(!doubleJump && !grounded)
                doubleJump = true;
        }
		

		//X-axis movement input
		move = Input.GetAxis (controller.XAxis);
        
        //sets variables for vertical speed and horizontal speed
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);   
        anim.SetFloat("speed", Mathf.Abs (move));
        
        rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
        
		//determines which way we should be facing and flips sprite towards the appropriate direction
        if (move > 0 && playerState.isFacingLeft())
            flip();
        else if (move < 0 && !(playerState.isFacingLeft()))
            flip();

    }


    //triggers when a trigger is maintained for more than a frame
    void OnTriggerStay2D(Collider2D other) {
        
        Vector3 forward = new Vector3 (0f, 0f, 0.0f);

        //check to ensure they are a player
        if (other.gameObject.tag == "Player") {
            player = (Player)other.gameObject.GetComponent(typeof(Player));

            //THE CODE TO STOP PEOPLE FROM STANDING ON EACH OTHER'S HEADS
            //finds out which player is on the other
            if (player.gameObject.transform.position.y >this.gameObject.transform.position.y) {
                //pushes you in the direction you are closest to not colliding with
                if(player.gameObject.transform.position.x >this.gameObject.transform.position.x) {
                    forward = new Vector3(1f, 0f, 0f);
                    player.rigidbody2D.AddForce (forward * slideSpeed);
                }
                else{
                    forward = new Vector3(-1f, 0f, 0f);
                    player.rigidbody2D.AddForce (forward * slideSpeed);
                }
            }

            if (this.transform.position.x < other.transform.position.x && playerState.isFacingLeft()) {
                player = null;
            } else if (this.transform.position.x > other.transform.position.x && !(playerState.isFacingLeft())) {
                player = null;
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D other) {
        // When a Player object stops colliding with another Player object
        Debug.Log("Exit Called");
        if (other.gameObject.tag == "Player") {
            Debug.Log("Trigger Exit Player");
            player = null;
        }
    }

	/*
     * DESCR: Flips the horizontal direction the sprite is facing.
     * PRE: Player Character's sprite and the direction it is facing
     * POST: Player Character's sprite is facing the opposite direction they were facing
     */
	 void flip() {
        playerState.setFacingLeft(!(playerState.isFacingLeft()));
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}