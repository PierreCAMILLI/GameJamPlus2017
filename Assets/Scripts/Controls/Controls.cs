using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : SingletonBehaviour<Controls> {

    [SerializeField]
    private PlayerController[] _playerControls;

    public PlayerController Player(byte player = 0)
    {
        return _playerControls[player];
    }
}

[System.Serializable]
public class PlayerController
{
#region Unity Axis
    [SerializeField]
    private string m_leftButton = "LeftButton";

    [SerializeField]
    private string m_rightButton = "RightButton";

    [SerializeField]
    private string m_pauseButton = "Pause";
    #endregion

    public float Horizontal
    {
        get { return Input.GetAxisRaw(m_rightButton) - Input.GetAxisRaw(m_leftButton); }
    }

    public bool PauseDown
    {
        get { return Input.GetButtonDown(m_pauseButton); }
    }

    public bool Pause
    {
        get { return Input.GetButton(m_pauseButton); }
    }

    public bool PauseUp
    {
        get { return Input.GetButtonUp(m_pauseButton); }
    }

    public bool SwapDown
    {
        get { return Input.GetButtonDown(m_rightButton) && Input.GetButtonDown(m_leftButton); }
    }

    public bool Swap
    {
        get { return Horizontal == 0f; }
    }
    
    public bool SwapUp
    {
        get { return Input.GetButtonUp(m_rightButton) && Input.GetButtonUp(m_leftButton); }
    }
}
