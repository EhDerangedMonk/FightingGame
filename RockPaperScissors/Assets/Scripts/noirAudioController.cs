using UnityEngine;
using System.Collections;

public class noirAudioController : MonoBehaviour {
	
	public AudioSource voice;

	public AudioClip lightAttack;
	public AudioClip heavyAttack;
	public AudioClip specialAttack;
	public AudioClip flinch;
	public AudioClip launched;
	public AudioClip death;

	private bool soundPlaying = false;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = voice.GetComponentInParent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("noirIdle")) 
			soundPlaying = false;

		else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("noirLightAttack") && !soundPlaying) 
			setAndPlay (lightAttack);

		else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("noirHeavyAttack") && !soundPlaying)
			setAndPlay (heavyAttack);

		else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("noirSpecialEx") && !soundPlaying) 
			setAndPlay (specialAttack);
		
		else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("noirFlinch") && !soundPlaying)
			setAndPlay (flinch);

		else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("noirLaunch") && !soundPlaying) 
			setAndPlay (launched);
		
		else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("noirDeath"))
			trumpAudio (death);
		
	}

	void setAndPlay(AudioClip sound) {
		voice.clip = sound;
		voice.Play ();
		soundPlaying = true;
	}

	void trumpAudio (AudioClip sound) {
		if (voice.clip != sound) {
			voice.clip = sound;

			if (audio.isPlaying)
				voice.Stop ();

			voice.Play ();
		}
	}
}
