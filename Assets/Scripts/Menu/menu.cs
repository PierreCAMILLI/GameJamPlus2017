using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : SingletonBehaviour<menu>{


	public GameObject menuStart;
	public GameObject UIGame;
	public GameObject pausePanel;

	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name == "GameScene") {
			/*if (Controls.Instance.Player().pause) {

			}*/
		}


	}

	//Scripts button
	public void play() {
		string scene = "GameScene";
		changeMenuGameAndStart();
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
		GameManager.Instance.InitGame(GameManager.GameMode.Cooperation);
	}
	public void quitGame()
	{
		Application.Quit();
		Debug.Log("Game closed");
	}
	//A tester
	public void pause()
	{
		pausePanel.SetActive(true);
	}
	public void backButton()
	{
		pausePanel.SetActive(false);
	}

	//change the menu game and pause
	public void changeMenuGameAndStart()
	{
		if (!menuStart.activeSelf)
		{
			menuStart.SetActive(true);
			UIGame.SetActive(false);
		}
		else
		{
			menuStart.SetActive(false);
			UIGame.SetActive(true);
		}
	}
	

}
