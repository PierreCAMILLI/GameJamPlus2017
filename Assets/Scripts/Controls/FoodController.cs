using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Food))]
public class FoodController : MonoBehaviour {

    public const float _moveStep = 0.5f;

    private Food _food;
    public Food Food { get { return _food; } }

    [SerializeField]
    private byte _playerNumber = 0;
    public byte PlayerNumber { get { return _playerNumber; } set { _playerNumber = value; } }

    private bool _enableControls = true;

    private const float _moveDelayMax = 0.5f;
    private float _moveDelay;
    private float _lastMoveTime;


	// Use this for initialization
	void Awake () {
        _food = GetComponent<Food>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateMovements();
	}

    void UpdateMovements()
    {
        if (_food.UsePhysicsGravity || !_enableControls)
            return;
        // Gère le swap
        if (Controls.Instance.Player(_playerNumber).SwapDown)
        {
            _food.Swap();
            _enableControls = false;
        }
        // Gère l'appui d'une touche
        else
        {
            if (Controls.Instance.Player(_playerNumber).LeftDown || Controls.Instance.Player(_playerNumber).RightDown)
            {
                _food.MoveHorizontally(_moveStep * Controls.Instance.Player(_playerNumber).Horizontal);
                _lastMoveTime = Time.time;
            }
            // Gère le maintien de la commande de mouvement
            if (Controls.Instance.Player(_playerNumber).Horizontal != 0f)
            {
                if ((Time.time - _lastMoveTime) > _moveDelay)
                {
                    _food.MoveHorizontally(_moveStep * Controls.Instance.Player(_playerNumber).Horizontal);
                    _moveDelay *= 0.75f;
                }
            }
            else
                _moveDelay = _moveDelayMax;
        }
    }
}
