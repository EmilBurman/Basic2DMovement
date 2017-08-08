using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour, IController2D
{
    //Interface-----------------------------
    public void Move()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
    }

    public void Jump()
    {
        jumpButton = Input.GetButtonDown("Jump");
    }

    public void Dash()
    {
        dashButton = Input.GetButtonDown("Dash");
    }

    public void Sprint()
    {
        sprintButton = Input.GetButton("Sprint");
    }

    public void Shadowstep()
    {
        throw new NotImplementedException();
    }
    //End interface-------------------------

    // Input variables.
    float horizontalAxis;                                   // Stores the input axies for horizontal movement.
    bool jumpButton;                                        // Checks if the jump button has been pressed.
    bool dashButton;                                        // Checks if the dash button has been activated.
    bool sprintButton;                                      // Checks if player is sprinting.
    public string currentState;

    // Interal variables
    private PlayerTerrainState stateMachine;
    private PlayerJump jump;
    private PlayerMovement move;
    private PlayerDash dash;

    // Use this for initialization
    void Awake()
    {
        stateMachine = GetComponent<PlayerTerrainState>();
        jump = GetComponent<PlayerJump>();
        move = GetComponent<PlayerMovement>();
        dash = GetComponent<PlayerDash>();
    }

    private void Update()
    {
        Move();
        Jump();
        Dash();
        Sprint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState = stateMachine.GetState().ToLower();

        switch (currentState)
        {
            case "grounded":
                jump.Grounded(jumpButton, sprintButton);
                move.Grounded(horizontalAxis, sprintButton);
                dash.ResetDash();
                break;
            case "airborne":
                jump.Airborne(jumpButton);
                move.Airborne(horizontalAxis, sprintButton);
                dash.Dash(horizontalAxis, dashButton);
                break;
            case "wallclimbingright":
                jump.RightWall(jumpButton);
                move.Wallride(horizontalAxis, sprintButton);
                dash.ResetDash();
                break;
            case "wallclimbingleft":
                jump.LeftWall(jumpButton);
                move.Wallride(horizontalAxis, sprintButton);
                dash.ResetDash();
                break;
        }
    }
}
