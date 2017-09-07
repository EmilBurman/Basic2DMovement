using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehavior : MonoBehaviour
{
    private IController2D controller;
    private ITerrainState stateMachine;
    private IDash dash;

    void Awake()
    {
        controller = GetComponent<IController2D>();
        stateMachine = GetComponent<ITerrainState>();
        dash = GetComponent<IDash>();
    }

    void Update()
    {
        controller.Dash();
        controller.MoveHorizontal();
        controller.MoveVertical();
    }

    void FixedUpdate()
    {
        if (stateMachine.Grounded())
            dash.ResetDash();
        else if (stateMachine.Airborne())
        {
            dash.Dash(controller.MoveHorizontal(), controller.MoveVertical(), controller.Dash());
            if (stateMachine.WallLeft())
                dash.ResetDash();
            if (stateMachine.WallRight())
                dash.ResetDash();
        }
    }
}
