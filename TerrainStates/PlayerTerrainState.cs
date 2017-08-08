using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerTerrainState : MonoBehaviour, ITerrainState
{
    //Interface--------------------

    public bool Grounded()
    {
        return IsGrounded();
    }

    public bool Airborne()
    {
        return (!IsGrounded());
    }

    public bool WallLeft()
    {
        return IsWallClimbingLeft();
    }

    public bool WallRight()
    {
        return IsWallClimbingRight();
    }
    //End interface----------------


    // Public variables for terrain checks.
    public LayerMask groundLayer;                           // Layermask variable to check for ground layer.
    public LayerMask wallLayer;                             // Layermask variable to check for ground layer.
    public string state;

    // Internal system variables.
    private Rigidbody2D rigidbody2D;                        // Reference to the player's rigidbody.
    private SpriteRenderer mySpriteRenderer;                // Player sprite.
    private float groundCheckDistanceY;                     // Defines the distance to start checking if the player is grounded.
    private float groundCheckDistanceX;                     // Defines the distance to start checking if the player is grounded.
    private float length;                                   // Defines the distance to check for terrain.

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        groundCheckDistanceY = (GetComponent<Collider2D>().bounds.extents.y + 0.06f) / 1.17f;
        groundCheckDistanceX = (GetComponent<Collider2D>().bounds.extents.x + 0.06f) / 1.18f;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        length = 0.03f;
    }

    bool IsGrounded()
    {
        Color color;
        bool bottomGround1 = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x, rigidbody2D.transform.position.y - groundCheckDistanceY), Vector2.down, length, groundLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x, rigidbody2D.transform.position.y - groundCheckDistanceY), Vector2.down, color = Color.yellow, 0.1f);

        bool bottomGround2 = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x + (groundCheckDistanceX + 0.02f), rigidbody2D.transform.position.y - groundCheckDistanceY), Vector2.down, length, groundLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x + (groundCheckDistanceX + 0.02f), rigidbody2D.transform.position.y - groundCheckDistanceY), Vector2.down, color = Color.green, 0.1f);

        bool bottomGround3 = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x - (groundCheckDistanceX + 0.02f), rigidbody2D.transform.position.y - groundCheckDistanceY), Vector2.down, length, groundLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x - (groundCheckDistanceX + 0.02f), rigidbody2D.transform.position.y - groundCheckDistanceY), Vector2.down, color = Color.red, 0.1f);

        return (bottomGround1 || bottomGround2 || bottomGround3) ? true : false;
    }
    bool IsWallClimbingLeft()
    {
        Color color;
        bool leftWall1 = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x - groundCheckDistanceX, rigidbody2D.transform.position.y), Vector2.left, length, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x - groundCheckDistanceX, rigidbody2D.transform.position.y), Vector2.left, color = Color.yellow, length);

        bool leftWall2 = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x - groundCheckDistanceX, rigidbody2D.transform.position.y - groundCheckDistanceY / 2), Vector2.left, length, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x - groundCheckDistanceX, rigidbody2D.transform.position.y - groundCheckDistanceY / 2), Vector2.left, color = Color.green, length);

        bool leftWall3 = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x - groundCheckDistanceX, rigidbody2D.transform.position.y + groundCheckDistanceY / 2), Vector2.left, length, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x - groundCheckDistanceX, rigidbody2D.transform.position.y + groundCheckDistanceY / 2), Vector2.left, color = Color.red, length);

        return (leftWall1 || leftWall2 || leftWall3) ? true : false;
    }
    bool IsWallClimbingRight()
    {
        Color color;
        bool rightWall1 = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x + groundCheckDistanceX, rigidbody2D.transform.position.y), Vector2.right, length, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x + groundCheckDistanceX, rigidbody2D.transform.position.y), Vector2.right, color = Color.yellow, length);

        bool rightWall2 = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x + groundCheckDistanceX, rigidbody2D.transform.position.y - groundCheckDistanceY / 2), Vector2.right, length, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x + groundCheckDistanceX, rigidbody2D.transform.position.y - groundCheckDistanceY / 2), Vector2.right, color = Color.green, length);

        bool rightWall3 = Physics2D.Raycast(new Vector2(rigidbody2D.transform.position.x + groundCheckDistanceX, rigidbody2D.transform.position.y + groundCheckDistanceY), Vector2.right, length, wallLayer);
        Debug.DrawRay(new Vector2(rigidbody2D.transform.position.x + groundCheckDistanceX, rigidbody2D.transform.position.y + groundCheckDistanceY / 2), Vector2.right, color = Color.red, length);

        return (rightWall1 || rightWall2 || rightWall3) ? true : false;
    }
}
