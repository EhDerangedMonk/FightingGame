using UnityEngine;
using System.Collections;


public class noirBehaviour: PlayerState {

    private AnimatorStateInfo stateInfo;
    private int specState;// int representing the charge value
    private Player curPlayer;
    private bool attack; // If player currently in attack don't redo dmg for it


    // Constructor
    public noirBehaviour(Animator animation) {
        attack = false;
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
    }

    /*
     * DESCR: Checks the actions a player is currently performing and
     *        does the according move
     * PRE: A Player object to compare to self
     * POST: if an attack was made true will return otherwise false
     */
    override public bool checkState(Player player) {
        bool chargeState; // Player is charging set to true (Prevents player from moving while charging)

        chargeState = false;
        curPlayer = player;
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if(stateInfo.nameHash == idleStateHash) {
            attack = false;
        }

        if (!attack && stateInfo.nameHash == lightAttackStateHash) {
            lightAttack();
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
            specialAttack(specState);
            attack = true;
        }

        if (!attack && stateInfo.nameHash == heavyAttackStateHash) {
            heavyAttack();
            attack = true;
        }

        return (attack || chargeState);
    }

    override public void lightAttack() {
        
        if (curPlayer == null)
            return;
        curPlayer.playerHealth.damage(50);
        Debug.Log("Light Attack for 50 damage");    
    }

    override public void specialAttack(int curState) {
        int damage;

        damage = 0;

        if (curPlayer == null)
            return;
        switch (curState) {
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

        curPlayer.playerHealth.damage(damage);
        Debug.Log("Special Attack!: Power is " + curState + "- Damage hit for " + damage);
    }

    override public void heavyAttack() {
        
        if (curPlayer == null)
            return;
        curPlayer.playerHealth.damage(80);
        Debug.Log("Heavy Attack for 80 damage");
    }
}