using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour {
    public IMovement movement;
    float horizontalAxis;
    bool sprint;

    // Use this for initialization
    void Start () {
        movement = GetComponent<IMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        movement.Wallride(horizontalAxis,sprint);
	}
}
