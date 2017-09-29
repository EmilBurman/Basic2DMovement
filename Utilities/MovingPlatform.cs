using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float smoothing,
                 waitTime;
    public Vector3 startPosition,
                   currentPosition,
                   endPosition;
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
        currentPosition = transform.position;

        if (Vector3.Distance(transform.position, startPosition) < 0.06f)
            StartCoroutine(MovePlatform(transform, currentPosition, endPosition, 3));
        if (Vector3.Distance(transform.position, endPosition) < 0.06f)
            StartCoroutine(MovePlatform(transform, currentPosition, startPosition, 3));
    }

    IEnumerator MovePlatform(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
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
