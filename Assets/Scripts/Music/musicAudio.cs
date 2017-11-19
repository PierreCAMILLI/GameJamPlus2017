using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicAudio : SingletonBehaviour<musicAudio>
{ 

	public AudioClip MenuTheme;
	public AudioClip GameTheme;
	public AudioSource speakers;

	public float volMenu;
	public float volGame;

	// Use this for initialization
	void Start()
	{
		speakers.volume = volMenu;
		speakers.clip = MenuTheme;
		speakers.PlayDelayed(1f);
	}

	// Update is called once per frame
	void Update()
	{
		

	}
	public void playMenuTheme()
	{
		speakers.volume = volMenu;
		speakers.clip = MenuTheme;
		speakers.PlayDelayed(1f);
	}

	public void playGameTheme()
	{
		speakers.volume = volGame;
		speakers.clip = GameTheme;
		speakers.PlayDelayed(1f);
	}
}

