using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; }} // Accessor

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);// To delete previous instance if exist

        instance = this;
    }

    public GameObject player;
    public GameObject spawnPoint;

    private HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        healthManager = GetComponentInParent<HealthManager>();
        RespawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        player.transform.position = spawnPoint.transform.position;
    }

    public void PlayerDie()
    {
        healthManager.health -= 1;
        RespawnPlayer();
    }
}
