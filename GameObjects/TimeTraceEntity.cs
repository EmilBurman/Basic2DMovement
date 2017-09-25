﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTraceEntity : MonoBehaviour 
{
    // Set the tracing GameObject, fetch its time controll script, fetch its position array and its interpolation value
    public GameObject traceEntity;
    ITimeControll timeControllScript;
    float interpolation;

    void Start()
    {
        traceEntity = transform.parent.gameObject;
        timeControllScript = traceEntity.GetComponent<ITimeControll>();
        StartCoroutine(LateStart(2));
    }


    void LateUpdate()
    {
        transform.parent = null;
        transform.position = Vector2.Lerp(transform.position, timeControllScript.GetPositionFromArrayAt(1), 0.09f);

    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = timeControllScript.GetPositionFromArrayAt(0);
    }
}
