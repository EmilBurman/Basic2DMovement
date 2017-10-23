using StateEnumerators;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class ShotgunPellet : MonoBehaviour, IProjectile
{
    //Interface
    public void SetDirection(Directions direction)
    {
        this.direction = direction;
    }
    public Directions GetDirection()
    {
        return direction;
    }
    public float GetKnockback()
    {
        return knockback;
    }
    public void SetSafeTags()
    {

    }
    //End interface

    [Header("Bullet setup")]
    public float bulletSpeed;
    public float knockback;
    public float damage;
    public double lifeSpan;

    //Internal variables
    new CircleCollider2D collider;
    new Rigidbody2D rigidbody2D;
    Directions direction;
    Vector3 targetDirection;
    float time;
    double randomLifespan;

    // Use this for initialization
    void Awake()
    {
        transform.position = transform.parent.position;
        direction = GetComponentInParent<IProjectile>().GetDirection();
        // transform.parent = null;
        collider = GetComponent<CircleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        time = 0f;
        randomLifespan = GetRandomNumber(0.01, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Directions.Up:
                targetDirection = new Vector3(UnityEngine.Random.Range(-10, 10), bulletSpeed);
                break;
            case Directions.Down:
                targetDirection = new Vector3(UnityEngine.Random.Range(-10, 10), -bulletSpeed);
                break;
            case Directions.Left:
                targetDirection = new Vector3(-bulletSpeed, UnityEngine.Random.Range(-10, 10));
                break;
            case Directions.Right:
                targetDirection = new Vector3(bulletSpeed, UnityEngine.Random.Range(-10, 10));
                break;
        }
        rigidbody2D.velocity = targetDirection;
        float angle = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        time += Time.deltaTime;
        if (time > randomLifespan)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.PROJECTILE))
            Destroy(gameObject);
        if (collision.gameObject.CompareTag(Tags.ENEMY) || collision.gameObject.CompareTag(Tags.PLAYER))
            collision.gameObject.GetComponent<IHealth>().TakeDamage(damage);
    }

    double GetRandomNumber(double minimum, double maximum)
    {
        System.Random random = new System.Random();
        return random.NextDouble() * (maximum - minimum) + minimum;
    }
}
