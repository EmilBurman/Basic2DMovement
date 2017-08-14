using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour, IController2D
{
    //Interface-----------------------------
    public float Move()
    {
        if (slowReverseCheck)
            return 0f;
        else
            return Input.GetAxisRaw("Horizontal");
    }

    public bool Jump()
    {
        if (slowReverseCheck)
            return false;
        else
            return Input.GetButtonDown("Jump");
    }

    public bool Dash()
    {
        if (slowReverseCheck)
            return false;
        else
            return Input.GetButtonDown("Dash");
    }

    public bool Sprint()
    {
        if (slowReverseCheck)
            return false;
        else
            return Input.GetButton("Sprint");
    }

    public bool FlashReverse()
    {
        if (slowReverseCheck)
            return false;
        else
            return Input.GetButtonDown("Flash");
    }
    
    public bool SlowReverse()
    {
        slowReverseCheck = Input.GetButton("Reverse");
        return Input.GetButton("Reverse");
    }
    //End interface-------------------------
    bool slowReverseCheck;
}
