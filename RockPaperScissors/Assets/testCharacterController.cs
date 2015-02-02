using UnityEngine;
using System.Collections;

public class testCharacterController : MonoBehaviour {
	
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

	//presently using light attack
	bool lightAttack = false;
	AnimatorStateInfo stateInfo;
	int lightAttackStateHash = Animator.StringToHash("Base Layer.noirLightAttack");


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		stateInfo = anim.GetCurrentAnimatorStateInfo (0);
		if (stateInfo.nameHash == lightAttackStateHash)
			lightAttack = true;
		else
			lightAttack = false;

		if (lightAttack)
			return;

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		
		if (grounded)
			doubleJump = false;
		
		
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		
		
		//if (!grounded)
		//	return;
		
		float move = Input.GetAxis ("Horizontal");
		
		anim.SetFloat ("speed", Mathf.Abs (move));
		
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		
		if (move > 0 && facingLeft)
			flip ();
		else if (move < 0 && !facingLeft)
			flip ();
		
		
	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			anim.SetTrigger("Light");
		}
		//if (Input.GetKeyUp (KeyCode.Space)) {
		//	anim.SetBool ("Light", false);
		//}

		if ((grounded || !doubleJump) && Input.GetKeyDown (KeyCode.W)) 
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
