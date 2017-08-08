using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroicMovement : MonoBehaviour, IMovement
{
    // Interface-------------------------------------------
    public void Airborne(float horizontalAxis, bool sprint)
    {
        float airborneMovement = 0.5f;
        // Help change direction in the air to the right
        if (horizontalAxis > 0 && mySpriteRenderer.flipX)
        {
            rigidbody2D.angularVelocity = 0f;
            rigidbody2D.AddForce(vector2Right * 1.4f, ForceMode2D.Impulse);
        }
        // If not changing direction continue as normal.
        else if (horizontalAxis > 0)
            rigidbody2D.AddForce(vector2Right * airborneMovement, ForceMode2D.Impulse);

        // Help change direction in the air to the left
        if (horizontalAxis < 0 && !mySpriteRenderer.flipX)
        {
            rigidbody2D.angularVelocity = 0f;
            rigidbody2D.AddForce(vector2Left * 1.4f, ForceMode2D.Impulse);
        }
        // If not changing direction continue as normal.
        else if (horizontalAxis < 0)
            rigidbody2D.AddForce(vector2Left * airborneMovement, ForceMode2D.Impulse);

        // If player is falling, increase gravity
        if (rigidbody2D.velocity.y < -0.001)
            rigidbody2D.AddForce(Physics.gravity * 1.4f, ForceMode2D.Force);
        MovementSpeedClamp();
    }

    public void Grounded(float horizontalAxis, bool sprint)
    {
        SetCanWallRide(true);
        rigidbody2D.velocity = new Vector2(horizontalAxis * moveSpeed, rigidbody2D.velocity.y);

        if (sprint)
        {
            if (rigidbody2D.velocity.x > 0)
                rigidbody2D.AddForce(vector2Right * (moveSpeed * sprintForceMultiplier), ForceMode2D.Force);
            else if (rigidbody2D.velocity.x < 0)
                rigidbody2D.AddForce(vector2Left * (moveSpeed * sprintForceMultiplier), ForceMode2D.Force);
        }
    }

    public void Wallride(float horizontalAxis, bool sprint)
    {
        if (rigidbody2D.velocity.y > -0.001 && sprint && canWallRide)
        {
            rigidbody2D.AddForce(Vector2.up * Mathf.Abs(horizontalAxis * rigidbody2D.velocity.x * 1.3f), ForceMode2D.Impulse);
            canWallRide = false;
        }
        MovementSpeedClamp();
    }
    //End interface----------------------------------------

    // Variables needed for movement.
    public float moveSpeed = 8.0f;                          // The speed that the player will move at.
    public float sprintForceMultiplier = 40.0f;             // The factor which will * the player speed when sprinting.
    bool canWallRide;                                       // Checks if the player can wallride
    private Rigidbody2D rigidbody2D;                         // Reference to the player's rigidbody.
    private SpriteRenderer mySpriteRenderer;                // To get the current sprite.
    private Vector2 vector2Right;
    private Vector2 vector2Left;

    // Use this for initialization
    void Awake()
    {
        // Set up references.
        rigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        vector2Right = new Vector2(1f, rigidbody2D.velocity.y);
        vector2Left = new Vector2(-1f, rigidbody2D.velocity.y);
    }
    private void SetCanWallRide(bool setWallRide)
    {
        canWallRide = setWallRide;
    }

    private void MovementSpeedClamp()
    {
        if (rigidbody2D.velocity.sqrMagnitude > moveSpeed * 1.3f)
            rigidbody2D.velocity *= 0.97f;
    }
}
