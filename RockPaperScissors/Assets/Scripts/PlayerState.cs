/*
    Authored By: Jerrit Anderson & Josiah Menezes
    Purpose: An interface that must be implemented by all behaviour classes for the game to use them
*/
using UnityEngine;
using System.Collections;


public abstract class PlayerState {

    public Player curPlayer; // Player that the behaviour belongs to

    public int lightAttackStateHash; // All State hashes are links to the animator
    public int[] specStateHash; // Can have more than one state
    public int heavyAttackStateHash;
    public int idleStateHash;
    public int flinchStateHash;
    public int blockStateHash;
    public int grappleStateHash;
    public int launchStateHash;
    public int runStateHash;
    public int jumpStateHash;
	public int grappleFailStateHash;
	public int grappleSuccessStateHash;

    public bool flinch; // Is the player currently unable attack due to a flinch?
    public bool launch; // Is the player launching in the air from an attack?
    public bool block; // Is the player currently blocking
    public bool facingLeft = true;// Current direction the player is facing in


    /*
     * DESCR: Performs a light attack and damages an intersecting player
     * POST: Returns true if the move intersected with a player
     */
    public abstract bool lightAttack();


    /*
     * DESCR: Performs a special attack and damages an intersecting player
     * PRE: An integer representing the current charge of the special attack
     * POST: Returns true if the move intersected with a player
     */
    public abstract bool specialAttack(int curStates);

    /*
     * DESCR: Performs a heavy attack and damages an intersecting player
     * POST: Returns true if the move intersected with a player
     */
    public abstract bool heavyAttack();


    /*
     * DESCR: Performs a grapple and tosses the player
     * POST: Returns true if the move intersected with a player
     */
    public abstract bool grapple();


    /*
     * DESCR: Checks the actions a player is currently performing and
     *        does the according move
     * PRE: A Player object to compare to self
     * POST: if an attack was made true will return otherwise false
     */
    public abstract bool checkState(Player player);


    /*
     * DESCR: Deals damage from environmental hazards to the player
     * PRE: An amount of damage
     * POST: If the damage was successful
     */
    public abstract bool environmentDamage(int dmg);


    /* 
     * DESCR: Applies force to current player in the left or right direction as a push
     * PRE: bool direction your force is coming from, intensity of force (0- 1000's)
     */
    public void sideForcePush(bool isFacingLeft, int intensity) { // 200
        if (isFacingLeft == true) {
            curPlayer.rigidbody2D.AddForce(new Vector3(-2f, 0f, 0f) * intensity); //Test force not final
        } else if (isFacingLeft == false) {
            curPlayer.rigidbody2D.AddForce(new Vector3(2f, 0f, 0f) * intensity); //Test force not final
        }
    }


    /* 
     * DESCR: Applies force to current player in the left or right direction going up
     * PRE: bool direction your force is coming from, intensity of force (0- 1000's)
     */
    public void forceLaunch(bool isFacingLeft, int intensity) { // 310
        if (isFacingLeft == true) {
            curPlayer.rigidbody2D.AddForce(new Vector3(-1.1f, 3f, 0f) * intensity); //Test force not final
        } else if (isFacingLeft == false) {
            curPlayer.rigidbody2D.AddForce(new Vector3(1.1f, 3f, 0f) * intensity); //Test force not final
        }
    }


    /*
     * DESCR: getter method if the player is currently flinching
     * POST: True (Currently flichning) - False (Not flinchings)
     */
    public bool isFlinch() {
        return flinch;
    }


    /*
     * DESCR: This is to be set by an enemy that performs a move that makes the opposing player flinch
     * PRE: are they flinching true or false
     */
    public void setFlinch(bool newFlinch) {
        flinch = newFlinch;
    }


    /*
     * DESCR: getter method if the player is currently launching
     * POST: True (Currently Launching) - False (Not Launching)
     */
    public bool isLaunch() {
        return launch;
    }


    /*
     * DESCR: This is to be set by an enemy that performs a move that makes the opposing player launch
     * PRE: are they launching true or false
     */
    public void setLaunch(bool newLaunch) {
        launch = newLaunch;
    }


    /*
     * DESCR: getter method if the player currently blocking
     */
    public bool isBlock() {
        return block;
    }


    /*
     * DESCR: Set the player to blocking
     * PRE: bool (True if the player is blocking)
     */
    public void setBlock(bool newBlock) {
        block = newBlock;
    }


    /*
     * DESCR: getter method for the direction the current player is facing
     * POST: True (Facing left) - False (Facing Right)
     */
    public bool isFacingLeft() {
        return facingLeft;
    }


    /*
     * DESCR: Set the direction the player is facing (Doesn't update what is shown on screen)
     * PRE: True (Facing left) - False (Facing Right)
     */
    public void setFacingLeft(bool newFacingLeft) {
        facingLeft = newFacingLeft;
    }
}