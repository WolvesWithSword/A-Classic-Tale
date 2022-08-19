using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    #region SINGLETON
    private static GameEventManager instance;
    public static GameEventManager Instance { get { return instance; } } // Accessor

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);// To delete previous instance if exist
            return;
        }
        instance = this;
    }
    #endregion

    public bool hasDefeatedNecromanger = false;
    public bool hasSeenTutorial = false;
}
