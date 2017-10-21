using StateEnumerators;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IProjectile
{
    //Interface
    public void SetDirection(Directions direction)
    {
        this.direction = direction;
    }
    //End interface

    [Header("Bullet setup")]
    public float bulletSpeed;

    //Internal variables
    new CircleCollider2D collider;
    new Rigidbody2D rigidbody2D;
    Directions direction;
    Vector2 target;

    // Use this for initialization
    void Awake()
    {
        transform.parent = null;
        collider = GetComponent<CircleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Directions.Up:
                target = new Vector2(0, bulletSpeed);
                break;
            case Directions.Down:
                target = new Vector2(0, -bulletSpeed);
                break;
            case Directions.Left:
                target = new Vector2(-bulletSpeed, 0);
                break;
            case Directions.Right:
                target = new Vector2(bulletSpeed, 0);
                break;
        }
        rigidbody2D.velocity = target;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
        try
        {
            collision.gameObject.GetComponent<IHealth>().TakeDamage(20);
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e);
        }
    }
}
