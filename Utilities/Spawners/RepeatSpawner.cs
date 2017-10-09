using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnTime;
    GameObject obj;

    // Update is called once per frame
    void FixedUpdate()
    {
        Invoke("SpawnObject", 1f);
    }

    void SpawnObject()
    {
        Instantiate(prefab, this.gameObject.transform);
    }
}
