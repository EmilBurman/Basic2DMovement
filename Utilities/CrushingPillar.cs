using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrushingPillar : MonoBehaviour
{
    public float speed;
    public bool teleportPlayer;

    [Header("Setup for end points.")]
    public Vector3 startPosition;
    public Vector3 endPosition;

    [Header("Setup for exit point")]
    public Vector3 exitPoint;


    float startTime;
    float distCovered;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawLine(startPosition, endPosition, Color.blue);
        if (teleportPlayer)
            Debug.DrawLine(transform.position, exitPoint, Color.green);

        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / speed, 1f)));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (teleportPlayer)
            collision.transform.position = exitPoint;
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
