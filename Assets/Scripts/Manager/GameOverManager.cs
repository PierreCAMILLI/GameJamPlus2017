using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : SingletonBehaviour<GameOverManager> {

    [SerializeField]
    Transform _ui;
    public Transform UI { get { return _ui; } }

	public Text textGameOver;
	public SaveManager.score_struct score;

	public bool _gameOver;

	[SerializeField]
	private bool _endGame;

	// Use this for initialization
	void Start () {
		_gameOver = false;
		_endGame = false;
		score.pseudo = "anonymous";
		score.score_min = 06;
		score.score_sec = 66;
	}

	// Update is called once per frame
	void Update()
	{
		ToggleUI(_gameOver);

		if (_gameOver && _endGame)
		{
			if (Controls.Instance.Player(0).PauseDown || Controls.Instance.Player(1).PauseDown)
			{
				Time.timeScale = 1f;
				_endGame = false;
				_gameOver = false;
				score.score_min++;
				menu.Instance.menuLoad();
			}
		}
		else if (_gameOver && !_endGame)
		{
			menu.Instance.backPause();
			Time.timeScale = 0f;
			int classmt = SaveManager.Instance.saveScore(score);
			if (classmt > 9)
			{
				textGameOver.text = "--. " + score.pseudo;
			}
			else
			{
				textGameOver.text = (classmt+1) + ". " + score.pseudo + " " + score.score_min + ":" + score.score_sec;
			}
			_endGame = true;
		}
			
		

		
	}



    public void ToggleUI(bool toggle)
    {
		
        _ui.gameObject.SetActive(toggle);

	
    }


	

	/// <summary>
	/// Convertisseur en jeu
	/// </summary>
	public string convertTimerString(float time)
	{
		int min = _get_min(time);
		int sec = _get_sec(time);

		return (min / 10).ToString() + (min % 10).ToString() + " : " + (sec / 10).ToString() + (sec % 10).ToString();
	}
	public int _get_min(float time)
	{
		return Mathf.FloorToInt(time / 60);
	}
	public int _get_sec(float time)
	{
		return Mathf.FloorToInt(time % 60);
	}
}
