using UnityEngine;
using System.Collections;

public class noir2CharacterController : MonoBehaviour {
	
	public float maxSpeed = 3f;
	bool facingLeft = true;
	
	Animator anim;
	
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 300f;
	
	//used or not used
	bool doubleJump = false;
	
	//presently using attack
	bool attack = false;
	AnimatorStateInfo stateInfo;
	int lightAttackStateHash = Animator.StringToHash("Base Layer.noirLightAttack");
	int specState1Hash = Animator.StringToHash ("Base Layer.noirSpecial1");
	int specState2Hash = Animator.StringToHash ("Base Layer.noirSpecial2");
	int specState3Hash = Animator.StringToHash ("Base Layer.noirSpecial3");
	int specState4Hash = Animator.StringToHash ("Base Layer.noirSpecial4");
	int specStateExHash = Animator.StringToHash ("Base Layer.noirSpecialEx");
	int heavyAttackStateHash = Animator.StringToHash ("Base Layer.noirHeavyAttack");
	
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		attack = false;
		stateInfo = anim.GetCurrentAnimatorStateInfo (0);
		if (stateInfo.nameHash == lightAttackStateHash)
			attack = true;
		if (stateInfo.nameHash == specState1Hash ||stateInfo.nameHash == specState2Hash ||stateInfo.nameHash == specState3Hash ||stateInfo.nameHash == specState4Hash)
			attack = true;
		if (stateInfo.nameHash == specStateExHash)
			attack = true;
		if (stateInfo.nameHash == heavyAttackStateHash)
			attack = true;
		
		if (attack)
			return;
		
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		
		if (grounded)
			doubleJump = false;
		
		
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		
		
		//if (!grounded)
		//	return;
		float move;
		if (Input.GetKey (KeyCode.J))
						move = -1;
				else if (Input.GetKey (KeyCode.L))
						move = 1;
				else
						move = 0;
		 //= Input.GetAxis ("Horizontal");
		
		anim.SetFloat ("speed", Mathf.Abs (move));
		
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		
		if (move > 0 && facingLeft)
			flip ();
		else if (move < 0 && !facingLeft)
			flip ();
		
		
	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.O)) {
			anim.SetTrigger("Light");
		}
		if (Input.GetKeyUp (KeyCode.Semicolon)) {
			anim.SetBool("Special", false);
		}
		if (Input.GetKeyDown (KeyCode.Semicolon)) {
			anim.SetBool("Special", true);
		}
		if (Input.GetKeyDown (KeyCode.Quote)) {
			anim.SetTrigger("Heavy");
		}
		
		
		if ((grounded || !doubleJump) && Input.GetKeyDown (KeyCode.I)) 
		{
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
			
			if(!doubleJump && !grounded)
				doubleJump = true;
		}
		
	}
	
	void flip(){
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
