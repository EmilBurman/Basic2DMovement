using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : MonoBehaviour
{
    private IController2D controller;
    private ITerrainState stateMachine;
    private IJump jump;

    void Awake()
    {
        controller = GetComponent<IController2D>();
        stateMachine = GetComponent<ITerrainState>();
        jump = GetComponent<IJump>();
    }

    void Update()
    {
        controller.Move();
        controller.Sprint();
        controller.Jump();
    }

    void FixedUpdate()
    {
        if (stateMachine.Grounded())
            jump.Grounded(controller.Jump(), controller.Sprint());
        else if (stateMachine.Airborne())
        {
            jump.Airborne(controller.Jump());
            if (stateMachine.WallLeft())
                jump.LeftWall(controller.Jump());
            if (stateMachine.WallRight())
                jump.RightWall(controller.Jump());
        }
    }
}
