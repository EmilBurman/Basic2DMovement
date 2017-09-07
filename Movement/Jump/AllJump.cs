﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllJump : MonoBehaviour, IJump
{
    //Interface----------------------------
    public void Grounded(bool jump, bool sprint)
    {
        SetCanAirJump(true);
        if (jump)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            if (sprint)
                rigidbody2D.AddForce(Vector2.up * jumpForce * 0.8f, ForceMode2D.Impulse);
            else
                rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Airborne(bool jump)
    {
        if (jump && canJump)
        {
            // If player wants to change direction, help them change.
            if (rigidbody2D.velocity.x > 0 && mySpriteRenderer.flipX)
                rigidbody2D.AddForce(Vector2.left * 4f, ForceMode2D.Impulse);
            // If player wants to change direction, help them change.
            else if (rigidbody2D.velocity.x < 0 && !mySpriteRenderer.flipX)
                rigidbody2D.AddForce(Vector2.right * 4f, ForceMode2D.Impulse);

            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 1f);
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            timesJumped++;

            if (timesJumped >= numberOfAirJumps)
                canJump = false;
        }
    }

    public void RightWall(bool jump)
    {
        SetCanAirJump(true);
        if (jump)
        {
            sideJump = new Vector2(-0.7f, 0.9f);
            WallJump();
        }
    }

    public void LeftWall(bool jump)
    {
        SetCanAirJump(true);
        if (jump)
        {
            sideJump = new Vector2(0.7f, 0.9f);
            WallJump();
        }
    }
    // End interface-----------------------

    public float numberOfAirJumps;
    public float jumpForce = 22f;                   // The height the player can jump

    //Internal
    Rigidbody2D rigidbody2D;                        // Reference to the player's rigidbody.
    SpriteRenderer mySpriteRenderer;                // To get the current sprite.
    float sideJumpForce;                            // Force when jumping from a wall.
    float timesJumped;                              // Number of jumps performed.
    bool canJump;                                   // Checks if the player can double jump.
    Vector2 sideJump;                               // The angle when jumping from a wall.

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        sideJumpForce = jumpForce * 1.2f;
    }

    void SetCanAirJump(bool setDoubleJump)
    {
        canJump = setDoubleJump;
        if (canJump)
            timesJumped = 0;
    }

    void WallJump()
    {
        rigidbody2D.velocity = new Vector2(0, 0);
        rigidbody2D.AddForce(sideJump * sideJumpForce, ForceMode2D.Impulse);
    }
}
