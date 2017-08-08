using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour, IController2D
{
    //Interface-----------------------------
    public float Move()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public bool Jump()
    {
        return Input.GetButtonDown("Jump");
    }

    public bool Dash()
    {
        return Input.GetButtonDown("Dash");
    }

    public bool Sprint()
    {
        return Input.GetButton("Sprint");
    }

    public bool Shadowstep()
    {
        throw new NotImplementedException();
    }
    //End interface-------------------------
}
