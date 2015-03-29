/*
	Authored By: Josiah Menezes & Jerrit Anderson
	Purpose: Generic player class, controls the default functionality given to all players.
     Main objective is to interact with unity and the animator
*/
using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {


    //** Want to turn into getters/setters
    public int layout; // Controller layout
    public int characterChoice; //player's character indicator ~Only selected once on class creation when prefab is loaded

    //** Other code need to call these classes to check behaviour of the players and change control schemes
    public Health playerHealth; // Player health representation - Needs to be public for GUI to read
    public Controls controller; // Player keyboard/game controller controls
    public PlayerState playerState; // Contains the attack behaviours and reactions of the selected player
    public Player player; //when collided check player collided with

    // Unity needs public access to assign these to the scene
    public Transform groundCheck; // object that is used to check if the player is on the ground
    public LayerMask whatIsGround;//Layer object that indicates that the player themselves and other players are not the ground, everything else is
    
	
    public Animator anim; // Stores player animation -This is read and changed to determine what the character is doing

    
    private bool grounded = false;
    private float groundRadius = 0.2f;
    private bool doubleJump = false; // If the player is currently allowed to double jump

    private bool isFlinching; // Stops player from flinching more than one until flinch is done
    private bool isLaunching; // Stops the player from launching more than once until done

    private const float maxSpeed = 5f; // limit on player run speed
    private const float jumpDelay = 0.3f;
    private float nextJump = 0.0f;


    void Start() {
        player = null; // Not colliding with a player by default
        isFlinching = false;
        isLaunching = false;

        anim = GetComponent<Animator>(); // State machine is controlled by looking at the animator
        controller = new Controls(layout);
        playerHealth = new Health(1000);

		if (characterChoice == 0) {
			playerState = new noirBehaviour();
		} else if(characterChoice == 1) {
			playerState = new zakirBehaviour();
		} else if (characterChoice == 2) {
			playerState = new violetBehaviour();
		}
    }

    // Update is called whenever the processor is free (As fast as possible)
    void Update() {
        bool attack;

        setGround();
        attack = playerState.checkState(this);

        // Disable controls if the player is dead/ otherwise accept them
        if (playerHealth.isDead() == true) {
            anim.SetBool("Death", true);
        } else  {
            
            
            if (playerState.isBlock() == false && playerState.isFlinch() == false && Input.GetKeyDown (controller.getLightKey())) {
                anim.SetTrigger("Light");
            } else if (Input.GetKeyUp (controller.getSpecialKey())) {
                anim.SetBool("Special", false);
            } else if (playerState.isBlock() == false && playerState.isFlinch() == false && Input.GetKeyDown (controller.getSpecialKey())) {
                anim.SetBool("Special", true);
            } else if (playerState.isBlock() == false && playerState.isFlinch() == false && Input.GetKeyDown (controller.getHeavyKey())) {
                anim.SetTrigger("Heavy");
			} else if (Input.GetKeyUp(controller.getBlockKey())) {
				anim.SetBool ("Block", false);
			} else if (playerState.isBlock() == false && playerState.isFlinch() == false && Input.GetKeyDown (controller.getBlockKey())) {
				anim.SetBool("Block", true);
			} else if (playerState.isBlock() == false && Input.GetKeyDown (controller.getGrappleKey())) {
				anim.SetTrigger("Grapple");
            } else if (!attack && playerState.isBlock() == false && playerState.isFlinch() == false && (Input.GetKeyDown (controller.getJumpKey()) || -0.6 >Input.GetAxis(controller.getYAxisKey()))) {
                jump(); // Controls players jumping ability
			} else if (!attack && playerState.isBlock() == false && playerState.isFlinch() == false && playerState.isLaunch() == false) {
                movement();
            }

        }

    }

    // Update is called once per frame (Controlled)
    void FixedUpdate ()  {

        if (playerHealth.isDead() == true) {
            anim.SetBool("Death", true);
            return;
        } else if (isFlinching == false && playerState.isFlinch() == true) { // Code to handle the movement of the player if they are flinching
            anim.SetTrigger("Flinch");
            isFlinching = true;
            return;
        } else if (isFlinching == true && playerState.isFlinch() == false) {
            isFlinching = false; // Animation is complete flip the variable
        } else if (isLaunching == false && playerState.isLaunch() == true) {
            anim.SetTrigger("Launch");
            isLaunching = true;
            return;
        } else if (isLaunching == true && playerState.isLaunch() == false) {
            isLaunching = false; // Animation is complete flip the variable
        }

        //** Don't look in child once update is pushed from Nigel
        Collider2D[] colliders = this.gameObject.GetComponentsInChildren<Collider2D> ();
        foreach (Collider2D collider in colliders) {
            if (collider.isTrigger && collider.enabled == false) {
                    player = null;
            }
        }

        //setGround();

        // if (playerState.checkState(player) == true) { //Controls player and their actions
        //     return; // Currently in attack if return is true so don't allow character movement
        // } else if (playerState.isFlinch() == true || playerState.isBlock() == true || playerState.isLaunch() == true) {
        //     return;
        // }

    }


    //triggers when a trigger is maintained for more than a frame
    void OnTriggerEnter2D(Collider2D other) {
        //If you're intersecting with another player you will capture their class to be able to interact with them
        if (other.gameObject.tag == "Player")
            player = (Player)other.gameObject.GetComponent(typeof(Player));
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


    /*
     * DESCR: Sets the player grounded animation if they are touching the ground
     */
    void setGround() {
        //determines if we are presently on the ground
        grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool ("Ground", grounded);
    }


    /*
     * DESCR: Checks if the player is currently jumping (Sets accordingly) + and allows the player to
     *  jump if they hit their jump key
     */
    void jump() {

        
        //if we are on the ground, refreshes our double jump
        if (grounded) { //Can't double jump unless already in air
            doubleJump = false;
        }

        //checks conditions for jumping, launches player if they can jump
        if ((grounded || !doubleJump) && Time.time > nextJump){
            nextJump = Time.time + jumpDelay;
            anim.SetBool("Ground", false);
            Vector2 v = rigidbody2D.velocity;
            rigidbody2D.velocity = new Vector2(v.x, 15);


            if(!doubleJump && !grounded)
                doubleJump = true;
        }
    }


    void movement() {
        float move; // Direction that the player is moving in forward(1) - backwards(-1) - stop(0)
        //X-axis movement input
        move = Input.GetAxis (controller.getXAxisKey());

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

}