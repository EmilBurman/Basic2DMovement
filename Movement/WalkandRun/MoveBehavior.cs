using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour
{
    private IController2D controller;
    private ITerrainState stateMachine;
    private IMovement move;

    void Awake()
    {
        controller = GetComponent<IController2D>();
        stateMachine = GetComponent<ITerrainState>();
        move = GetComponent<IMovement>();
    }

    void Update()
    {
        controller.Move();
        controller.Sprint();
    }


    void FixedUpdate()
    {
        if (stateMachine.Grounded())
            move.Grounded(controller.Move(), controller.Sprint());
        else if (stateMachine.Airborne())
        {
            move.Airborne(controller.Move(), controller.Sprint());
            if (stateMachine.WallLeft())
                move.Wallride(controller.Move(), controller.Sprint());
            if (stateMachine.WallRight())
                move.Wallride(controller.Move(), controller.Sprint());
        }
    }
}
