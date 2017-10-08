using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet setup.")]
    public float bulletSpeed;

    //Internal variables
    CircleCollider2D collider;
    new Rigidbody2D rigidbody2D;

    // Use this for initialization
    void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(bulletSpeed, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<IHealth>().TakeDamage(20);
        }
        else
            Destroy(this.gameObject);
    }
}
