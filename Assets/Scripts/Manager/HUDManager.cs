using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : SingletonBehaviour<HUDManager> {

    [Header("Texts")]
    [SerializeField]
    Text _timer;

    [SerializeField]
    Text _firstPlayerScore;
    [SerializeField]
    Text _secondPlayerScore;

    // Update is called once per frame
    void Update () {
        if (_timer != null)
            _timer.text = Mathf.FloorToInt(GameManager.Instance.Timer).ToString();
        if(_firstPlayerScore != null)
            _firstPlayerScore.text = Mathf.FloorToInt(GameManager.Instance.PlayerStats[0].FallenObjects).ToString();
        if (_secondPlayerScore != null)
            _secondPlayerScore.text = Mathf.FloorToInt(GameManager.Instance.PlayerStats[1].FallenObjects).ToString();
    }
}
