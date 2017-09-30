using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMultiDirection : MonoBehaviour, IDash
{

    // Interface----------------------------
    public void Dash(float horizontalAxis, float verticalAxis, bool dash)
    {
        DashAbility(horizontalAxis, verticalAxis, dash);
    }

    public void ResetDash()
    {
        dashTimer = 0;
        dashState = DashState.Ready;
    }
    // End interface------------------------

    new Rigidbody2D rigidbody2D;                        // Reference to the player's rigidbody.
    public DashState dashState;                     // Shows the current state of dashing.
    float dashTimer;                                // Shows the current cooldown.
    float dashCooldownLimit = 1f;                   // Sets the cooldown of the dash in seconds.
    float boostSpeed = 50f;
    float hAxis;
    float yAxis;
    Vector2 boostSpeedRight;
    Vector2 boostSpeedLeft;
    Vector2 boostSpeedUp;
    Vector2 boostSpeedDown;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boostSpeedRight = new Vector2(boostSpeed, 0);
        boostSpeedLeft = new Vector2(-boostSpeed, 0);
        boostSpeedUp = new Vector2(0, boostSpeed);
        boostSpeedDown = new Vector2(0, -boostSpeed);
    }

    void DashAbility(float horizontalAxis, float verticalAxis, bool dash)
    {
        switch (dashState)
        {
            case DashState.Ready:
                if (dash)
                {
                    hAxis = horizontalAxis;
                    yAxis = verticalAxis;
                    if (Mathf.Abs(hAxis) >= Mathf.Abs(yAxis))
                        StartCoroutine(HorizontalDash(0.1f));
                    else if (Mathf.Abs(hAxis) < Mathf.Abs(yAxis))
                        StartCoroutine(VerticalDash(0.1f));
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                // Set the cooldown and initate cooldown state.
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= dashCooldownLimit)
                {
                    dashTimer = dashCooldownLimit;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }
    IEnumerator HorizontalDash(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {
        float time = 0f; //create float to store the time this coroutine is operating
        while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            if (hAxis > 0)
                rigidbody2D.velocity = boostSpeedRight;
            else if (hAxis < 0)
                rigidbody2D.velocity = boostSpeedLeft;
            yield return 0; //go to next frame
        }
        rigidbody2D.velocity = new Vector2(0, 0.5f);
    }

    IEnumerator VerticalDash(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
    {
        float time = 0f; //create float to store the time this coroutine is operating
        while (boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            if (yAxis > 0)
                rigidbody2D.velocity = boostSpeedUp;
            else if (yAxis < 0)
                rigidbody2D.velocity = boostSpeedDown;
            yield return 0; //go to next frame
        }
        rigidbody2D.velocity = new Vector2(0, 0.5f);
    }
}
