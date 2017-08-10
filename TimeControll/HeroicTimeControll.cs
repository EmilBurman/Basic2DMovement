using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroicTimeControll : MonoBehaviour, ITimeControll
{
    // Interface--------------------
    public void FlashReverse(bool flashReverse)
    {
        if (flashReverse)
        {
            entity.transform.position = (keyframes[0] as Keyframe).position;
            keyframes.Clear();
        }
    }

    public void SlowReverse(bool reversing)
    {
        if (reversing && keyframes.Count > 1)
            isReversing = true;
        else
        {
            firstRun = true;
            isReversing = false;
        }
    }
    // End interface----------------

    // Set which entity to track
    public GameObject entity;
    private ArrayList keyframes;

    //Checks for if player is reversing
    private bool isReversing = false;
    private bool firstRun = true;
    
    // Cooldown and state variables
    private TimeState timeState;
    float reverseTimer; 			// Shows the current cooldown.
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


    void Start()
    {
        keyframes = new ArrayList();
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
                keyframes.Add(new Keyframe(entity.transform.position/*, entity.transform.velocity*/));
            }
        }
        else
            ReverseAbility();

        if (keyframes.Count > 15)
            keyframes.RemoveAt(0);
    }

    void RestorePositions()
    {
        int lastIndex = keyframes.Count - 1;
        int secondToLastIndex = keyframes.Count - 2;

        if (secondToLastIndex >= 0)
        {
            currentPosition = (keyframes[lastIndex] as Keyframe).position;
            previousPosition = (keyframes[secondToLastIndex] as Keyframe).position;
            keyframes.RemoveAt(lastIndex);
        }
    }
    
    private void ReverseAbility()
    {
	switch (timeState)
        {
            case TimeState.Ready:
                if (dash)
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
                    timeState = TimeState.Cooldown;
                }
                break;
            case TimeState.Cooldown:
                reverseTimer -= Time.deltaTime;
                if (reverseTimer <= 0)
                {
                    reverseTimer  = 0;
                    timeState = TimeState.Ready;
                }
                break;
        }
    }
    
    IEnumerator ReverseEntityTimeFlow()
    {
	if (reverseCounter > 0)
        	reverseCounter -= 1;
        else
            {
                reverseCounter = keyframe;
                RestorePositions();
            }

            if (firstRun)
            {
                firstRun = false;
                RestorePositions();
            }
            float interpolation = (float)reverseCounter / (float)keyframe;
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

public class Keyframe
{
    public Vector2 position;
    public Vector2 velocity;

    public Keyframe(Vector2 position)
    {
        this.position = position;
    }
}
