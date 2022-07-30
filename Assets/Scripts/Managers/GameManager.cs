using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager instance;
    public static GameManager Instance { get { return instance; }} // Accessor

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);// To delete previous instance if exist
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    #endregion

    public RestartScreen restartScreen;
    public GameOverScreen gameOverScreen;

    private GameObject spawnPoint;

    [HideInInspector]
    public string teleporterTag;//To do link between teleporter and map

    void Start()
    {
        spawnPoint = GameObject.Find("RespawnPoint");
        SceneManager.sceneLoaded += OnSceneLoaded;

        PlayerManager.Instance.FetchComponents();// Need to update PlayerManager
        PlayerManager.Instance.SpawnPlayer(spawnPoint.transform.position);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        spawnPoint = GameObject.Find("RespawnPoint");
        PlayerManager.Instance.FetchComponents();// Need to update PlayerManager

        if (teleporterTag != null) // If player take teleporter then teleport
        {
            var teleporters = FindObjectsOfType<Teleporter>();
            foreach (var teleporter in teleporters)
            {
                if (teleporter.teleporterTag == teleporterTag)
                {
                    teleporterTag = null;
                    PlayerManager.Instance.SpawnPlayer(teleporter.spawnPoint.transform.position, teleporter.enterPosition);
                    return;
                }
            }
        }
        // Restart of the game, teleport to spawn point
        PlayerManager.Instance.SpawnPlayer(spawnPoint.transform.position);
    }

    public void ShowRestartScreen()
    {
        restartScreen.Show(true);
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.Show(true);
        AudioManager.Instance.PlayGameOverSong();
    }

    public void RetryLevel()
    {
        restartScreen.Show(false);
        PlayerManager.Instance.SpawnPlayer(spawnPoint.transform.position);// Teleport to spawn point
        PlayerManager.Instance.PlayerRevive();

        foreach (RunForwardMotor motor in FindObjectsOfType<RunForwardMotor>()) // Need to reset moved ennemy
        {
            motor.ResetPosition();
        }
    }

    public void RestartGame()
    {
        gameOverScreen.Show(false);
        SceneManager.LoadScene("Forest - Scene1");// Back to last check point
        AudioManager.Instance.PlayAmbiantSong();
        PlayerManager.Instance.ResetPlayer();
    }
}