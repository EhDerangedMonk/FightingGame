using UnityEngine;
/*
	Authored By: Nigel Martinez
	Purpose: Controls the behaviour of a character's audio source, or "voice".
*/
using System.Collections;

public class voiceController : MonoBehaviour {
	
	private AudioSource voice;
	private bool hasPlayed;
	
	public AudioClip lightAttack;
	public AudioClip heavyAttack;
	public AudioClip specialAttack;
	public AudioClip flinch;
	public AudioClip launched;
	public AudioClip death;
	
	// Use this for initialization
	void Start () {
		voice = this.audio;
		hasPlayed = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void reset() {
		hasPlayed = false;
	}

	void playLightAttack () {
		if (!hasPlayed) {
			voice.clip = lightAttack;
			voice.Play ();
			hasPlayed = true;
		}
	}
	
	void playHeavyAttack() {
		if (!hasPlayed) {
			voice.clip = heavyAttack;
			voice.Play ();
			hasPlayed = true;
		}
	}
	
	void playSpecialAttack() {
		if (!hasPlayed) {
			voice.clip = specialAttack;
			voice.Play ();
			hasPlayed = true;
		}
	}
	
	void playFlinch() {
		if (!hasPlayed) {
			voice.clip = flinch;
			voice.Play ();
			hasPlayed = true;
		}
	}
	
	void playLaunched() {
		if (!hasPlayed) {
			voice.clip = launched;
			voice.Play ();
			hasPlayed = true;
		}
	}
	
	void playDeath() {
		if (!hasPlayed) {
			voice.clip = death;
			voice.Play ();
			hasPlayed = true;
		}
	}
	
}
