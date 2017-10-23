using StateEnumerators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBlast : MonoBehaviour, IProjectile
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
    public float knockback;
    public float damage;
    public double lifeSpan;

    Directions direction;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
