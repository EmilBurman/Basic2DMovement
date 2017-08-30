using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalToPortal : MonoBehaviour
{
    //Public variables
    public GameObject portal;
    public float offsetDistance;
    public bool up;
    public bool down;
    public bool left;
    public bool right;

    //Interal variables
    Vector3 location;
    Vector3 teleportOffset;

    public Vector3 exitPoint()
    {
        return transform.position + teleportOffset;
    }


    // Use this for initialization
    void Start()
    {
        location = portal.GetComponent<PortalToPortal>().exitPoint();
        if (up)
            teleportOffset = new Vector3(0, offsetDistance, 0);
        if (down)
            teleportOffset = new Vector3(0, -offsetDistance, 0);
        if (left)
            teleportOffset = new Vector3(-offsetDistance, 0, 0);
        if (right)
            teleportOffset = new Vector3(offsetDistance, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, portal.transform.position, Color.green);
        Debug.DrawLine(transform.position, transform.position + teleportOffset, Color.blue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Porting collided entity.");
        collision.transform.position = location;
    }
}