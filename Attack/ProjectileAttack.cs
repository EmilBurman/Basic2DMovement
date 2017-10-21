using UnityEngine;
using StateEnumerators;
using System;

public class ProjectileAttack : MonoBehaviour, IAttack
{

    // Interface-----------------------------
    public void GroundedAttack(bool attack, bool lockedShooting, float hAxis, float yAxis)
    {
        if (lockedShooting)
            LockedShooting();
        else
            SetProjectileDirection(hAxis, yAxis);
        Attack(attack);
    }

    public void AirborneAttack(bool attack, bool lockedShooting, float hAxis, float yAxis)
    {
        if (lockedShooting)
            LockedShooting();
        else
            SetProjectileDirection(hAxis, yAxis);
        Attack(attack);
    }

    public void WallAttackLeft(bool attack, bool lockedShooting)
    {
        if (lockedShooting)
            LockedShooting();
        else
            projectileDirection = Directions.Right;
        Attack(attack);
    }

    public void WallAttackRight(bool attack, bool lockedShooting)
    {
        if (lockedShooting)
            LockedShooting();
        else
            projectileDirection = Directions.Left;
        Attack(attack);
    }
    public void AttackButtonUp(bool attack)
    {
        throw new NotImplementedException();
    }
    // End interface------------------------

    public float attackCooldown;                                // Sets the cooldown of the dash in seconds.
    public GameObject projectilePrefab;
    GameObject projectile;
    AttackState attackState;
    Directions projectileDirection;
    SpriteRenderer spriteRenderer;                              // Get entity sprite.
    float attackTimer;                                          // Shows the current cooldown.
    Vector2 offset;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Attack(bool attack)
    {
        offset = transform.position;
        switch (attackState)
        {
            case AttackState.Ready:
                if (attack)
                {
                    switch (projectileDirection)
                    {
                        case Directions.Right:
                            offset.x += 0.6f;
                            projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                            projectile.GetComponent<IProjectile>().SetDirection(Directions.Right);
                            break;
                        case Directions.Left:
                            offset.x -= 0.6f;
                            projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                            projectile.GetComponent<IProjectile>().SetDirection(Directions.Left);
                            break;
                        case Directions.Up:
                            offset.y += 0.6f;
                            projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                            projectile.GetComponent<IProjectile>().SetDirection(Directions.Up);
                            break;
                        case Directions.Down:
                            offset.y -= 0.6f;
                            projectile = Instantiate(projectilePrefab, offset, Quaternion.Euler(0, 180, 0));
                            projectile.GetComponent<IProjectile>().SetDirection(Directions.Down);
                            break;
                    }
                    attackState = AttackState.Attacking;
                }
                break;
            case AttackState.Attacking:
                attackTimer += Time.deltaTime * 3;
                if (attackTimer >= attackCooldown)
                {
                    attackTimer = attackCooldown;
                    attackState = AttackState.Cooldown;
                }
                break;
            case AttackState.Cooldown:
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {
                    attackTimer = 0;
                    attackState = AttackState.Ready;
                }
                break;
        }
    }

    void SetProjectileDirection(float hAxis, float yAxis)
    {
        if (Mathf.Abs(hAxis) >= Mathf.Abs(yAxis))
            HorziontalFire(hAxis);
        else if (Mathf.Abs(hAxis) < Mathf.Abs(yAxis))
            VeritcalFire(yAxis);
    }

    void HorziontalFire(float hAxis)
    {
        if (spriteRenderer.flipX)
            projectileDirection = Directions.Left;
        else if (!spriteRenderer.flipX)
            projectileDirection = Directions.Right;
    }

    void VeritcalFire(float yAxis)
    {
        if (yAxis < 0)
            projectileDirection = Directions.Down;
        else if (yAxis > 0)
            projectileDirection = Directions.Up;
    }

    void LockedShooting()
    {

    }
}
