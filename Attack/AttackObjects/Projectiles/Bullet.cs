using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet setup")]
    public float bulletSpeed;

    public void SetDirection (Angle direction)
    {

    }

    //Internal variables
    new CircleCollider2D collider;
    new Rigidbody2D rigidbody2D;
    Vector2 exitDirection;
    public Angle angleDirection;

    // Use this for initialization
    void Awake()
    {
        transform.parent = null;
        collider = GetComponent<CircleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        switch (angleDirection)
        {
            case Angle.up:
                exitDirection = new Vector2(0, bulletSpeed);
                break;
            case Angle.right:
                exitDirection = new Vector2(bulletSpeed, 0);
                break;
            case Angle.down:
                exitDirection = new Vector2(0, -bulletSpeed);
                break;
            case Angle.left:
                exitDirection = new Vector2(-bulletSpeed, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = exitDirection;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<IHealth>().TakeDamage(20);
        }
        else
            Destroy(gameObject);
    }
}
