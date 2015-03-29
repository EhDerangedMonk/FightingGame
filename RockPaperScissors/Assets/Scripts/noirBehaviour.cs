/*
	Authored By: Jerrit Anderson & Josiah Menezes
	Purpose: Class for the Character Noir, defines her attack's effects.
*/
using UnityEngine;
using System.Collections;


public class noirBehaviour: PlayerState {

    private AnimatorStateInfo stateInfo;
    private int counterStateHash;
    private int specState;// int representing the charge value
    private bool attack; // If player currently in attack don't redo dmg for it
    private bool canAttack; // If the player currently can attack

	//TEMP CODE - Nigel
	private HitMarkerSpawner hitFactory = GameObject.FindObjectOfType<HitMarkerSpawner> ();

    // Constructor
    public noirBehaviour() {
        canAttack = true;
        attack = false;
        flinch = false;
        specState = 0;// No spec attack by default
        specStateHash = new int[5]; // Noir has 4 special states
        idleStateHash = Animator.StringToHash("Base Layer.noirIdle");
        lightAttackStateHash = Animator.StringToHash("Base Layer.noirLightAttack");
        specStateHash[0] = Animator.StringToHash("Base Layer.noirSpecial1"); // Noir has multiple special states (Charging)
        specStateHash[1] = Animator.StringToHash("Base Layer.noirSpecial2");
        specStateHash[2] = Animator.StringToHash("Base Layer.noirSpecial3");
        specStateHash[3] = Animator.StringToHash("Base Layer.noirSpecial4");
        specStateHash[4] = Animator.StringToHash("Base Layer.noirSpecialEx"); // Extension when the attack is considered to hitting
        heavyAttackStateHash = Animator.StringToHash("Base Layer.noirHeavyAttack");
        flinchStateHash = Animator.StringToHash("Base Layer.noirFlinch");
        grappleStateHash = Animator.StringToHash("Base Layer.noirGrapple");
        blockStateHash = Animator.StringToHash("Base Layer.noirBlock");
        counterStateHash = Animator.StringToHash("Base Layer.noirCounter");
        launchStateHash = Animator.StringToHash("Base Layer.noirRecovery");
    }


    override public bool checkState(Player player) {
        bool chargeState; // Player is charging set to true (Prevents player from moving while charging)

        chargeState = false;
        curPlayer = player;
        stateInfo = curPlayer.anim.GetCurrentAnimatorStateInfo(0);

        //**FIX should relate this code to the idle state instead refactoring
        if (stateInfo.nameHash == flinchStateHash) {
            //curPlayer.anim.SetBool("Flinch", false);
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
        if (!attack && stateInfo.nameHash == grappleStateHash) {
            canAttack = false;
            if (contact() == true) {
                attack = true;
                grapple();
            }
            
        }

        // If player is not already in an attack and they have triggered attack animations
        // Set attack to true and see if they are currently hitting or missing a player (If hit inflict damage)
        // If player is not already in an attack and they have triggered attack animations
        // Set attack to true and see if they are currently hitting or missing a player (If hit inflict damage)
        if (!attack && stateInfo.nameHash == lightAttackStateHash) {
            canAttack = false;
            if (contact() == true) {
                attack = true;
                lightAttack();
            }
        }


        if (!attack && stateInfo.nameHash == specStateHash[4]) {
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

        if (!attack && stateInfo.nameHash == counterStateHash) {
            canAttack = false;
            if (contact() == true) {
                attack = true;
                counterAttack();
            }
        }

        // Special attack for Noir can have 4 states of charging
        for (int i = 0; i < 4; i++) {
            if (stateInfo.nameHash == specStateHash[i]) {
                canAttack = false;
                chargeState = true;
                specState = i + 1;
            }
        }



        if (!attack && stateInfo.nameHash == blockStateHash) {
            setBlock(true); // Player is blocking incoming damage
        } else {
            setBlock(false);
        }


        return (attack || chargeState || !canAttack); //Don't allow the player to attack again until the attack/move is finished
    }


    override public bool lightAttack() {
        
        if (curPlayer.player == null) {
            return false;
        } else if ((curPlayer.player.playerState.isBlock() && isFacingLeft() && !(curPlayer.player.playerState.isFacingLeft()))
            ||  (curPlayer.player.playerState.isBlock() && !isFacingLeft() && curPlayer.player.playerState.isFacingLeft())) {
            setFlinch(true);
            curPlayer.player.anim.SetTrigger("Counter");
            sideForcePush(isFacingLeft());
            sideForcePush(curPlayer.player.playerState.isFacingLeft());
            return false;
        }

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.player.gameObject, 1);

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
            sideForcePush(isFacingLeft());
            sideForcePush(curPlayer.player.playerState.isFacingLeft());
            return false;
        }

        switch (curState) { // Calculate damage based on charge of noirs special
            case 1:
                damage = 50;
                break;
            case 2:
                damage = 200;
                break;
            case 3:
                damage = 300;
                break;
            case 4:
                damage = 400;
                break;
        }

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.player.gameObject, 1);

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
            sideForcePush(isFacingLeft());
            sideForcePush(curPlayer.player.playerState.isFacingLeft());
            return false;
        }

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker(curPlayer.player.gameObject, 1);
		
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
            sideForcePush(isFacingLeft());
            sideForcePush(curPlayer.player.playerState.isFacingLeft());
            return false;
        }

		//TEMP CODE - Nigel
		hitFactory.MakeHitMarker (curPlayer.player.gameObject, 1);

        curPlayer.player.playerState.setFlinch(true);
        curPlayer.player.playerHealth.damage(25);
        curPlayer.player.playerState.sideForcePush(isFacingLeft());
        return true;
    }

}