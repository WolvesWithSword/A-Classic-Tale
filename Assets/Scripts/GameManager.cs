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
    private GameObject player;
    [SerializeField]
    private GameObject spawnPoint;
    private PlayerManager playerManager;

    [HideInInspector]
    public string teleporterTag;

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();
        SceneManager.sceneLoaded += OnSceneLoaded;
        RespawnPlayer();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetComponents();

        if(teleporterTag != null)
        {
            var teleporters = FindObjectsOfType<Teleporter>();
            foreach (var teleporter in teleporters)
            {
                if (teleporter.teleporterTag == teleporterTag)
                {
                    spawnPoint = teleporter.spawnPoint;
                    teleporterTag = null;
                    RespawnPlayer();
                }
            }
        }
        RespawnPlayer();
    }

    private void GetComponents()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoint = GameObject.Find("RespawnPoint");
        playerManager = player.GetComponent<PlayerManager>();
    }

    public void RespawnPlayer()
    {
        player.transform.position = spawnPoint.transform.position;
        playerManager.PlayerRevive();
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

    public void RestartGame()
    {
        gameOverScreen.Show(false);
        SceneManager.LoadScene("Scene1");
    }
}
