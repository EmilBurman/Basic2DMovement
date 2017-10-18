using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour, IAttack
{

    // Interface-----------------------------
    public void Attack(float hAxis, float yAxis)
    {
        throw new NotImplementedException();
    }

    public void AttackButtonUp()
    {
        throw new NotImplementedException();
    }
    // End interface------------------------

    public GameObject projectile;



    // Use this for initialization
    void Start()
    {
        projectile.GetComponent<Bullet>().SetDirection(Angle.up);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
