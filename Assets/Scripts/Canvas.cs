using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour // Use for parent gameobject with singleton childs
{
    private static Canvas instance;
    public static Canvas Instance { get { return instance; } } // Accessor

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);// To delete previous instance if exist
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
