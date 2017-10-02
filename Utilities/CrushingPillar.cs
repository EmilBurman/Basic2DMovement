using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrushingPillar : MonoBehaviour
{
    public float smoothing,
                   waitTime;
    public bool teleportPlayer;

    [Header("Setup for end points.")]
    public Vector3 startPosition;
    public Vector3 currentPosition;
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
    void Update()
    {

        Debug.DrawLine(startPosition, endPosition, Color.blue);
        if (teleportPlayer)
            Debug.DrawLine(transform.position, exitPoint, Color.green);

        currentPosition = transform.position;

        if (Vector3.Distance(transform.position, startPosition) < 0.06f)
            StartCoroutine(MovePillar(transform, currentPosition, endPosition, 3));
        if (Vector3.Distance(transform.position, endPosition) < 0.06f)
            StartCoroutine(MovePillar(transform, currentPosition, startPosition, 3));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (teleportPlayer)
            collision.transform.position = exitPoint;
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator MovePillar(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0.0f;
        float rate = smoothing / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
