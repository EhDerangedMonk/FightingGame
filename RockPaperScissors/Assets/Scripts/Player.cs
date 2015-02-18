using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {

    private const float jumpForce = 700f;
    private const float maxSpeed = 5f;

    public int layout; // 1 - player 1 / 2 - player 2 <- Gets assigned in Unity right now
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Health playerHealth; // Player health representation - Needs to be public for GUI to read

    private bool facingLeft = true;// Current direction the player is facing in

    private PlayerState playerState; // Generic state that implements state and behaviour
    private Animator anim; // Stores player animation
    private Controls controller; // Player keyboard/game controller controls
    private Player player; //when collided check player collided with
    
    private bool grounded = false;
    private float groundRadius = 0.2f;
    private bool doubleJump = false; // If the player is currently allowed to double jump
    private float slideSpeed = 200; // Speed characters slide at when standing on someone's head
    
    void Start() {
        player = null; // Not colliding with a player by default
        anim = GetComponent<Animator>();
        controller = new Controls(layout);
        playerHealth = new Health(1000);
        playerState = new noirBehaviour(anim);
    }

    // Update is called whenever the processor is free (As fast as possible)
    void Update() {

        // Disable controls if the player is dead/ otherwise accept them
        if (playerHealth.isDead() == true) {
            anim.SetBool("Death", true);
        } else {
            if (Input.GetKeyDown (controller.light)) {
                anim.SetTrigger("Light");
            } else if (Input.GetKeyUp (controller.special)) {
                anim.SetBool("Special", false);
            } else if (Input.GetKeyDown (controller.special)) {
                anim.SetBool("Special", true);
            } else if (Input.GetKeyDown (controller.heavy)) {
                anim.SetTrigger("Heavy");
			} else if (Input.GetKeyUp (controller.block)) {
				anim.SetBool ("Block", false);
			} else if (Input.GetKeyDown (controller.block)) {
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

        if (playerState.checkState(player)) { //Controls player and their actions
            return; // Currently in attack if return is true so don't allow character movement
        }

        // Keyboard input
        if (Input.GetKey (controller.left)) {
            move = -1;
        } else if (Input.GetKey (controller.right)) {
            move = 1;
        } else {
            move = 0;
        }
        
        
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);   
        anim.SetFloat("speed", Mathf.Abs (move));
        
        rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
        
        if (move > 0 && facingLeft)
            flip();
        else if (move < 0 && !facingLeft)
            flip();

    }


    //triggers when someone would stand on another person's head
    void OnTriggerStay2D(Collider2D other) {
        
        Vector3 forward = new Vector3 (0f, 0f, 0.0f);
        

        //check to ensure they are a player
        if (other.gameObject.tag == "Player") {
            player = (Player)other.gameObject.GetComponent(typeof(Player));
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
        }
        
    }

    // Called when a 2D object collides with another 2D object
    void OnCollisionEnter2D(Collision2D other) {
        // When a Player object collides with another Player object
        if (other.gameObject.tag == "Player") {
            player = (Player)other.gameObject.GetComponent(typeof(Player));
            if (this.transform.position.x < other.transform.position.x && facingLeft) {
                player = null;
            } else if (this.transform.position.x > other.transform.position.x && !facingLeft) {
                player = null;
            }
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        // When a Player object collides with another Player object
        if (other.gameObject.tag == "Player") {
            player = (Player)other.gameObject.GetComponent(typeof(Player));
            if (this.transform.position.x < other.transform.position.x && facingLeft) {
                player = null;
            } else if (this.transform.position.x > other.transform.position.x && !facingLeft) {
                player = null;
            }
        }
    }

    // Called when a 2D object stops colliding with another 2D object
    void OnCollisionExit2D(Collision2D other) {

        // When a Player object stops colliding with another Player object
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