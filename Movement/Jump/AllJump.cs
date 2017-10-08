using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllJump : MonoBehaviour, IJump
{
    //Interface----------------------------
    public void SetContinousJump(bool continuousJump, bool endJump)
    {
        continuedJump = continuousJump;
        // Stop the continuous jump if the button is released.
        if (endJump)
        {
            jumpTimeCounter = 0;
            stoppedGroundJump = true;
            needToReleaseJump = false;
        }
    }

    public void Grounded(bool jump, bool sprint)
    {
        jumpTimeCounter = continuousJumpTime; 
        SetCanAirJump(true);
        if (jump && stoppedGroundJump)
        {
            needToReleaseJump = true;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            if (sprint)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce / 1.2f * 0.8f);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce / 1.2f);
            }
            stoppedGroundJump = false;
        }
        else
            ContinuousGroundedJump();
    }

    public void Airborne(bool jump)
    {
        if (!stoppedGroundJump)
            ContinuousGroundedJump();
        else if (jump && canAirJump && !needToReleaseJump)
        {
            // If player wants to change direction, help them change.
            if (rigidbody2D.velocity.x > 0 && mySpriteRenderer.flipX)
                rigidbody2D.AddForce(Vector2.left * 4.5f, ForceMode2D.Impulse);
            // If player wants to change direction, help them change.
            else if (rigidbody2D.velocity.x < 0 && !mySpriteRenderer.flipX)
                rigidbody2D.AddForce(Vector2.right * 4.5f, ForceMode2D.Impulse);

            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 2f);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce * 1.2f);
            timesJumped++;
            needToReleaseJump = true;

            if (timesJumped >= numberOfAirJumps)
                canAirJump = false;
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
    public float jumpForce = 16f;                   // The height the player can jump
    public float continuousJumpTime;                // The time the player can continue to jump from the ground.

    //Internal
    new Rigidbody2D rigidbody2D;                    // Reference to the player's rigidbody.
    SpriteRenderer mySpriteRenderer;                // To get the current sprite.
    float sideJumpForce;                            // Force when jumping from a wall.
    float timesJumped;                              // Number of jumps performed.
    float jumpTimeCounter;                          // A counter to keep track of how long the player has been jumping.
    bool continuedJump;                             // Checks if the jump button is held down.
    bool canAirJump;                                // Checks if the entity can double jump.
    bool needToReleaseJump;                         // Checks if they player has released the jump button
    bool stoppedGroundJump;                         // Checks if the entity has stopped a grounded jump.
    Vector2 sideJump;                               // The angle when jumping from a wall.

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        sideJumpForce = jumpForce * 1.2f;
    }

    void SetCanAirJump(bool setDoubleJump)
    {
        canAirJump = setDoubleJump;
        if (canAirJump)
            timesJumped = 0;
    }

    void WallJump()
    {
        if (!stoppedGroundJump)
            ContinuousGroundedJump();
        else if (!needToReleaseJump)
        {
            rigidbody2D.velocity = new Vector2(0, 0);
            rigidbody2D.AddForce(sideJump * sideJumpForce, ForceMode2D.Impulse);
            needToReleaseJump = true;
        }
    }

    void ContinuousGroundedJump()
    {
        if (continuedJump && jumpTimeCounter > 0)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce / 2f);
            jumpTimeCounter -= Time.deltaTime;
        }
    }
}
