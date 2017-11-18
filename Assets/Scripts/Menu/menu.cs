using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : SingletonBehaviour<menu> {


	public GameObject menuStart;
	public GameObject UIGame;
	public GameObject pausePanel;

	public int buttonchoice;
	public  Button[] buttonMenu;
	

	// Use this for initialization
	void Start () {
		buttonchoice = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name == "GameScene") {
			/*if (Controls.Instance.Player().pause) {

			}*/
		}
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			
			buttonchoice = (buttonchoice + (int)Controls.Instance.Player().Horizontal) % 2;
			buttonMenu[buttonchoice].Select();
			
			if (Controls.Instance.Player().SwapUp)
			{
				if (buttonchoice == 0)
				{
					play();
				}
				else if (buttonchoice == 1)
				{
					quitGame();

				}
			}
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
