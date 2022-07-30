using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInst : MonoBehaviour // Use for parent gameobject with singleton childs
{
    private static CanvasInst instance;
    public static CanvasInst Instance { get { return instance; } } // Accessor

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
