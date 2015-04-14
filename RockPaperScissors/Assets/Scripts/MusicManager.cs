using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip menu, traditional, battlefield, lava, landslide;
	private AudioClip playingSong = null;
	private AudioSource music;

	// Use this for initialization
	void Start () {
		music = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		string map = Application.loadedLevelName;
		Debug.Log (map);

		if (map == "Landslide Map")
			playingSong = landslide;
		else if (map == "Traditional Fighter Map")
			playingSong = traditional;
		else if (map == "Smash Bros Battlefield Map")
			playingSong = battlefield;
		else if (map == "Lava Map")
			playingSong = lava;
		else
			playingSong = menu;

		if (music.clip != playingSong) {
			music.clip = playingSong;
			music.Play ();
		}
	}

	void Awake()
	{
		DontDestroyOnLoad(this); // make sure that this script stays active throughout the entire game
	}
	
}
