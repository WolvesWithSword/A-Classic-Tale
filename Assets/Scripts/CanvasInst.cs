using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasInst : MonoBehaviour // Use for parent gameobject with singleton childs
{
    #region SINGLETON
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
    #endregion
    private Canvas canvas;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        canvas = GetComponent<Canvas>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


}
