﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : SingletonBehaviour<menu> {


	public GameObject menuStart;
	public GameObject UIGame;
	public GameObject pausePanel;
	public GameObject highScorePanel;
	public int buttonchoice;
	public  Button[] buttonMenu;
	public Button[] buttonPause;
	

	// Use this for initialization
	void Start () {
		buttonchoice = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name == "GameScene") {
			
			//Debug.Log(pausePanel.activeSelf);
			if (pausePanel.activeSelf)
			{
				if (Controls.Instance.Player().PauseDown)
				{
					switch (buttonchoice)
					{
						case 0:
						buttonchoice = 0;
						backPause();
						break;

						case 1:
						buttonchoice = 0;
						backPause();
						menuLoad();
						break;

					}

				}

				//Menu
				buttonchoice = (int)Mathf.Repeat((buttonchoice + (Controls.Instance.Player().RightDown ? 1 : 0) + (Controls.Instance.Player().LeftDown ? -1 : 0)), 2);
				buttonPause[buttonchoice].Select();


			}
			else
			{
				if (Controls.Instance.Player().PauseDown)
				{
					pause();
				}
			}
			

			


		}
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			if (!highScorePanel.activeSelf)
			{
				if (Controls.Instance.Player().PauseDown) 
				{
					switch (buttonchoice)
					{ case 0:
						play();
						break;

					case 1:
						highscore();
						break;

					case 2:
						quitGame();
						break;

					}
					
				}

				//Menu
				buttonchoice = (int)Mathf.Repeat((buttonchoice + (Controls.Instance.Player().RightDown ? 1 : 0) + (Controls.Instance.Player().LeftDown ? -1 : 0)), 3);
				buttonMenu[buttonchoice].Select();

				
			}
			else {
				if (Controls.Instance.Player().PauseDown) 
				{
					backHighscore();
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
	public void menuLoad()
	{
		string scene = "MainMenu";
		changeMenuGameAndStart();
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
		GameManager.Instance.InitGame(GameManager.Instance.Mode);
	}

	public void backHighscore()
	{
		highScorePanel.SetActive(false);
	}

	public void quitGame()
	{
		Application.Quit();
		Debug.Log("Game closed");
	}


	//A tester
	public void highscore() {
		highScorePanel.SetActive(true);
		
		/*
		Debug.Log("Construction Higscore panel");
			for (int i = 0; i < SaveManager.nbSucces; i++)
			{
				GameObject prefab;
				if (contentPanel.transform.Find("Succes" + i) == null)
				{
					prefab = Instantiate(sucessLine, contentPanel.transform) as GameObject;
					prefab.name = "Succes" + i;
				}
				else
				{
					prefab = contentPanel.transform.Find("Succes" + i).gameObject;
				}
				if (!PlayerPrefs.HasKey("Succes(" + i + ")"))
				{
					prefab.GetComponentInChildren<Text>().text = "Locked";
					
				}
				else
				{
					if (System.Convert.ToBoolean(PlayerPrefs.GetInt("Succes(" + i + ")")))
					{
						prefab.GetComponentInChildren<Text>().text = SaveManager.Succes[i];
						//Debug.Log(SaveManager.Succes[i]);
					}
					else
					{
						prefab.GetComponentInChildren<Text>().text = "Locked";
					}
				}*/
	}
	
	public void pause()
	{
		pausePanel.SetActive(true);
		Time.timeScale = 0f;
	}
	public void backPause()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 1f;
	}
	
	

	//change the menu start and game
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
