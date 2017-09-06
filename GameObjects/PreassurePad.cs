using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreassurePad : MonoBehaviour
{
    // Which doors to interact with
    public GameObject[] doors;

    //Public variables
    public float massTriggerAmount;

    void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].GetComponent<IDoor>().isClosed() && collision.GetComponent<Rigidbody2D>().mass > massTriggerAmount)
            {
                Debug.Log("Opening door");
                doors[i].GetComponent<IDoor>().Open();
            }
            else if (doors[i].GetComponent<IDoor>().isOpen() && collision.GetComponent<Rigidbody2D>().mass > massTriggerAmount)
            {
                Debug.Log("Closing door");
                doors[i].GetComponent<IDoor>().Close();
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            Debug.DrawLine(transform.position, doors[i].transform.position, Color.green);
        }
    }
}
