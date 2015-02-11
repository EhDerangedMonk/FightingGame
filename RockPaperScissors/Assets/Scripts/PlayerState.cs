using UnityEngine;
using System.Collections;


public abstract class PlayerState {

	public int lightAttackStateHash;
	public int[] specStateHash; // Can have more than one state
	public int heavyAttackStateHash;
	public int idleStateHash;
	public Animator anim; // Passing in a copy of the player animation to read it

	public abstract void lightAttack();
	public abstract void specialAttack(int curStates);
	public abstract void heavyAttack();
	public abstract bool checkState(Player player);
}