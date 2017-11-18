using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveManager : SingletonBehaviour<SaveManager>{




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

	new void Awake()
	{
		//Construction sauvegarde
		
		Highscores = new List<score_struct>(nbScore_MAX);


		if (!PlayerPrefs.HasKey("nbScore"))
		{
			nbScore = 0;
			PlayerPrefs.SetInt("nbScore", 0);

			//Debug.Log("FirstConnection");
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
				Highscores[i] = score;
			}
		}
	}
	

	//sauve le score dans le tableau, dans la mémoire, et retourne la position du joueur
	int saveScore(score_struct newScore)
	{

		nbScore = Mathf.Min(nbScore_MAX, ++nbScore);

		PlayerPrefs.SetInt("nbScore", Highscores.Count);

		int i = 0;
		//Comparation rapide du score
		while( i < nbScore && (_GetScore(Highscores[i]) > _GetScore(newScore)))//comparaison des scores
		{
			++i;
		}

		Highscores.Insert(i, newScore);
		if (Highscores.Count > nbScore_MAX)
			Highscores.RemoveRange(nbScore_MAX, Highscores.Count - 1);


		//Permettra d'afficher à l'écran de fin
		int index = i;

		//Sauvegarde mémoire
		for (; i < nbScore; ++i)
		{
			
			PlayerPrefs.SetString("pseudo-" + i, Highscores[i].pseudo);
			PlayerPrefs.SetInt("score_min-" + i, Highscores[i].score_min);
			PlayerPrefs.SetInt("score_sec-" + i, Highscores[i].score_sec);
		}
		PlayerPrefs.Save();
		return index;
	}

	private int _GetScore(score_struct score)
	{
		return score.score_min * 100 + score.score_sec;
	}

}
