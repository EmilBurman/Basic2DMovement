using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    //Public variables
    public GameObject portal;
    public bool portalToPortal;
    public float offSetDistance;
    public Vector3 exitPoint;
    public bool isActive;

    //Internal variables
    Angle angleDirection;
    Vector3 entityOffset;
    Vector3 offSetDistanceFixed;
    Vector2 exitDirection;
    float speed;

    void Start()
    {
        if (transform.rotation.eulerAngles.z >= 0 && transform.rotation.eulerAngles.z < 90)
            angleDirection = Angle.up;

        if (transform.rotation.eulerAngles.z >= 90 && transform.rotation.eulerAngles.z < 180)
            angleDirection = Angle.left;

        if (transform.rotation.eulerAngles.z >= 180 && transform.rotation.eulerAngles.z < 270)
            angleDirection = Angle.down;

        if (transform.rotation.eulerAngles.z >= 270 && transform.rotation.eulerAngles.z < 360)
            angleDirection = Angle.right;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (portalToPortal)
            Debug.DrawLine(transform.position, portal.transform.position, Color.green);
        else
            Debug.DrawLine(transform.position, exitPoint, Color.green);
    }

    public Vector3 portalExitPoint(Bounds entitySize)
    {
        switch (angleDirection)
        {
            case Angle.up:
                entityOffset = new Vector3(0, entitySize.extents.y, 0);
                offSetDistanceFixed = new Vector3(0, offSetDistance, 0);
                exitDirection = new Vector2(0, 1);
                break;
            case Angle.right:
                entityOffset = new Vector3(entitySize.extents.x, 0, 0);
                offSetDistanceFixed = new Vector3(offSetDistance, 0, 0);
                exitDirection = new Vector2(1, 0);
                break;
            case Angle.down:
                entityOffset = new Vector3(0, -entitySize.extents.y, 0);
                offSetDistanceFixed = new Vector3(0, -offSetDistance, 0);
                exitDirection = new Vector2(0, -1);
                break;
            case Angle.left:
                entityOffset = new Vector3(-entitySize.extents.x, 0, 0);
                offSetDistanceFixed = new Vector3(-offSetDistance, 0, 0);
                exitDirection = new Vector2(-1, 0);
                break;
        }
        return transform.position + entityOffset + offSetDistanceFixed;
    }

    public void setActive(bool state)
    {
        isActive = state;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive)
        {
            float velocityX = collision.GetComponent<Rigidbody2D>().velocity.x;
            float velocityY = collision.GetComponent<Rigidbody2D>().velocity.y;
            if (velocityX > velocityY)
                speed = Mathf.Abs(velocityX);
            if (velocityX < velocityY)
                speed = Mathf.Abs(velocityY);

            if (portalToPortal)
            {
                portal.GetComponent<PortalBehavior>().setActive(true);
                collision.transform.position = portal.GetComponent<PortalBehavior>().portalExitPoint(collision.bounds);
                collision.GetComponent<Rigidbody2D>().AddForce(exitDirection * (speed*1.2f), ForceMode2D.Impulse);
                portal.GetComponent<PortalBehavior>().setActive(false);
            }
            else
                collision.transform.position = exitPoint;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false;
    }
}
public enum Angle { up, down, right, left }
