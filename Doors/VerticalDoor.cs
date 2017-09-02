using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDoor : MonoBehaviour, IDoor {
    //Interface------------------------------
    public bool canBeOpened()
    {
        throw new NotImplementedException();
    }

    public bool canBeClosed()
    {
        throw new NotImplementedException();
    }

    public bool isOpen()
    {
        if (transform.position == endPosition)
            return true;
        else
            return false;
    }
    public bool isClosed()
    {
        if (transform.position == startPosition)
            return true;
        else
            return false;
    }
    public void Open()
    {
        if (toSetPoint)
            MoveDoor(endPosition);
    }
    public void Close()
    {
        if (toSetPoint)
            MoveDoor(startPosition);

    }
    //End interface-------------------------

    //Move between two points
    public bool toSetPoint;
    public float doorSpeed;
    public Vector3 startPosition;
    public Vector3 endPosition;

    void Awake()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        Debug.DrawLine(startPosition, endPosition, Color.blue);
    }

    void MoveDoor(Vector3 toPosition)
    {
        Vector3 currentPosition = transform.position;
        if (!(transform.position == toPosition))
            transform.position = Vector3.Lerp(currentPosition, toPosition, doorSpeed);
    }
}
