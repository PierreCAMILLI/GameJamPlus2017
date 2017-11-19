using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Tooltip("Largeur de la zone inoccupable au milieu de la balance.")]
    [SerializeField]
    private float _offMiddleZoneWidth = 1f;
    public float OffMiddleZoneWidth { get { return _offMiddleZoneWidth; } }
	public ContactFilter2D colisionFilter;
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
    [SerializeField]
    private bool _toggleTimer = false;

    private float _timer;
    /// <summary>
    /// Indique la valeur du timer du jeu
    /// </summary>
    public float Timer { get { return _timer; } }

    public void ResetTimer()
    {
        _timer = 0f;
    }

    public void ToggleTimer(bool toggle)
    {
        _toggleTimer = toggle;
    }
    #endregion

    #region Fall Objects
    [SerializeField]
    private float _fallSpeedMultiplier = 1.0f;
    public float FallSpeedMultiplier { get { return _fallSpeedMultiplier; } set { _fallSpeedMultiplier = value; } }
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


    public bool debug_InitCoopGame = false;



    void Start()
    {
        if (debug_InitCoopGame)
            InitGame(GameMode.Cooperation);
    }

    void Update()
    {
        if (_toggleTimer)
            _timer += Time.deltaTime;
        if (FoodSpawner.Spawners.All(x => x.ReadyToSpawn))
            foreach (FoodSpawner spawner in FoodSpawner.Spawners)
                spawner.Spawn();
    }

    public void InitGame(GameMode mode)
    {
        _mode = mode;
        _startCountdown = _startCountdownInit;
        _timer = 0f;

        for (int i = 0; i < _playerStats.Length; ++i)
        {
            _playerStats[i] = new PlayerStats();
            _playerStats[i].InitGame(mode);
        }
    }
}

public class PlayerStats
{
    private float _timer;
    public float Timer { get { return _timer; } }

    private int _fallenObjects;
    public int FallenObjects { get { return _fallenObjects; } set { _fallenObjects = value; } }

    private float _fallSpeed = 1.0f;
    public float FallSpeed { get { return _fallSpeed; } set { _fallSpeed = value; } }

    public void InitGame(GameManager.GameMode mode)
    {
        _timer = 0f;
        _fallenObjects = GameManager.Instance.LimitObjectsToFall;
        _fallSpeed = 1f;
    }

}
