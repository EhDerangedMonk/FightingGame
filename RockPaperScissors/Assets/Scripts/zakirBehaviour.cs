/*
	Authored By: Josiah Menezes && Jerrit Anderson
	Purpose: Class for the Character Zakir, defines his attack's effects.
*/
using UnityEngine;
using System.Collections;


public class zakirBehaviour: PlayerState {
	
	private AnimatorStateInfo stateInfo;
	private bool attack; // If player currently in attack don't redo dmg for it

	//TEMP CODE - Nigel
	private HitMarkerSpawner hitFactory = GameObject.FindObjectOfType<HitMarkerSpawner> ();
	
	// Constructor
	public zakirBehaviour() {
		attack = false;
		flinch = false;
		specStateHash = new int[1]; // Zakir has 1 special state
		idleStateHash = Animator.StringToHash("Base Layer.zakirIdle");
		lightAttackStateHash = Animator.StringToHash("Base Layer.zakirLightAttack");
		specStateHash[0] = Animator.StringToHash("Base Layer.zakirSpecial"); // Noir has multiple special states (Charging)
		heavyAttackStateHash = Animator.StringToHash("Base Layer.zakirHeavyAttack");
		flinchStateHash = Animator.StringToHash("Base Layer.zakirFlinch");
		grappleStateHash = Animator.StringToHash("Base Layer.zakirThrow");
		blockStateHash = Animator.StringToHash("Base Layer.zakirBlock");
		runStateHash = Animator.StringToHash("Base Layer.zakirRun");
        jumpStateHash = Animator.StringToHash("Base Layer.zakirJump");
		launchStateHash = Animator.StringToHash("Base Layer.zakirRecovery");
		grappleFailStateHash = Animator.StringToHash ("Base Layer.zakirGrappleFail");
		grappleSuccessStateHash = Animator.StringToHash ("Base Layer.zakirGrappleSuccess");
	}
	
	
	override public bool checkState(Player player) {
        bool canAttack; // Is the player in a valid animation to attack

        canAttack = false;
		curPlayer = player;
		stateInfo = curPlayer.anim.GetCurrentAnimatorStateInfo(0);
		
		if (stateInfo.nameHash == flinchStateHash) {
			setFlinch(false);
		} else if (stateInfo.nameHash == launchStateHash) {
			setLaunch(false);
		} else if (stateInfo.nameHash == idleStateHash || stateInfo.nameHash == runStateHash || stateInfo.nameHash == jumpStateHash) {
			attack = false; // Player is in a valid animation to attack running or idle
            canAttack = true;
            setBlock(false);
		} else if (stateInfo.nameHash == blockStateHash) {
			setBlock(true);
		} else if (!attack && contact() == true) {
			if (stateInfo.nameHash == grappleStateHash) {
	            attack = grapple();
	        } else if (stateInfo.nameHash == lightAttackStateHash) {
				attack = lightAttack();
			} else if (stateInfo.nameHash == specStateHash[0]) {
				attack = specialAttack(0);
			} else if (stateInfo.nameHash == heavyAttackStateHash) {
				attack = heavyAttack();
			}
		} else if (attack == true && stateInfo.nameHash == grappleSuccessStateHash) {
			attack = false;
			hitFactory.MakeHitMarker (curPlayer.player.gameObject, 0);
			curPlayer.player.anim.SetTrigger("Launch");
			curPlayer.player.playerState.sideForcePush(isFacingLeft(), 120);
		}
		
		return (attack || !canAttack); //Don't allow the player to attack again until the attack/move is finished
	}
	
	
	override public bool lightAttack() {
		
		if (curPlayer.player == null || checkIfCountered(200) == true)
			return false;

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.player.gameObject, 0);
		
		curPlayer.player.playerState.setFlinch(true);
		curPlayer.player.playerHealth.damage(50);
		curPlayer.player.playerState.sideForcePush(isFacingLeft(), 200);
		return true; 
	}
	
	
	override public bool specialAttack(int curState) {
		
		if (curPlayer.player == null || checkIfCountered(200))
			return false;

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.player.gameObject, 0);

		curPlayer.player.playerState.setFlinch(true);
		curPlayer.player.playerHealth.damage(200);
		curPlayer.player.playerState.sideForcePush(isFacingLeft(), 300);
		return true;
	}
	
	
	override public bool heavyAttack() {
		
		if (curPlayer.player == null || checkIfCountered(200))
			return false;

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.player.gameObject, 0);
		
		curPlayer.player.playerState.setLaunch(true);
		curPlayer.player.playerHealth.damage(100);
		curPlayer.player.playerState.forceLaunch(isFacingLeft(), 300);
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
            curPlayer.anim.SetTrigger("GrappleFail");
            return false;
        }

        curPlayer.anim.SetTrigger("GrappleSuccess");
        curPlayer.player.playerState.setFlinch(true);
        //curPlayer.player.playerState.sideForcePush(isFacingLeft(), 180);
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
		if (curPlayer.player == null || checkIfCountered(200) == true)
			return false;
		
		
		curPlayer.player.playerState.setFlinch(true);
		curPlayer.player.playerState.sideForcePush(isFacingLeft(), 200);
		return true;
	}

    private bool checkIfCountered(int intensity) {
        if ((curPlayer.player.playerState.isBlock() && isFacingLeft() && !(curPlayer.player.playerState.isFacingLeft()))
                   ||  (curPlayer.player.playerState.isBlock() && !isFacingLeft() && curPlayer.player.playerState.isFacingLeft())) {
            setFlinch(true);
            curPlayer.player.anim.SetTrigger("Counter");
            sideForcePush(curPlayer.player.playerState.isFacingLeft(), intensity);
            return true;
        }
        return false;
    }
	
}