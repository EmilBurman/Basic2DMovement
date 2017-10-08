using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour, IAttack
{
    // Interface----------------------------
    public void Attack()
    {

    }
    public void AirBorneAttack()
    {

    }
    public void AttackButtonUp()
    {

    }
    //End interface--------------------------------------------

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<IHealth>().TakeDamage(20);
    }
}
