﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject prefab;
    GameObject obj;

    public void SpawnObject(bool ifChild)
    {
        obj = Instantiate(prefab);
        if (ifChild) obj.transform.parent = gameObject.transform;
    }

    public void DestroyGameObject()
    {
        Destroy(obj);
    }
}
