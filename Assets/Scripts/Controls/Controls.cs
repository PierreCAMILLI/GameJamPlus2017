using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : SingletonBehaviour<Controls> {

    [SerializeField]
    private float _timeMultipleTriggers = 0.25f;
    public float TimeMultipleTrigger { get { return _timeMultipleTriggers; } }

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

#region Double Press

    struct PressTime
    {
        public float Up;
        public float Down;
    }

    PressTime _leftPressTime;
    PressTime _rightPressTime;
#endregion

    public float Horizontal
    {
        get { return Input.GetAxisRaw(m_rightButton) - Input.GetAxisRaw(m_leftButton); }
    }

    public bool RightDown
    {
        get { return Input.GetButtonDown(m_rightButton); }
    }

    public bool LeftDown
    {
        get { return Input.GetButtonDown(m_leftButton); }
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
        get {
            bool buttonPressed = false;
            if (Input.GetButtonDown(m_rightButton))
            {
                _rightPressTime.Down = Time.time;
                buttonPressed = true;
            }
            if (Input.GetButtonDown(m_leftButton))
            {
                _leftPressTime.Down = Time.time;
                buttonPressed = true;
            }
            return buttonPressed && Horizontal == 0f && Mathf.Abs(_rightPressTime.Down - _leftPressTime.Down) <= Controls.Instance.TimeMultipleTrigger;
        }
    }

    public bool Swap
    {
        get { return Input.GetButton(m_rightButton) && Input.GetButton(m_leftButton); }
    }
    
    public bool SwapUp
    {
        get
        {
            bool buttonPressed = false;
            if (Input.GetButtonUp(m_rightButton))
            {
                _rightPressTime.Up = Time.time;
                buttonPressed = true;
            }
            if (Input.GetButtonUp(m_leftButton))
            {
                _leftPressTime.Up = Time.time;
                buttonPressed = true;
            }
            return buttonPressed && Mathf.Abs(_rightPressTime.Up - _leftPressTime.Up) <= Controls.Instance.TimeMultipleTrigger;
        }
    }
}
