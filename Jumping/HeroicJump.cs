using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroicJump : MonoBehaviour, IJump
{
    //Interface----------------------------
    public void Grounded(bool jump, bool sprint)
    {
        SetCanDoubleJump(true);
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
        if (jump && canDoubleJump)
        {
            // If player wants to change direction, help them change.
            if (rigidbody2D.velocity.x > 0 && mySpriteRenderer.flipX)
                rigidbody2D.AddForce(Vector2.left * 4f, ForceMode2D.Impulse);
            // If player wants to change direction, help them change.
            else if (rigidbody2D.velocity.x < 0 && !mySpriteRenderer.flipX)
                rigidbody2D.AddForce(Vector2.right * 4f, ForceMode2D.Impulse);

            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 1f);
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canDoubleJump = false;
        }
    }

    public void RightWall(bool jump)
    {
        SetCanDoubleJump(true);
        if (jump)
        {
            sideJump = new Vector2(-0.7f, 0.9f);
            WallJump();
        }
    }

    public void LeftWall(bool jump)
    {
        SetCanDoubleJump(true);
        if (jump)
        {
            sideJump = new Vector2(0.7f, 0.9f);
            WallJump();
        }
    }
    // End interface-----------------------

    Rigidbody2D rigidbody2D;                         // Reference to the player's rigidbody.
    public float jumpForce = 22f;                   // The height the player can jump
    private SpriteRenderer mySpriteRenderer;        // To get the current sprite.
    private float sideJumpForce;                    // Force when jumping from a wall.
    private bool canDoubleJump;                     // Checks if the player can double jump.
    private Vector2 sideJump;                       // The angle when jumping from a wall.

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        sideJumpForce = jumpForce * 1.2f;
    }

    void SetCanDoubleJump(bool setDoubleJump)
    {
        canDoubleJump = setDoubleJump;
    }

    void WallJump()
    {
        rigidbody2D.velocity = new Vector2(0, 0);
        rigidbody2D.AddForce(sideJump * sideJumpForce, ForceMode2D.Impulse);
    }
}
