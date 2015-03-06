using UnityEngine;
using System.Collections;

public class voiceController : MonoBehaviour {
	
	private AudioSource voice;
	
	public AudioClip lightAttack;
	public AudioClip heavyAttack;
	public AudioClip specialAttack;
	public AudioClip flinch;
	public AudioClip launched;
	public AudioClip death;
	
	// Use this for initialization
	void Start () {
		voice = this.audio;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void playLightAttack () {
		voice.clip = lightAttack;
		voice.Play ();
	}
	
	void playHeavyAttack() {
		voice.clip = heavyAttack;
		voice.Play ();
	}
	
	void playSpecialAttack() {
		voice.clip = specialAttack;
		voice.Play ();
	}
	
	void playFlinch() {
		voice.clip = flinch;
		voice.Play ();
	}
	
	void playLaunched() {
		voice.clip = launched;
		voice.Play ();
	}
	
	void playDeath() {
		voice.clip = death;
		voice.Play ();
	}
	
}
