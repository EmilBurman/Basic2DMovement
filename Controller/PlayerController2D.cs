using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour, IController2D
{
    //Interface-----------------------------
    public float Move()
    {
        if (!SlowReverse())
            return 0f;
        else
            return Input.GetAxisRaw("Horizontal");
    }

    public bool Jump()
    {
        if (!SlowReverse())
            return false;
        else
            return Input.GetButtonDown("Jump");
    }

    public bool Dash()
    {
        if (!SlowReverse())
            return false;
        else
            return Input.GetButtonDown("Dash");
    }

    public bool Sprint()
    {
        if (!SlowReverse())
            return false;
        else
            return Input.GetButton("Sprint");
    }

    public bool FlashReverse()
    {
        return Input.GetButtonDown("Flash");
    }
    
    public bool SlowReverse()
    {
        return Input.GetButton("Reverse");
    }
    //End interface-------------------------
}
