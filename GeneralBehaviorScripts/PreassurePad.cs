using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreassurePad : MonoBehaviour
{
    // Which door to interact with
    public GameObject door;
    IDoor doorControllScript;

    //Public variables
    public float massTriggerAmount = 500f;

    private Rigidbody2D rigidbody2D;                         // Reference to the entity's rigidbody.

    void Start()
    {
        doorControllScript = door.GetComponent<IDoor>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void OnTriggerEnter2D(Collider col)
    {
        if (doorControllScript.isClosed() && rigidbody2D.mass > massTriggerAmount)
        {
            doorControllScript.Open();
        }
    }


    void OnTriggerExit2D(Collider col)
    {
        if (doorControllScript.isOpen() && rigidbody2D.mass > massTriggerAmount)
        {
            doorControllScript.Close();
        }
    }
}
