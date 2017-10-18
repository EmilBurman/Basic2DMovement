using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void Attack(float hAxis, float yAxis);
    void AttackButtonUp();
}
