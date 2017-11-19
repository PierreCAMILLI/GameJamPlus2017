using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitCredit : MonoBehaviour {

	IEnumerator couroutine;
	public float delay;
	// Use this for initialization
	void Start () {

		couroutine = quitGame(delay);
		StartCoroutine(coroutine);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator quitGame(float delay)
	{
		_playcorout = true;
		yield return new WaitForSecondsRealtime(5f);
		Application.Quit();
		
	}
}
