/*
	Authored By: Josiah Menezes && Jerrit Anderson
	Purpose: Class for the Character Zakir, defines his attack's effects.
*/
using UnityEngine;
using System.Collections;


public class zakirBehaviour: PlayerState {
	
	private Transform transform; // Current player coordinates - Compare to other players
	private AnimatorStateInfo stateInfo;
	//private int counterStateHash;
	private int specState;// int representing the charge value
	private Player curPlayer; // Colliding player which is usually the opponent
	private bool attack; // If player currently in attack don't redo dmg for it
	private bool canAttack; // If the player currently can attack

	//TEMP CODE - Nigel
	private HitMarkerSpawner hitFactory = GameObject.FindObjectOfType<HitMarkerSpawner> ();
	
	// Constructor
	public zakirBehaviour(Transform trans, Animator animation) {
		canAttack = true;
		attack = false;
		flinch = false;
		specState = 0;// No spec attack by default
		anim = animation; // Getting component from unity passed in
		transform = trans; // Player coordinates
		specStateHash = new int[1]; // Zakir has 1 special state
		idleStateHash = Animator.StringToHash("Base Layer.zakirIdle");
		lightAttackStateHash = Animator.StringToHash("Base Layer.zakirLightAttack");
		specStateHash[0] = Animator.StringToHash("Base Layer.zakirSpecial"); // Noir has multiple special states (Charging)
		heavyAttackStateHash = Animator.StringToHash("Base Layer.zakirHeavyAttack");
		flinchStateHash = Animator.StringToHash("Base Layer.zakirFlinch");
		grappleStateHash = Animator.StringToHash("Base Layer.zakirThrow");
		blockStateHash = Animator.StringToHash("Base Layer.zakirBlock");
		//counterStateHash = Animator.StringToHash("Base Layer.noirCounter");
		launchStateHash = Animator.StringToHash("Base Layer.zakirRecovery");
	}
	
	
	override public bool checkState(Player player) {

		curPlayer = player;
		stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		
		//**FIX should relate this code to the idle state instead refactoring
		if (stateInfo.nameHash == flinchStateHash) {
			//anim.SetBool("Flinch", false);
			canAttack = false;
			setFlinch(false);
		}
		
		if (stateInfo.nameHash == launchStateHash) {
			canAttack = false;
			setLaunch(false);
		}
		
		// Player is idling thus not attacking
		if (stateInfo.nameHash == idleStateHash) {
			canAttack = true;
			attack = false;
		}
		
		// Place holder grapple has no current use
		if (stateInfo.nameHash == grappleStateHash) {
			canAttack = false;
            if (contact() == true) {
            	attack = true;
                grapple();
            }
            
        }
		
		// If player is not already in an attack and they have triggered attack animations
		// Set attack to true and see if they are currently hitting or missing a player (If hit inflict damage)
		if (!attack && stateInfo.nameHash == lightAttackStateHash) {
			canAttack = false;
			if (contact() == true) {
				attack = true;
				lightAttack();
			}
		}


		if (!attack && stateInfo.nameHash == specStateHash[0]) {
			canAttack = false;
			if (contact() == true) {
				attack = true;
				specialAttack(specState);
			}
		}
		
		if (!attack && stateInfo.nameHash == heavyAttackStateHash) {
			canAttack = false;		
			if (contact() == true) {
				attack = true;
				heavyAttack();
			}
		}
		
		
		if (!attack && stateInfo.nameHash == blockStateHash)
			setBlock(true); // Player is blocking incoming damage
		else
			setBlock(false);
		
		
		return (attack || !canAttack); //Don't allow the player to attack again until the attack/move is finished
	}
	
	
	override public bool lightAttack() {
		
		if (curPlayer == null) {
			return false;
		} else if ((curPlayer.playerState.isBlock() && isFacingLeft() && !(curPlayer.playerState.isFacingLeft()))
		           ||  (curPlayer.playerState.isBlock() && !isFacingLeft() && curPlayer.playerState.isFacingLeft())) {
			setFlinch(true);
			curPlayer.playerState.anim.SetTrigger("Counter");
			return false;
		}

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.gameObject, 0);
		
		curPlayer.playerState.setFlinch(true);
		curPlayer.playerHealth.damage(50);
		return true; 
	}
	
	
	override public bool specialAttack(int curState) {
		int damage;
		
		damage = 0;
		
		if (curPlayer == null) {
			return false;
		} else if ((curPlayer.playerState.isBlock() && isFacingLeft() && !(curPlayer.playerState.isFacingLeft()))
		           ||  (curPlayer.playerState.isBlock() && !isFacingLeft() && curPlayer.playerState.isFacingLeft())) {
			setFlinch(true);
			curPlayer.playerState.anim.SetTrigger("Counter");
			return false;
		}

		//damgage that zakir's special does
		damage = 200;

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.gameObject, 0);

		curPlayer.playerState.setFlinch(true);
		curPlayer.playerHealth.damage(damage);
		return true;
	}
	
	
	override public bool heavyAttack() {
		
		if (curPlayer == null) {
			return false;
		} else if ((curPlayer.playerState.isBlock() && isFacingLeft() && !(curPlayer.playerState.isFacingLeft()))
		           ||  (curPlayer.playerState.isBlock() && !isFacingLeft() && curPlayer.playerState.isFacingLeft())) {
			setFlinch(true);
			curPlayer.playerState.anim.SetTrigger("Counter");
			return false;
		}

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.gameObject, 0);
		
		curPlayer.playerState.setLaunch(true);
		curPlayer.playerHealth.damage(100);
		return true;
	}

    override public bool environmentDamage(int dmg) {
        if (attack == true)
            return false;
            
        attack = true;
        curPlayer.playerHealth.damage(dmg);
        return true;
    }


	override public bool grapple() {

        if (curPlayer == null) {
            return false;
        }

        curPlayer.playerState.setFlinch(true);
        //curPlayer.playerState.setLaunch(true);
        //curPlayer.playerHealth.damage(100);
        return true;
    }
	
	// Helper Method
	private bool contact() {
		if (curPlayer == null)
			return false;

		if (this.transform.position.x < curPlayer.transform.position.x && isFacingLeft())
			return false;
		else if (this.transform.position.x > curPlayer.transform.position.x && !(isFacingLeft()))
			return false;
		else    
			return true;
	}
	
	private bool counterAttack() {
		if (curPlayer == null) {
			return false;
		} else if ((curPlayer.playerState.isBlock() && isFacingLeft() && !(curPlayer.playerState.isFacingLeft()))
		           ||  (curPlayer.playerState.isBlock() && !isFacingLeft() && curPlayer.playerState.isFacingLeft())) {
			setFlinch(true);
			curPlayer.playerState.anim.SetTrigger("Counter");
			return false;
		}
		
		
		curPlayer.playerState.setFlinch(true);
		//curPlayer.playerHealth.damage(25);
		return true;
	}
	
}