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

	public int buttonchoice1;
	public int buttonchoice2;
	//public  Button[] buttonMenu;
	public Button[] buttonPause;

	public GameObject scoreText;

	
	public GameObject contentScore;

	// Use this for initialization
	void Start () {
		creteHighscore();
		buttonchoice = 0;
		
	}

	// Update is called once per frame
	void Update() {
		if (SceneManager.GetActiveScene().name == "GameScene") {

			//Debug.Log(pausePanel.activeSelf);
			if (pausePanel.activeSelf)
			{
				if (Controls.Instance.Player(0).PauseDown || Controls.Instance.Player(1).PauseDown)
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

				//Pause
				buttonchoice1 = buttonchoice + (Controls.Instance.Player(0).RightDown ? 1 : 0) + (Controls.Instance.Player(0).LeftDown ? -1 : 0);
				buttonchoice2 = buttonchoice + (Controls.Instance.Player(1).RightDown ? 1 : 0) + (Controls.Instance.Player(1).LeftDown ? -1 : 0);

				buttonchoice = (int)Mathf.Repeat(buttonchoice1 + buttonchoice2, 2);
				buttonPause[buttonchoice].Select();


			}
			else
			{
				if (Controls.Instance.Player(0).PauseDown || Controls.Instance.Player(1).PauseDown)
				{
					pause();
				}
			}

		}
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{

			
			if (Controls.Instance.Player(0).Swap)
			{
				Debug.Log("Player 1 here");
				//change sprite player 1

			}
			if (Controls.Instance.Player(1).Swap)
			{
				Debug.Log("Player 2 here");
				//change sprite player 2

			}
			if (Controls.Instance.Player(0).Swap && Controls.Instance.Player(1).Swap)
			{
				Debug.Log("play");
				play();
			}

			//Menu
			//buttonchoice1 = (int)Mathf.Repeat((buttonchoice1 + (Controls.Instance.Player(0).RightDown ? 1 : 0) + (Controls.Instance.Player(0).LeftDown ? -1 : 0)), 3);
			//buttonMenu[buttonchoice].Select();


		}
		else
		{
			if (Controls.Instance.Player(0).PauseDown || Controls.Instance.Player(1).PauseDown)
			{
				quitGame();
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
		deleteHighscore();
		creteHighscore();
		changeMenuGameAndStart();
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
		GameManager.Instance.Mode = GameManager.GameMode.None;
	}

	

	public void quitGame()
	{
		Application.Quit();
		Debug.Log("Game closed");
	}

	public void deleteHighscore()
	{

		Transform[] exter = contentScore.GetComponentsInChildren<Transform>();
		for (int i = 0; i < contentScore.transform.childCount; i++)
		{
			Destroy(contentScore.transform.GetChild(i).gameObject);
		}

	}

	public void creteHighscore() {

		
		//test
		SaveManager.score_struct scoreTest;
		scoreTest.pseudo = "Marc";
		scoreTest.score_min = 10;
		scoreTest.score_sec = 0;
		SaveManager.Instance.saveScore(scoreTest);

		
		for (int i = 0; i < SaveManager.Instance.nbScore; i++)
		{
			SaveManager.score_struct score = SaveManager.Instance.Highscores[i];
			GameObject scoreLine = Instantiate(scoreText, contentScore.transform);
			scoreLine.GetComponent<Text>().text = (i+1)+ ". "+ score.pseudo+ " "+ score.score_min + ":" + score.score_sec + " ";
			scoreLine.name = "TextScore_"+i;
		}
		
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
