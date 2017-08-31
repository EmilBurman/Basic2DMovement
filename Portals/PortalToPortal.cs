using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalToPortal : MonoBehaviour
{
    //Public variables
    public GameObject portal;
	public bool portalToPortal;
	public Vector3 offSetDistance; 
    public Vector3 exitPoint;  	

	//Internal variables
	Vector3 portalOffset;

	void Start()
    	{
		if (transform.rotation.eulerAngles.z >= 0 && transform.rotation.eulerAngles.z < 90)
			angle = angle.up
		if (transform.rotation.eulerAngles.z >= 90 && transform.rotation.eulerAngles.z < 180)
			angle = angle.right
		if (transform.rotation.eulerAngles.z >= 180 && transform.rotation.eulerAngles.z < 270)
			angle = angle.down
		if (transform.rotation.eulerAngles.z >= 270 && transform.rotation.eulerAngles.z < 360)
			angle = angle.left
    	}

    	// Update is called once per frame
    	void Update()
    	{
		if(portalToPortal)
        		Debug.DrawLine(transform.position, portal.transform.position, Color.green);
		else
			Debug.DrawLine(transform.position, exitPoint, Color.green);
    	}

	private enum angle{up,down,right,left}

    	public Vector3 portalExitPoint(Vector3 entitySize)
    	{
		switch (angle)
		case angle.up:
			entityOffset = new Vector3(0,entitySize.y,0);
			offSetDistanceFixed = new Vector3(0,portalOffset,0);
			break;
		case angle.right:
			entityOffset = new Vector3(entitySize.x,0,0);
			offSetDistanceFixed = new Vector3(portalOffset,0,0);
			break;
		case angle.down:
			entityOffset = new Vector3(0,-entitySize.y,0);
			offSetDistanceFixed = new Vector3(0,-portalOffset,0);
			break;
		case angle.left:
			entityOffset = new Vector3(-entitySize.x,0,0);
			offSetDistanceFixed = new Vector3(-portalOffset,0,0);
			break;
        	return transform.position + entityOffset + offSetDistanceFixed;
    	}

	private void OnTriggerEnter2D(Collider2D collision)
    	{
		if (portalToPortal)
			collision.transform.position = portal.GetComponent<PortalToPortal>().portalExitPoint(collision.bounds);
		else
			collision.transform.position = new Vector3(exitPoint);
	}
}
