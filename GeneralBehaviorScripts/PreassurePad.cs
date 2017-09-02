using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreassurePad : MonoBehaviour
{
    // Which door to interact with
    public GameObject door;
    IDoor doorControllScript;

    //Public variables
    public float massTriggerAmount;

    void Start()
    {
        doorControllScript = door.GetComponent<IDoor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorControllScript.isClosed() && collision.GetComponent<Rigidbody2D>().mass > massTriggerAmount)
        {
            Debug.Log("Opening door");
            doorControllScript.Open();
        }

        else if (doorControllScript.isOpen() && collision.GetComponent<Rigidbody2D>().mass > massTriggerAmount)
        {
            Debug.Log("Closing door");
            doorControllScript.Close();
        }
    }

    void Update()
    {
        Debug.DrawLine(transform.position, door.transform.position, Color.green);
    }
}
