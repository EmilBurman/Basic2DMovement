﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaFlower : MonoBehaviour
{

    public int scoreValue = 10;             // The amount added to the player's score when collecting.
    public bool playerOnly;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "Player", if it is...
        if (playerOnly)
        {
            if (other.gameObject.CompareTag(Tags.PLAYER))
            {
                this.gameObject.SetActive(false);
                Invoke("Respawn", 10);
                other.gameObject.GetComponent<IStamina>().EarnStamina(4000);
            }
        }
        else
        {
            if (other.gameObject.CompareTag(Tags.PLAYER))
            {
                other.gameObject.GetComponent<IStamina>().EarnStamina(600);
                Destroy(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }
    }
    void Respawn()
    {
        this.gameObject.SetActive(true);
    }
}
