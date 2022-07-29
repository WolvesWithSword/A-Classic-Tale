using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

	private static PlayerManager instance;
	public static PlayerManager Instance { get { return instance; } } // Accessor



	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);// To delete previous instance if exist
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);

		playerStats = gameObject.GetComponent<PlayerStats>();
	}

	public int startHealth = 3;

	private GameObject player;
	private PlayerMotor playerMotor;
	private PlayerStats playerStats;
	private bool canInteract = true;
	private static bool hasLoadedScene = false;

	public static bool IsReadyAsDependency()
	{
		return instance != null && hasLoadedScene;
	}

	/*private void Start()
	{
		Debug.Log("PM START");
		player = GameObject.FindGameObjectWithTag("Player");
		playerMotor = player.GetComponent<PlayerMotor>();
		playerStats.healthUI = FindObjectOfType<HealthUI>();

		playerStats.setHealth(startHealth);
		hasLoadedScene = true;
	}*/

	public void FetchComponents()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerMotor = player.GetComponent<PlayerMotor>();
		playerStats.healthUI = FindObjectOfType<HealthUI>();
	}

	public void PlayerDie()
	{
		if (!canInteract) return;

		playerMotor.Die();
		playerStats.TakeDamage();
		canInteract = false;

		if (playerStats.IsDead())
        {
			GameManager.Instance.ShowGameOverScreen();
		}
        else
        {
			GameManager.Instance.ShowRestartScreen();
		}
	}

	public void PlayerRevive()
    {
		canInteract = true;
		playerMotor.Revive();
	}

	public void SpawnPlayer(Vector3 spawnPos)
	{
		player.transform.position = spawnPos;
	}

	public void ResetPlayer()
	{
		canInteract = true;
		playerStats.setHealth(startHealth);
	}
}
