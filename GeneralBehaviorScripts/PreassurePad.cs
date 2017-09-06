using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreassurePad : MonoBehaviour
{
     // Which doors to interact with
    public GameObject[] door;

    //Public variables
    public float massTriggerAmount;

    void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i:int=0; i<door.Length; i++)
   	    {
            if (door[i].GetComponent<IDoor>().isClosed() && collision.GetComponent<Rigidbody2D>().mass > massTriggerAmount)
            {
            Debug.Log("Opening door");
       	    door[i].GetComponent<IDoor>().Open();
            }
            else if (door[i].GetComponent<IDoor>().isOpen() && collision.GetComponent<Rigidbody2D>().mass > massTriggerAmount)
            {
            Debug.Log("Closing door");
       	    door[i].GetComponent<IDoor>().Close();
   	        }
        }
    }

    void Update()
    {
        for(int i:int=0; i<door.Length; i++)
   	    {
        Debug.DrawLine(transform.position, door[i].transform.position, Color.green);
    	}
   }
}
