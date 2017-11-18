using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovements : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Controls.Instance.Player().RightDown)
            Debug.Log("Right");
	}
}
