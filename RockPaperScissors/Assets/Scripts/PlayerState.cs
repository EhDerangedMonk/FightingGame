/*
	Authored By: Jerrit Anderson
	Purpose: An interface that must be implemented by all behaviour classes for the game to use them
*/
using UnityEngine;
using System.Collections;


public abstract class PlayerState {

	public int lightAttackStateHash;
	public int[] specStateHash; // Can have more than one state
	public int heavyAttackStateHash;
	public int idleStateHash;
	public int flinchStateHash;
	public int blockStateHash;
	public int grappleStateHash;
	public Animator anim; // Passing in a copy of the player animation to read it
	public bool flinch; // Is the player currently unable attack due to a flinch?
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
	//public abstract bool grapple();


	/*
     * DESCR: Checks the actions a player is currently performing and
     *        does the according move
     * PRE: A Player object to compare to self
     * POST: if an attack was made true will return otherwise false
     */
	public abstract bool checkState(Player player);


	/*
	 * DESCR: getter method if the player is currently flinching
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
	 * DESCR: getter method if the player currently blocking
	 */
	public bool isBlock() {
		return block;
	}


	/*
	 * DESCR: Set the player to blocking
	 */
	public void setBlock(bool newBlock) {
		block = newBlock;
	}


	public bool isFacingLeft() {
		return facingLeft;
	}

	public void setFacingLeft(bool newFacingLeft) {
		facingLeft = newFacingLeft;
	}
}