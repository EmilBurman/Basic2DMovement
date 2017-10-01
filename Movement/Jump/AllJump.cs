using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllJump : MonoBehaviour, IJump
{
    //Interface----------------------------
    public void SetContinousJump(bool continuousJump, bool endJump)
    {
        continuedGroundJump = continuousJump;
        // Stop the continuous jump if the button is released.
        if (endJump)
        {
            jumpTimeCounter = 0;
            stoppedGroundJump = true;
        }
    }

    public void Grounded(bool jump, bool sprint)
    {
        jumpTimeCounter = jumpTime;
        stoppedGroundJump = false;
        SetCanAirJump(true);
        if (jump)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            if (sprint)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce / 1.5f * 0.8f);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce / 1.5f);
            }
        }
    }

    public void Airborne(bool jump)
    {
        if (!stoppedGroundJump)
            ContinuousGroundedJump();
        else if (jump && canJump)
        {
            // If player wants to change direction, help them change.
            if (rigidbody2D.velocity.x > 0 && mySpriteRenderer.flipX)
                rigidbody2D.AddForce(Vector2.left * 4.5f, ForceMode2D.Impulse);
            // If player wants to change direction, help them change.
            else if (rigidbody2D.velocity.x < 0 && !mySpriteRenderer.flipX)
                rigidbody2D.AddForce(Vector2.right * 4.5f, ForceMode2D.Impulse);

            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 2f);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
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

    [Header("Jump variables.")]
    public float numberOfAirJumps;
    public float jumpForce = 22f;                   // The height the player can jump
    public float jumpTime;                          // The time the player can continue to jump from the ground.

    //Internal
    new Rigidbody2D rigidbody2D;                    // Reference to the player's rigidbody.
    SpriteRenderer mySpriteRenderer;                // To get the current sprite.
    float sideJumpForce;                            // Force when jumping from a wall.
    float timesJumped;                              // Number of jumps performed.
    float jumpTimeCounter;                          // A counter to keep track of how long the player has been jumping.
    bool continuedGroundJump;
    bool canJump;                                   // Checks if the player can double jump.
    bool stoppedGroundJump;                         // Checks if the player has stopped a grounded jump.
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

    void ContinuousGroundedJump()
    {
        if (continuedGroundJump && jumpTimeCounter > 0 && !stoppedGroundJump)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce / 2.5f);
            jumpTimeCounter -= Time.deltaTime;
        }
    }
}