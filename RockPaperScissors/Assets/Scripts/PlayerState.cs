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
	public Animator anim; // Passing in a copy of the player animation to read it
	public bool flinch; // Is the player currently unable attack due to a flinch?


	/*
	 * DESCR: Performs a light attack and damages an intersecting player
	 */
	public abstract bool lightAttack();


	/*
	 * DESCR: Performs a special attack and damages an intersecting player
	 * PRE: An integer representing the current charge of the special attack
	 */
	public abstract bool specialAttack(int curStates);

	/*
	 * DESCR: Performs a heavy attack and damages an intersecting player
	 */
	public abstract bool heavyAttack();


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
}