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

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();
        SceneManager.sceneLoaded += OnSceneLoaded;
        playerManager = player.GetComponent<PlayerManager>();
        RespawnPlayer();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetComponents();
    }

    private void GetComponents()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoint = GameObject.Find("RespawnPoint");
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
        SceneManager.LoadScene("Scene1");
    }
}
