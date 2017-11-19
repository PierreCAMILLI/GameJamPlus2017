using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitCredit : MonoBehaviour {

	private IEnumerator coroutine;
	public float delay;

	// Use this for initialization
	void Start () {

		coroutine = quitGame(delay);
		StartCoroutine(coroutine);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator quitGame(float delay)
	{
		
		yield return new WaitForSecondsRealtime(5f);
		Application.Quit();
		
	}
}
