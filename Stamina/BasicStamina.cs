using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicStamina : MonoBehaviour
{
    //Interface------------------------------------------------
    void LoseStamina(float staminaLoss)
    {
        // Reduce the current stamina by the stamina loss amount.
        currentStamina -= staminaLoss;

        // Update the slider value
        staminaSlider.value = currentStamina;
    }

    void EarnStamina(float staminaGain)
    {

    }
    float GetCurrentStamina()
    {
        return currentStamina;
    }

    //End interface--------------------------------------------
    [Header("Stamina setup.")]
    public float startingStamina = 100;                             // The amount of health the entity starts the game with.
    public float currentStamina;                                    // The current health of the entity.
    public Slider staminaSlider;                                    // Reference to the UI's health bar.

    void Awake()
    {
        // Set the initial stamina of the entity.
        currentStamina = startingStamina;
    }

}