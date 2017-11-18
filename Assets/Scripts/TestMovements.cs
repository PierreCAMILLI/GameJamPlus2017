using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovements : MonoBehaviour {

	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
	}

    Rigidbody2D rigidbody2D;

    [SerializeField]
    Vector2 gravity;

	// Update is called once per frame
	void FixedUpdate () {
        rigidbody2D.AddForce(gravity * rigidbody2D.mass);
	}
}
