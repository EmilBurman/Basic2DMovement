﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    //Interface------------------------------------------------
    public float CurrentHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Play the hurt sound effect.
        audioComponent.Play();

        // If the entity has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
            // ... it should die.
            Death();
    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Tell the animator that the player is dead.
        anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        audioComponent.clip = deathClip;
        audioComponent.Play();
    }


    //End interface--------------------------------------------

    [Header("Health setup.")]
    public int startingHealth = 100;                            // The amount of health the entity starts the game with.
    public int currentHealth;                                   // The current health of the entity.
    public AudioClip deathClip;                                 // The audio clip to play when the entity dies.

    [Header("Hurt Image")]
    public bool showImage = true;				// Should the script use a flash image?
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    // Internal system variables.
    Animator anim;                                              // Reference to the Animator component.
    AudioSource audioComponent;                                 // Reference to the AudioSource component.
    bool isDead;                                                // Whether the entity is dead.
    bool damaged;                                               // True when the player gets damaged.


    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        audioComponent = GetComponent<AudioSource>();

        // Set the initial health of the entity.
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (damageImage == null)
            // Do nothing

            if (showImage)
            {
                // If the entity has just been damaged...
                if (damaged)
                    // ... set the colour of the damageImage to the flash colour.
                    damageImage.color = flashColour;
                // Otherwise...
                else
                    // ... transition the colour back to clear.
                    damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
        // Reset the damaged flag.
        damaged = false;
    }
}