using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveManager : SingletonBehaviour<SaveManager>{

	public bool resetSave;


	//Les données sauvegardé de documents et succés
	[SerializeField]
	public int nbScore_MAX;
	public int nbScore;

	public List<score_struct> Highscores;


	//Structure de sauvegarde
	public struct score_struct
	{
		public string pseudo;
		public int score_min;
		public int score_sec;
	};

	void Awake()
	{
		resetSave = false;
		//Construction sauvegarde
		base.Awake();

		nbScore_MAX = 10;

		Highscores = new List<score_struct>();


		if (!PlayerPrefs.HasKey("nbScore"))
		{
			nbScore = 0;
			PlayerPrefs.SetInt("nbScore", 0);

			Debug.Log("FirstConnection");
		}
		else
		{
			nbScore = PlayerPrefs.GetInt("nbScore");
			for (int i = 0; i < nbScore; i++)
			{
				score_struct score;
				score.pseudo = PlayerPrefs.GetString("pseudo-" + i);
				score.score_min = PlayerPrefs.GetInt("score_min-" + i);
				score.score_sec = PlayerPrefs.GetInt("score_sec-" + i);
				Highscores.Insert(i, score); 
			}
		}
		Debug.Log("Awake done");
	}
	private void Update()
	{
		if (resetSave)
		{
			PlayerPrefs.DeleteAll();
			Debug.Log("reset saves");
		}
	}

	//sauve le score dans le tableau, dans la mémoire, et retourne la position du joueur
	public int saveScore(score_struct newScore)
	{
		++nbScore;
		nbScore = Mathf.Min(nbScore_MAX, nbScore);
		//Debug.Log(nbScore);

		PlayerPrefs.SetInt("nbScore", nbScore);

		int i = 0;
		//Debug.Log("i computed");
		//Comparation rapide du score
		while (i < nbScore-1 && (_GetScore(Highscores[i]) >= _GetScore(newScore)))//comparaison des scores
		{
			++i;
		}
		//Debug.Log(i);
		//Debug.Log(Highscores.Count);
		
		Highscores.Insert(i, newScore);
		
	

		
		/*if (Highscores.Count >= nbScore_MAX)
			Highscores.RemoveRange(nbScore_MAX, Highscores.Count - 1);*/
	

		//Permettra d'afficher à l'écran de fin
		int index = i;

		//Sauvegarde mémoire
		Debug.Log(i);
		for (i=0; i < nbScore ; i++)
		{
			
			PlayerPrefs.SetString("pseudo-" + i, Highscores[i].pseudo);
			PlayerPrefs.SetInt("score_min-" + i, Highscores[i].score_min);
			PlayerPrefs.SetInt("score_sec-" + i, Highscores[i].score_sec);
		}

		//Debug.Log(i);

		PlayerPrefs.Save();
		return index;
	}

	private int _GetScore(score_struct score)
	{
		return score.score_min * 100 + score.score_sec;
	}

}
