using UnityEngine;
/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of a character's audio source, or "voice".
*/
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

	void reset() {
		voice.clip = null;
	}

	void playLightAttack () {
		if (voice.clip != lightAttack) {
			voice.clip = lightAttack;
			voice.Play ();
		}
	}
	
	void playHeavyAttack() {
		if (voice.clip != heavyAttack) {
			voice.clip = heavyAttack;
			voice.Play ();
		}
	}
	
	void playSpecialAttack() {
		if (voice.clip != specialAttack) {
			voice.clip = specialAttack;
			voice.Play ();
		}
	}
	
	void playFlinch() {
		if (voice.clip != flinch) {
			voice.clip = flinch;
			voice.Play ();
		}
	}
	
	void playLaunched() {
		if (voice.clip != launched) {
			voice.clip = launched;
			voice.Play ();;
		}
	}
	
	void playDeath() {
		if (voice.clip != death) {
			voice.clip = death;
			voice.Play ();
		}
	}
	
}
