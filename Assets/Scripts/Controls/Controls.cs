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
    KeyCode _leftButton = KeyCode.Joystick1Button4;

    [SerializeField]
    KeyCode _rightButton = KeyCode.Joystick1Button5;

    [SerializeField]
    KeyCode _pauseButton = KeyCode.JoystickButton7;
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
        get { return (Right ? 1 : 0) - (Left ? 1 : 0); }
    }

    public bool RightDown
    {
        get { return Input.GetKeyDown(_rightButton); }
    }

    public bool LeftDown
    {
        get { return Input.GetKeyDown(_leftButton); }
    }

    public bool Right
    {
        get { return Input.GetKey(_rightButton); }
    }

    public bool Left
    {
        get { return Input.GetKey(_leftButton); }
    }

    public bool PauseDown
    {
        get { return Input.GetKeyDown(_pauseButton); }
    }

    public bool Pause
    {
        get { return Input.GetKey(_pauseButton); }
    }

    public bool PauseUp
    {
        get { return Input.GetKeyUp(_pauseButton); }
    }

    public bool SwapDown
    {
        get {
            bool buttonPressed = false;
            if (Input.GetKeyDown(_rightButton))
            {
                _rightPressTime.Down = Time.time;
                buttonPressed = true;
            }
            if (Input.GetKeyDown(_leftButton))
            {
                _leftPressTime.Down = Time.time;
                buttonPressed = true;
            }
            return buttonPressed && Horizontal == 0f && Mathf.Abs(_rightPressTime.Down - _leftPressTime.Down) <= Controls.Instance.TimeMultipleTrigger;
        }
    }

    public bool Swap
    {
        get { return Input.GetKey(_rightButton) && Input.GetKey(_leftButton); }
    }
    
    public bool SwapUp
    {
        get
        {
            bool buttonPressed = false;
            if (Input.GetKeyUp(_rightButton))
            {
                _rightPressTime.Up = Time.time;
                buttonPressed = true;
            }
            if (Input.GetKeyUp(_leftButton))
            {
                _leftPressTime.Up = Time.time;
                buttonPressed = true;
            }
            return buttonPressed && Mathf.Abs(_rightPressTime.Up - _leftPressTime.Up) <= Controls.Instance.TimeMultipleTrigger;
        }
    }
}
