﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager> {

#region SerializeField
    [Tooltip("Valeur du timer au lancement du jeu avant le 'GO!'.")]
    [SerializeField]
    private float _startCountdownInit = 3f;

    [Tooltip("Nombre maximal d'objets qu'un joueur peut laisser tomber.")]
    [SerializeField]
    private int _limitObjectsToFall = 30;
    public int LimitObjectsToFall { get { return _limitObjectsToFall; } }
    #endregion

    #region Game Mode
    public enum GameMode
    {
        None = 0,
        Cooperation,
        Versus,
    }

    private GameMode _mode;
    /// <summary>
    /// Indique le mode de jeu actuel
    /// </summary>
    public GameMode Mode { get { return _mode; } set { _mode = value; } }
    #endregion

    #region Start Countdown

    private float _startCountdown;
    /// <summary>
    /// Indique la valeur du countdown de début
    /// </summary>
    public float StartCountdown { get { return _startCountdown; } set { _startCountdown = value; } }
    public int StartCountdownRound { get { return Mathf.CeilToInt(_startCountdown); } }
    /// <summary>
    /// Indique si le countdown de début est écoulé
    /// </summary>
    public bool GameStarted { get { return _startCountdown <= 0f; } }
    #endregion

#region Timer
    private float _timer;
    /// <summary>
    /// Indique la valeur du timer du jeu
    /// </summary>
    public float Timer { get { return _timer; } }
#endregion

#region Player Stats
    private PlayerStats[] _playerStats = new PlayerStats[2];
    /// <summary>
    /// Statistiques pour chaque joueurs
    /// </summary>
    public PlayerStats[] PlayerStats { get { return _playerStats; } }
    public PlayerStats FirstPlayerStats { get { return _playerStats[0]; } }
    public PlayerStats SecondPlayerStats { get { return _playerStats[1]; } }
#endregion

    public void InitGame(GameMode mode)
    {
        _mode = mode;
        _startCountdown = _startCountdownInit;
        _timer = 0f;

        foreach (PlayerStats playerStat in _playerStats)
            playerStat.InitGame(mode);
    }
}

public class PlayerStats
{
    private float _timer;
    public float Timer { get { return _timer; } }

    private int _fallenObjects;
    public int FallenObjects { get { return _fallenObjects; } }

    public void InitGame(GameManager.GameMode mode)
    {
        _timer = 0f;
        _fallenObjects = GameManager.Instance.LimitObjectsToFall;
    }

}
