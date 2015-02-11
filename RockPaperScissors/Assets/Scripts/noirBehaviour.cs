using UnityEngine;
using System.Collections;

public class noirBehaviour: PlayerState {

	private AnimatorStateInfo stateInfo;
	private int specState;// int representing the charge value
	private Player curPlayer;


	// Constructor
	public noirBehaviour(Animator animation) {
		specState = 0;// No spec attack by default
		anim = animation; // Getting component from unity passed in
		specStateHash = new int[5]; // Noir has 4 special states
		lightAttackStateHash = Animator.StringToHash("Base Layer.noirLightAttack");
		specStateHash[0] = Animator.StringToHash ("Base Layer.noirSpecial1"); // Noir has multiple special states (Charging)
		specStateHash[1] = Animator.StringToHash ("Base Layer.noirSpecial2");
		specStateHash[2] = Animator.StringToHash ("Base Layer.noirSpecial3");
		specStateHash[3] = Animator.StringToHash ("Base Layer.noirSpecial4");
		specStateHash[4] = Animator.StringToHash ("Base Layer.noirSpecialEx"); // Extension when the attack is considered to hitting
		heavyAttackStateHash = Animator.StringToHash ("Base Layer.noirHeavyAttack");

		//deathStateHash = Animator.StringToHash ("Base Layer.noirDeath");
	}

	override public bool checkState(Player player) {
		bool attack;

		attack = false;
		curPlayer = player;
		stateInfo = anim.GetCurrentAnimatorStateInfo (0);

		if (stateInfo.nameHash == lightAttackStateHash) {
			lightAttack();
			attack = true;
		}

		for (int i = 0; i < 4; i++) { // Need to rework to only attack on release which is i = 4 is True
			
			if (specStateHash[i] == stateInfo.nameHash) {
				attack = true;
				specState = i + 1;
			}
		}

		if (specStateHash[4] == stateInfo.nameHash) {
			specialAttack(specState);
			attack = true;
		}

		if (stateInfo.nameHash == heavyAttackStateHash) {
			heavyAttack();
			attack = true;
		}

		return attack;
	}

	override public void lightAttack() {
		
		if (curPlayer == null)
			return;
		curPlayer.playerHealth.damage(5);
		Debug.Log("Light Attack for 5 damage");	
	}

	override public void specialAttack(int curState) {
		int damage;

		damage = 0;

		if (curPlayer == null)
			return;
		switch (curState) {
			case 1:
				damage = 10;
				break;
			case 2:
				damage = 20;
				break;
			case 3:
				damage = 50;
				break;
			case 4:
				damage = 100;
				break;
		}

		curPlayer.playerHealth.damage(damage);
		Debug.Log("Special Attack!: Power is " + curState + "- Damage hit for " + damage);
	}

	override public void heavyAttack() {
		
		if (curPlayer == null)
			return;
		curPlayer.playerHealth.damage(20);
		Debug.Log("Heavy Attack for 20 damage");
	}
}