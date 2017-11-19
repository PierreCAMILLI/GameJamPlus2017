using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : SingletonBehaviour<GameOverManager> {

    [SerializeField]
    Transform _ui;
    public Transform UI { get { return _ui; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleUI(bool toggle)
    {
        _ui.gameObject.SetActive(toggle);
    }
}
