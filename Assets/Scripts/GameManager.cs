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
    public RestartScreen restartScreen;
    public GameOverScreen gameOverScreen;

    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = player.GetComponent<PlayerManager>();
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        playerManager.PlayerRevive();
        player.transform.position = spawnPoint.transform.position;
    }

    public void ShowRestartScreen()
    {
        restartScreen.Show(true);
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.Show(true);
    }

    public void RetryLevel()
    {
        restartScreen.Show(false);
        RespawnPlayer();
    }
}
