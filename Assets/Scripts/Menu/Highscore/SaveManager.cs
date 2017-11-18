using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : SingletonBehaviour<SaveManager>{


	/*

	public int lastSave;


	//Les données sauvegardé de documents et succés
	public static int nbDocuments;
	public static bool[] DocumentInGame;

	//Tableau sert à mettre les indications de succes, mais en mémoireDur int pour bool.
	public static int nbSucces;
	public static string[] Succes;

	//Structure de sauvegarde
	public struct SaveFile
	{
		public int saveNumber;
		public string ActualSceneIndex;
		public float CharacterCoordX;
		public float CharacterCoordY;
	};

	public static SaveFile saveingame;

	void Awake()
	{
		//Construction inGame des sauvegardes futures, list pré-établi d'objets à sauvegardé (pas encore fait).
		nbDocuments = 30;
		DocumentInGame = new bool[30];


		//Créatin des succés
		nbSucces = 10;
		Succes = new string[10];
		Succes[0] = "Succes 1: blabla etc...";
		//Première connection au jeu sauvegarde, indice numéro dernière sauvegarde
		//Création de la liste de succés

		if (!PlayerPrefs.HasKey("NbLastSave") || PlayerPrefs.GetInt("NbLastSave") < 0)
		{
			SetSavingInt("NbLastSave", -1);

			for (int i = 0; i < nbSucces; i++)
			{
				SetSavingInt("Succes(" + i + ")", 0);
			}

			//Debug.Log("FirstConnection");
		}
		else
		{
			lastSave = PlayerPrefs.GetInt("NbLastSave");
			Debug.Log("Last Save is Number ");
			Debug.Log(lastSave);
		}


	}

	//Crée un template de sauvegarde de cette forme en mémoire, tout à 0 
	/*
	 * Heure et date de sauvegarde	"SaveTime-"					jj/mm/aaaa hh:mm
	 * Nom de la scene actuelle		"ActualSceneName-"			Scene1
	 * Coodonnées x					"CharacterCoordX-"			0
	 * coordonnés y					"CharacterCoordY-"			0
	 * Documents(1)					"Document(" + i + ")-"		0
	 * etc...
	 * 
	 * *
	public void saveCreation(int index)
	{
		//Debug.Log("Creation sauvegarde"+index);
		SetSavingString("SaveTime-" + index, System.DateTime.Now.ToString("dd/mm/yy HH:mm"));
		SetSavingString("ActualSceneName-" + index, "Scene1");
		SetSavingFloat("CharacterCoordX-" + index, 0);
		SetSavingFloat("CharacterCoordY-" + index, 0);

		for (int i = 0; i < nbDocuments; i++)
		{
			SetSavingInt("Document(" + i + ")-" + index, 0);
		}
		SetSavingInt("NbLastSave", index);
		PlayerPrefs.Save();
	}

	//Met à jour la sauvegarde  en mémoire, utilise la position x,y du joueur supposé.
	//! Tableaux de bool et coordonnés pas encore vérifié, tout sauf date non utilisé
	/*
	 * Heure et date de sauvegarde	"SaveTime-"					jj/mm/aaaa hh:mm
	 * Nom de a scene actuelle		"ActualSceneName-"			?
	 * Coodonnées x					"CharacterCoordX-"			?
	 * coordonnés y					"CharacterCoordY-"			?
	 * Documents(1)					"Document(" + i + ")-"		?
	 * etc...
	 * 
	 * *
	public static void autoSave(int index, Transform transform)
	{

		SetSavingString("SaveTime-" + index, System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
		SetSavingString("ActualSceneName-" + index, SceneManager.GetActiveScene().name);
		SetSavingFloat("CharacterCoordX-" + index, transform.position.x);
		SetSavingFloat("CharacterCoordY-" + index, transform.position.y);
		for (int i = 0; i < nbDocuments; i++)
		{
			SetSavingInt("Document(" + i + ")-" + index, System.Convert.ToInt32(DocumentInGame[i]));
		}
		SetSavingInt("NbLastSave", index);
		PlayerPrefs.Save();
	}

	//Permet de recueillir de la sauvegarde la scene, la position x,y, et d'écrie le tableau de bool des documents.
	public static void getScore(int index)
	{
		saveingame.saveNumber = index;

		saveingame.ActualSceneIndex = PlayerPrefs.GetString("ActualSceneName-" + index);
		saveingame.CharacterCoordX = PlayerPrefs.GetFloat("CharacterCoordX-" + index);
		saveingame.CharacterCoordY = PlayerPrefs.GetFloat("CharacterCoordY-" + index);

		for (int i = 0; i < nbDocuments; i++)
		{
			DocumentInGame[i] = System.Convert.ToBoolean(PlayerPrefs.GetInt("Document(" + i + ")-" + index));
		}
		/*
		Debug.Log(saveingame.saveNumber);
		Debug.Log(saveingame.ActualSceneIndex);
		Debug.Log(saveingame.CharacterCoordX);
		Debug.Log(saveingame.CharacterCoordY);
	}
*/

}
