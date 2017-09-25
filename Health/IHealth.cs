using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public interface IHealth
{
    float CurrentHealth();
    void TakeDamage(int amount);
    void Death();
}