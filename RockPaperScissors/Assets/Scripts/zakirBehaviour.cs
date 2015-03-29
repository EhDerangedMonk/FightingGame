/*
	Authored By: Josiah Menezes && Jerrit Anderson
	Purpose: Class for the Character Zakir, defines his attack's effects.
*/
using UnityEngine;
using System.Collections;


public class zakirBehaviour: PlayerState {
	
	private AnimatorStateInfo stateInfo;
	private int specState;// int representing the charge value
	private bool attack; // If player currently in attack don't redo dmg for it
	private bool canAttack; // If the player currently can attack

	//TEMP CODE - Nigel
	private HitMarkerSpawner hitFactory = GameObject.FindObjectOfType<HitMarkerSpawner> ();
	
	// Constructor
	public zakirBehaviour() {
		canAttack = true;
		attack = false;
		flinch = false;
		specState = 0; // No spec attack by default
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
		stateInfo = curPlayer.anim.GetCurrentAnimatorStateInfo(0);
		
		//**FIX should relate this code to the idle state instead refactoring
		if (stateInfo.nameHash == flinchStateHash) {
			//curPlayer.anim.SetBool("Flinch", false);
			canAttack = false;
			setFlinch(false);
		}
		
		if (stateInfo.nameHash != launchStateHash)
			setLaunch(false);
		
		// Player is idling thus not attacking
		if (stateInfo.nameHash == idleStateHash) {
			canAttack = true;
			attack = false;
		}
		
		// Place holder grapple has no current use
		if (!attack && stateInfo.nameHash == grappleStateHash) {
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
		
		if (curPlayer.player == null) {
			return false;
		} else if ((curPlayer.player.playerState.isBlock() && isFacingLeft() && !(curPlayer.player.playerState.isFacingLeft()))
		           ||  (curPlayer.player.playerState.isBlock() && !isFacingLeft() && curPlayer.player.playerState.isFacingLeft())) {
			setFlinch(true);
			curPlayer.player.anim.SetTrigger("Counter");
			sideForcePush(curPlayer.player.playerState.isFacingLeft());
			return false;
		}

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.player.gameObject, 0);
		
		curPlayer.player.playerState.setFlinch(true);
		curPlayer.player.playerHealth.damage(50);
		curPlayer.player.playerState.sideForcePush(isFacingLeft());
		return true; 
	}
	
	
	override public bool specialAttack(int curState) {
		int damage;
		
		damage = 0;
		
		if (curPlayer.player == null) {
			return false;
		} else if ((curPlayer.player.playerState.isBlock() && isFacingLeft() && !(curPlayer.player.playerState.isFacingLeft()))
		           ||  (curPlayer.player.playerState.isBlock() && !isFacingLeft() && curPlayer.player.playerState.isFacingLeft())) {
			setFlinch(true);
			curPlayer.player.anim.SetTrigger("Counter");
			sideForcePush(curPlayer.player.playerState.isFacingLeft());
			return false;
		}

		//damgage that zakir's special does
		damage = 200;

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.player.gameObject, 0);

		curPlayer.player.playerState.setFlinch(true);
		curPlayer.player.playerHealth.damage(damage);
		curPlayer.player.playerState.sideForcePush(isFacingLeft());
		return true;
	}
	
	
	override public bool heavyAttack() {
		
		if (curPlayer.player == null) {
			return false;
		} else if ((curPlayer.player.playerState.isBlock() && isFacingLeft() && !(curPlayer.player.playerState.isFacingLeft()))
		           ||  (curPlayer.player.playerState.isBlock() && !isFacingLeft() && curPlayer.player.playerState.isFacingLeft())) {
			setFlinch(true);
			curPlayer.player.anim.SetTrigger("Counter");
			sideForcePush(curPlayer.player.playerState.isFacingLeft());
			return false;
		}

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.player.gameObject, 0);
		
		curPlayer.player.playerState.setLaunch(true);
		curPlayer.player.playerHealth.damage(100);
		curPlayer.player.playerState.forceLaunch(isFacingLeft());
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

        if (curPlayer.player == null) {
            return false;
        }

        curPlayer.player.playerState.setFlinch(true);
        curPlayer.player.playerState.sideForcePush(isFacingLeft());
        return true;
    }
	
	// Helper Method
	private bool contact() {
		if (curPlayer.player == null)
			return false;


		if (curPlayer.transform.position.x < curPlayer.player.transform.position.x && isFacingLeft())
			return false;
		else if (curPlayer.transform.position.x > curPlayer.player.transform.position.x && !(isFacingLeft()))
			return false;
		else    
			return true;
	}
	
	private bool counterAttack() {
		if (curPlayer.player == null) {
			return false;
		} else if ((curPlayer.player.playerState.isBlock() && isFacingLeft() && !(curPlayer.player.playerState.isFacingLeft()))
		           ||  (curPlayer.player.playerState.isBlock() && !isFacingLeft() && curPlayer.player.playerState.isFacingLeft())) {
			setFlinch(true);
			curPlayer.player.anim.SetTrigger("Counter");
			sideForcePush(curPlayer.player.playerState.isFacingLeft());
			return false;
		}
		
		
		curPlayer.player.playerState.setFlinch(true);
		curPlayer.player.playerState.sideForcePush(isFacingLeft());
		return true;
	}
	
}