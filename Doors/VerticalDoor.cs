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
        if (rigidbody2D.position == endPosition)
            return true;
        else
            return false;
    }
    public bool isClosed()
    {
        if (rigidbody2D.position == startPosition)
            return true;
        else
            return false;
    }
    void Open()
    {
        if (toSetPoint && isClosed())
            transform.position = Vector2.Lerp(startPosition, endPosition, 1f);
    }
    void Close()
    {
        if (toSetPoint && isOpen())
            transform.position = Vector2.Lerp(endPosition, startPosition, 1f);

    }
    //End interface-------------------------

    //Move between two points
    public bool toSetPoint;
    public Vector2 startPosition;
    public Vector2 endPosition;

    //Internal variable
    Rigidbody2D rigidbody2D;                                        // Reference to the entity's rigidbody.

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = rigidbody2D.position;
    }
    void Update()
    {
        Debug.DrawLine(startPosition, endPosition, Color.red);
    }

    void IDoor.Open()
    {
        throw new NotImplementedException();
    }

    void IDoor.Close()
    {
        throw new NotImplementedException();
    }
}
