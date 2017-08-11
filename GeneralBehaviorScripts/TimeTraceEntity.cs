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

    ArrayList positionArray;

    // Get the linerenderer on this object
    LineRenderer entityTrail;


    void Start()
    {
        traceEntity = transform.parent.gameObject;
        timeControllScript = traceEntity.GetComponent<ITimeControll>();
        interpolation = timeControllScript.interpolation;
        positionArray = timeControllScript.GetPositionArray();
        entityTrail = GetComponent<LineRenderer>();
    }


    void FixedUpdate()
    {
        transform.position = Vector2.Lerp((positionArray[0] as PositionArray).position, (positionArray[1] as PositionArray).position, interpolation);
    }

}
/*
public class TimeEntityPositionArray
{
    public Vector2 position;

    public TimeEntityPositionArray(Vector2 position)
    {
        this.position = position;
    }
}
*/
