using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : MonoBehaviour, IDoor {
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
        if (Vector3.Distance(transform.position, endPosition) < 0.06f)
            return true;
        else
            return false;
    }
    public bool isClosed()
    {
        if (Vector3.Distance(transform.position, startPosition) < 0.06f)
            return true;
        else
            return false;
    }
    public void Open()
    {
        if (toSetPoint)
            StartCoroutine(MoveDoor(endPosition));
    }
    public void Close()
    {
        if (toSetPoint)
            StartCoroutine(MoveDoor(startPosition));
    }
    //End interface-------------------------

    //Move between two points
    public bool toSetPoint;
    public Vector3 startPosition,
                   currentPosition,
                   endPosition;

    public float smoothing = 1f;

    void Awake()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        Debug.DrawLine(startPosition, endPosition, Color.blue);
        currentPosition = transform.position;
    }

    IEnumerator MoveDoor(Vector3 toPosition)
    {
        while (Vector3.Distance(transform.position, toPosition) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, toPosition, smoothing * Time.deltaTime);
            yield return null;
        }
        print("Reached the target.");
        yield return new WaitForSeconds(3f);
        print("MyCoroutine is now finished.");
    }
}

