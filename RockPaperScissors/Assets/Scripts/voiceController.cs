/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of a character's audio source, or "voice".
*/
using UnityEngine;
using System.Collections;

public class voiceController : MonoBehaviour {

	public AudioClip lightAttack, heavyAttack, specialAttack, flinch, launched, death;

	private AudioSource voice;
	private Animator anim;

	// Use this for initialization
	void Start () {
		voice = this.audio;
		anim = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (anim.IsInTransition(0) && !voice.isPlaying)
			reset ();
	}

	/*
     * DESCR: Called by an animation event, the voice stops playing sounds.
     * PRE: NONE
     * POST: The voice clip is null.
     */
	void reset() {
		if(voice != null)
			voice.clip = null;
	}

	/*
     * DESCR: Called by an animation event, a corresponding sound effect is played.
     * PRE: An integer representing one of the character's sound effects,
     * POST: The audio source plays the correct sound effect.
     */
	void playVoice (int clip) {
		AudioClip sound = null;
		switch(clip) {
			case 0: //Light Attack
				sound = lightAttack;
				break;
			case 1: //Heavy Attack
				sound = heavyAttack;
				break;
			case 2: //Special Attack
				sound = specialAttack;
				break;
			case 3: //Flinch
				sound = flinch;
				break;
			case 4: //Launched
				sound = launched;
				break;
			case 5: //Death
				sound = death;
				break;
			default:
				sound = null;
				break;
		}

		if (voice.clip != sound) {
			voice.clip = sound;
			voice.Play ();
		}
	}

}
