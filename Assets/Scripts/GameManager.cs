using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; }} // Accessor

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
    public RestartScreen restartScreen;
    public GameOverScreen gameOverScreen;

    [SerializeField]
    private GameObject spawnPoint;

    [HideInInspector]
    public string teleporterTag;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GM START");
        spawnPoint = GameObject.Find("RespawnPoint");
        SceneManager.sceneLoaded += OnSceneLoaded;

        PlayerManager.Instance.FetchComponents();
        PlayerManager.Instance.SpawnPlayer(spawnPoint.transform.position);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        spawnPoint = GameObject.Find("RespawnPoint");
        PlayerManager.Instance.FetchComponents();

        if (teleporterTag != null)
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
        PlayerManager.Instance.SpawnPlayer(spawnPoint.transform.position);
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
        PlayerManager.Instance.SpawnPlayer(spawnPoint.transform.position);
        PlayerManager.Instance.PlayerRevive();
    }

    public void RestartGame()
    {
        gameOverScreen.Show(false);
        SceneManager.LoadScene("Scene1");
        PlayerManager.Instance.ResetPlayer();
    }
}
