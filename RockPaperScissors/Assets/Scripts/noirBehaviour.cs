/*
	Authored By: Jerrit Anderson & Josiah Menezes
*/
using UnityEngine;
using System.Collections;


public class noirBehaviour: PlayerState {

    private AnimatorStateInfo stateInfo;
    private int specState;// int representing the charge value
    private Player curPlayer; // Colliding player which is usually the opponent
    private bool attack; // If player currently in attack don't redo dmg for it


    // Constructor
    public noirBehaviour(Animator animation) {
        attack = false;
        flinch = false;
        specState = 0;// No spec attack by default
        anim = animation; // Getting component from unity passed in
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
    }


    override public bool checkState(Player player) {
        bool chargeState; // Player is charging set to true (Prevents player from moving while charging)

        chargeState = false;
        curPlayer = player;
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if(stateInfo.nameHash != flinchStateHash) {
            anim.SetBool("Flinch", false);
            setFlinch(false);
        }

        if(stateInfo.nameHash == idleStateHash) {
            attack = false;
        }

        if (!attack && stateInfo.nameHash == lightAttackStateHash) {
            if (lightAttack() == true)
                attack = true;
        }

        // Special attack for Noir can have 4 states of charging
        for (int i = 0; i < 4; i++) {
            
            if (stateInfo.nameHash == specStateHash[i]) {
                chargeState = true;
                specState = i + 1;
            }
        }

        if (!attack && stateInfo.nameHash == specStateHash[4]) {
            if (specialAttack(specState) == true)
                attack = true;
        }

        if (!attack && stateInfo.nameHash == heavyAttackStateHash) {
            if (heavyAttack() == true)
                attack = true;
        }

        return (attack || chargeState);
    }

    override public bool lightAttack() {
        
        if (curPlayer == null)
            return false;

        curPlayer.playerState.setFlinch(true);
        curPlayer.playerHealth.damage(50);
        return true; 
    }

    override public bool specialAttack(int curState) {
        int damage;

        damage = 0;

        if (curPlayer == null)
            return false;
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
        curPlayer.playerState.setFlinch(true);
        curPlayer.playerHealth.damage(damage);
        return true;
    }

    override public bool heavyAttack() {
        
        if (curPlayer == null)
            return false;

        curPlayer.playerState.setFlinch(true);
        curPlayer.playerHealth.damage(80);
        return true;
    }
}