using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTraceEntity : MonoBehaviour
{
    // Set the tracing GameObject, fetch its time controll script, fetch its position array and its interpolation value
    public GameObject traceEntity;
    ITimeControll timeControllScript;
    float interpolation;

    Vector2 positionArray;

    // Get the linerenderer on this object
    LineRenderer entityTrail;

    void Start()
    {
        traceEntity = transform.parent.gameObject;
        timeControllScript = traceEntity.GetComponent<ITimeControll>();
        positionArray = timeControllScript.GetPositionFromArrayAt(0);
        entityTrail = GetComponent<LineRenderer>();
        transform.position = timeControllScript.GetPositionFromArrayAt(0);
    }


    void LateUpdate()
    {
        transform.parent = null;
        transform.position = Vector2.Lerp(transform.position, timeControllScript.GetPositionFromArrayAt(1), 0.3f);
    }

}
