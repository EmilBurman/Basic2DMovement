using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroicTimeControll : MonoBehaviour, ITimeControll
{
    // Interface--------------------
    public void FlashReverse(bool flashReverse)
    {
        if (flashReverse && (timeState == TimeState.Ready))
        {
            entity.transform.position = (positionArray[0] as PositionArray).position;
            timeState = TimeState.Reversing;
            positionArray.Clear();
        }
    }

    public void SlowReverse(bool reversing)
    {
        if (reversing && positionArray.Count > 1)
            isReversing = true;
        else
        {
            firstCycle = true;
            isReversing = false;
        }
    }

    public Vector2 GetPositionFromArrayAt(int pos)
    {
        return (positionArray[pos] as PositionArray).position;
    }
    // End interface----------------

    // Set which entity to track
    public GameObject entity;
    private ArrayList positionArray;
    float interpolation;

    //Checks for if player is reversing
    bool isReversing = false;
    bool firstCycle = true;

    // Cooldown and state variables
    private TimeState timeState;
    public float reverseTimer; 			            // Shows the current cooldown.
    float reverseCooldownLimit = 5f;        	// Sets the cooldown of the dash in seconds.

    //Determine how much to save
    private int keyframe = 5;

    //Amount recorded.
    private int frameCounter = 0;

    //Amount played back.
    private int reverseCounter = 0;

    // Variables to interpolate between keyframes
    private Vector2 currentPosition;
    private Vector2 previousPosition;

    // Spawn/destory for the point of max return.
    SpawnBehavior returnPoint;

    void Start()
    {
        positionArray = new ArrayList();
        returnPoint = GetComponent<SpawnBehavior>();
        timeState = TimeState.Ready;
        returnPoint.SpawnObject(true);
    }

    void FixedUpdate()
    {
        if (!isReversing)
        {
            if (frameCounter < keyframe)
                frameCounter += 1;
            else
            {
                frameCounter = 0;
                positionArray.Add(new PositionArray(entity.transform.position));
            }
        }
        ReverseAbility();

        if (positionArray.Count > 15)
            positionArray.RemoveAt(0);
    }

    void RestorePositions()
    {
        int lastIndex = positionArray.Count - 1;
        int secondToLastIndex = positionArray.Count - 2;

        if (secondToLastIndex >= 0)
        {
            currentPosition = (positionArray[lastIndex] as PositionArray).position;
            previousPosition = (positionArray[secondToLastIndex] as PositionArray).position;
            positionArray.RemoveAt(lastIndex);
        }
    }

    private void ReverseAbility()
    {
        switch (timeState)
        {
            case TimeState.Ready:
                if (isReversing)
                {
                    StartCoroutine(ReverseEntityTimeFlow());
                    timeState = TimeState.Reversing;
                }
                break;
            case TimeState.Reversing:
                // Set the cooldown and initate cooldown state.
                reverseTimer += Time.deltaTime * 3;
                if (reverseTimer >= reverseCooldownLimit)
                {
                    reverseTimer = reverseCooldownLimit;
                    returnPoint.DestroyGameObject();
                    timeState = TimeState.Cooldown;
                }
                break;
            case TimeState.Cooldown:
                reverseTimer -= Time.deltaTime;
                if (reverseTimer <= 0)
                {
                    reverseTimer = 0;
                    returnPoint.SpawnObject(true);
                    timeState = TimeState.Ready;
                }
                break;
        }
    }

    IEnumerator ReverseEntityTimeFlow()
    {
        while (isReversing)
        {
            if (reverseCounter > 0)
                reverseCounter -= 1;
            else
            {
                reverseCounter = keyframe;
                RestorePositions();
            }

            if (firstCycle)
            {
                firstCycle = false;
                RestorePositions();
            }
            interpolation = reverseCounter / keyframe;
            entity.transform.position = Vector2.Lerp(previousPosition, currentPosition, interpolation);
            yield return 0; //go to next frame
        }
    }
}

public enum TimeState
{
    Ready,
    Reversing,
    Cooldown
}

public class PositionArray
{
    public Vector2 position;

    public PositionArray(Vector2 position)
    {
        this.position = position;
    }
}
