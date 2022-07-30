using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFlags : MonoBehaviour
{
    public Transform[] flags;

    private void Start()
    {
        Transform[] allTransform = gameObject.GetComponentsInChildren<Transform>();
        flags = new Transform[allTransform.Length - 1];
        Array.Copy(allTransform, 1, flags, 0,  allTransform.Length - 1);// Skip one because it's itself
    }
}
