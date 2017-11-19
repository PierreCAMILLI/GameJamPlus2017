using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicAudio : SingletonBehaviour<musicAudio>
{ 

	public AudioClip MenuTheme;
	public AudioClip GameTheme;
	public AudioClip sonette;
	public AudioClip game_over;

	public AudioSource speakers;
	public AudioSource objectSon;
	public float volGeneral;


	// Use this for initialization
	void Start()
	{
		speakers.volume = volGeneral;
		speakers.clip = MenuTheme;
		speakers.PlayDelayed(1f);
	}

	// Update is called once per frame
	void Update()
	{
		

	}
	public void playMenuTheme()
	{
		speakers.volume = volGeneral;
		speakers.clip = MenuTheme;
		speakers.PlayDelayed(1f);
	}

	public void instancePlayAudio(AudioClip son, Transform parent)
	{
		AudioSource clone =Instantiate(objectSon, parent).GetComponent<AudioSource>();
		clone.volume = volGeneral;
		clone.clip = son;
		clone.Play();
	}
	public void playAudioClipe(AudioClip son)
	{
		speakers.volume = volGeneral;
		speakers.clip = son;
		speakers.Play();
	}
	public void playGameOver()
	{
		speakers.volume = volGeneral;
		speakers.clip = game_over;
		speakers.Play();
	}

	public void playGameTheme()
	{
		speakers.volume = volGeneral;
		speakers.clip = GameTheme;
		speakers.PlayDelayed(2f);
	}
}

