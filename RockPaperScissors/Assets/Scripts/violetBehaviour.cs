﻿/*
	Authored By: Jerrit Anderson & Josiah Menezes
	Purpose: Class for the Character Violet, defines her attack's effects.
*/
using UnityEngine;
using System.Collections;


public class violetBehaviour: PlayerState {
	
	private AnimatorStateInfo stateInfo;
	//private int counterStateHash;
	private int specState;// int representing the charge value
	private bool attack; // If player currently in attack don't redo dmg for it

	private HitMarkerSpawner hitFactory = GameObject.FindObjectOfType<HitMarkerSpawner> ();
	
	// Constructor
	public violetBehaviour() {
		attack = false;
		flinch = false;
		specState = 0;// No spec attack by default
		specStateHash = new int[4]; // Noir has 4 special states
		idleStateHash = Animator.StringToHash("Base Layer.violetIdle");
		lightAttackStateHash = Animator.StringToHash("Base Layer.violetLightAttack");
		specStateHash[0] = Animator.StringToHash("Base Layer.violetSpecial1"); // Noir has multiple special states (Charging)
		specStateHash[1] = Animator.StringToHash("Base Layer.violetSpecial2");
		specStateHash[2] = Animator.StringToHash("Base Layer.violetSpecial3");
		specStateHash[3] = Animator.StringToHash("Base Layer.violetSpecialEX"); // Extension when the attack is considered to hitting
		heavyAttackStateHash = Animator.StringToHash("Base Layer.violetHeavyAttack");
		flinchStateHash = Animator.StringToHash("Base Layer.violetFlinch");
		grappleStateHash = Animator.StringToHash("Base Layer.violetGrapple");
		blockStateHash = Animator.StringToHash("Base Layer.violetBlock");
		launchStateHash = Animator.StringToHash("Base Layer.violetRecovery");
		runStateHash = Animator.StringToHash("Base Layer.violetRun");
        jumpStateHash = Animator.StringToHash("Base Layer.violetJump");
		grappleFailStateHash = Animator.StringToHash ("Base Layer.violetGrappleFail");
		grappleSuccessStateHash = Animator.StringToHash ("Base Layer.violetGrappleSuccess");
	}
	
	
	override public bool checkState(Player player) {
		bool canAttack; // Is the player in a valid animation to attack

        canAttack = false;
		curPlayer = player;
		stateInfo = curPlayer.anim.GetCurrentAnimatorStateInfo(0);
		
		if (stateInfo.nameHash == flinchStateHash) {
			setFlinch(false);
		} else if (stateInfo.nameHash == idleStateHash || stateInfo.nameHash == runStateHash || stateInfo.nameHash == jumpStateHash) {
			attack = false; // Player is in a valid animation to attack running or idle
            canAttack = true;
            setBlock(false);
			setLaunch(false);
        } else if (!attack && stateInfo.nameHash == blockStateHash) {
				attack = true;
				setBlock(true);
				sideForcePush(!facingLeft, 250);
		} else if (!attack && contact() == true) {
			if (stateInfo.nameHash == grappleStateHash) {
	            attack = grapple(); 
	        } else if (stateInfo.nameHash == lightAttackStateHash) {
				attack = lightAttack();
			}  else if (stateInfo.nameHash == heavyAttackStateHash) {
				attack = heavyAttack();
			}
		} else if (attack == true && stateInfo.nameHash == grappleSuccessStateHash) {
			hitFactory.MakeHitMarker (curPlayer.player.gameObject, 2);
            attack = false;
            curPlayer.player.playerState.setLaunch(true);
            curPlayer.player.playerState.forceLaunch(isFacingLeft(), 300);
		} else {
			// Special attack for Violet can have 4 states of charging
			for (int i = 0; i < 3; i++) {
				if (stateInfo.nameHash == specStateHash[i]) {
					canAttack = false;
					specState = i + 1;
				}
			}
			if (!attack && stateInfo.nameHash == specStateHash[3]) {
				attack = specialAttack(specState);
			}
		}
		
		return (attack || !canAttack); //Don't allow the player to attack again until the attack/move is finished		
	}
	
	
	override public bool lightAttack() {
		
		if (curPlayer.player == null || checkIfCountered(200) == true)
			return false;
		
		hitFactory.MakeHitMarker (curPlayer.player.gameObject, 2);
		
		curPlayer.player.playerState.setFlinch(true);
		curPlayer.player.playerHealth.damage(50);
		curPlayer.player.playerState.sideForcePush(isFacingLeft(), 200);
		return true; 
	}
	
	
	override public bool specialAttack(int curState) {
		GameObject newBolt;
		
		switch (curState) { // Calculate damage based on charge of Violet's special
		case 1:
			newBolt = Resources.Load ("Weak Lightning") as GameObject;
			newBolt.GetComponent<Lightning>().damage = 25;
			newBolt.GetComponent<Lightning>().facingLeft = facingLeft;
			break;
		case 2:
			newBolt = Resources.Load ("Medium Lightning") as GameObject;
			newBolt.GetComponent<Lightning>().damage = 100;
			newBolt.GetComponent<Lightning>().facingLeft = facingLeft;
			break;
		case 3:
			newBolt = Resources.Load ("Strong Lightning")as GameObject;
			newBolt.GetComponent<Lightning>().damage = 200;
			newBolt.GetComponent<Lightning>().facingLeft = facingLeft;
			break;
		case 4:
			newBolt = Resources.Load ("Strong Lightning")as GameObject;
			newBolt.GetComponent<Lightning>().damage = 25;
			newBolt.GetComponent<Lightning>().facingLeft = facingLeft;
			break;
		default:
			newBolt = Resources.Load ("Weak Lightning") as GameObject;
			newBolt.GetComponent<Lightning>().damage = 25;
			newBolt.GetComponent<Lightning>().facingLeft = facingLeft;
			break;
		}

		Vector3 newPos = curPlayer.transform.position;
		if (facingLeft) {
			newPos.x = newPos.x - 1;
			MonoBehaviour.Instantiate (newBolt, newPos, curPlayer.transform.rotation);
		} 
		else {
			newPos.x = newPos.x + 1;
			MonoBehaviour.Instantiate (newBolt, newPos, Quaternion.Euler (0,0,180));
		}

		return true;
	}
	
	
	override public bool heavyAttack() {
		
		if (curPlayer.player == null || checkIfCountered(200) == true)
			return false;
		
		hitFactory.MakeHitMarker (curPlayer.player.gameObject, 1);
		
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