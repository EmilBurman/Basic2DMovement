using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public interface IHealth
{
    float CurrentHealth();
    void TakeDamage(float amount);
    bool Invulnerable(bool state);
    void Death();
}