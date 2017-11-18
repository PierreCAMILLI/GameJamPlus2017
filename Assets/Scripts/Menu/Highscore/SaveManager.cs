using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : SingletonBehaviour<SaveManager>{




	//Les données sauvegardé de documents et succés
	public static int nbScore_MAX;
	public static int nbScore;
	public static score_struct[] highscore;


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
		nbScore_MAX = 10;
		highscore = new score_struct[10];


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
				highscore[i] = score;
			}
		}
	}
	

	//sauve le score dans le tableau, dans la mémoire, et retourne la position du joueur
	int saveScore(score_struct newScore)
	{
		nbScore ++;
		if (nbScore >= 10) {
			nbScore = 10;
		}

		PlayerPrefs.SetInt("nbScore", nbScore);

		int i = 0;
		//Comparation rapide du score
		while( i < nbScore && 
			(highscore[i].score_min*100+ highscore[i].score_sec < newScore.score_min * 100 + newScore.score_sec))//
		{
			i++;
		}

		for (int j = nbScore-1; j > i; j--) {
			highscore[i]= highscore[i-1];
		}
		highscore[i] = newScore;

		//Permettra d'afficher à l'écran de fin
		int index = i;

		//Sauvegarde mémoire
		for (; i < nbScore; i++)
		{
			
			PlayerPrefs.SetString("pseudo-" + i, highscore[i].pseudo);
			PlayerPrefs.SetInt("score_min-" + i, highscore[i].score_min);
			PlayerPrefs.SetInt("score_sec-" + i, highscore[i].score_sec);
		}
		PlayerPrefs.Save();
		return index;
	}

}
