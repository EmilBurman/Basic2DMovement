using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;

    [Header("Setup for end points.")]
    public Vector3 startPosition;
    public Vector3 endPosition;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawLine(startPosition, endPosition, Color.blue);
        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / speed, 1f)));
    }
}
