using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : SingletonBehaviour<menu> {


	public GameObject menuStart;
	public GameObject UIGame;
	public GameObject pausePanel;
	public GameObject highScorePanel;
	public GameObject gameOverPanel;

	public int buttonchoice;
	public Button[] buttonPause;
	public GameObject checkp1;
	public GameObject checkp2;
	private bool _playcheck;
	private bool _playcorout;

	public GameObject scoreText;
	public GameObject contentScore;

	// Use this for initialization
	void Start () {
		createHighscore();
		buttonchoice = 0;
		_playcheck = false;


	}

	// Update is called once per frame
	void Update() {
		//test Tme
		//Debug.Log(Time.timeSinceLevelLoad);
		//Debug.Log(GameManager.Instance.Timer);
		//Debug.Log(convertTimerString(Time.timeSinceLevelLoad));
		//Debug.Log(_get_min(Time.timeSinceLevelLoad));
		//Debug.Log(_get_sec(Time.timeSinceLevelLoad));


		if (SceneManager.GetActiveScene().name == "GameScene")
		{


			if (pausePanel.activeSelf && !gameOverPanel.activeSelf)
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

				buttonchoice = (int)Mathf.Repeat(buttonchoice + ((Controls.Instance.Player(0).RightDown || Controls.Instance.Player(1).RightDown) ? 1 : 0) + ((Controls.Instance.Player(0).LeftDown || Controls.Instance.Player(1).LeftDown) ? -1 : 0), 2);
				buttonPause[buttonchoice].Select();


			}
			else if (!gameOverPanel.activeSelf && !pausePanel.activeSelf)
			{
				if (Controls.Instance.Player(0).PauseDown || Controls.Instance.Player(1).PauseDown)
				{
					pause();
				}
			}
			
		}
		else if (SceneManager.GetActiveScene().name == "MainMenu")
		{


			if (Controls.Instance.Player(0).Swap)
			{
				//Debug.Log("Player 1 here");
				checkp1.SetActive(true);

			}
			else {
				if (!_playcheck)
				{
					checkp1.SetActive(false);
				}
			}

			if (Controls.Instance.Player(1).Swap)
			{
				//Debug.Log("Player 2 here");
				checkp2.SetActive(true);

			}
			else
			{
				if (!_playcheck)
				{
					checkp2.SetActive(false);
				}
			}
			if (Controls.Instance.Player(0).Swap && Controls.Instance.Player(1).Swap)
			{
				if (!_playcorout)
				{
					Debug.Log("play");
					StartCoroutine("PlayDelay");
					_playcheck = true;
				}
			}
			
			//Menu
			//buttonchoice1 = (int)Mathf.Repeat((buttonchoice1 + (Controls.Instance.Player(0).RightDown ? 1 : 0) + (Controls.Instance.Player(0).LeftDown ? -1 : 0)), 3);
			//buttonMenu[buttonchoice].Select();



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
		musicAudio.Instance.playGameTheme();
	}

	public void menuLoad()
	{
		string scene = "MainMenu";
		deleteHighscore();
		createHighscore();
		changeMenuGameAndStart();
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
		GameManager.Instance.Mode = GameManager.GameMode.None;
		musicAudio.Instance.playMenuTheme();
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

	public void createHighscore() {
		/*
		Debug.Log("Create save _>");
		//test Mémoire
		SaveManager.score_struct scoreTest;
		scoreTest.pseudo = "Marc";
		scoreTest.score_min = 10;
		scoreTest.score_sec = 0;
		SaveManager.Instance.saveScore(scoreTest);
		Debug.Log("Create save done");
		Debug.Log("Count :"+SaveManager.Instance.Highscores.Count);
		Debug.Log(SaveManager.Instance.nbScore);
		*/
		for (int i = 0; i < SaveManager.Instance.nbScore; i++)
		{
			SaveManager.score_struct score = SaveManager.Instance.Highscores[i];
			GameObject scoreLine = Instantiate(scoreText, contentScore.transform);
			scoreLine.GetComponent<Text>().text = (i+1) + ". "+ score.pseudo+ " "+ score.score_min + ":" + score.score_sec + " ";
			scoreLine.name = "TextScore_"+ i;
		}
		
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

	IEnumerator PlayDelay() {
		_playcorout = true;
		yield return new WaitForSecondsRealtime(2f);
		_playcheck = false;
		checkp1.SetActive(false);
		checkp2.SetActive(false);
		play();
		_playcorout = false;
	}

}
