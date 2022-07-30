using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region SINGLETON
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
		DontDestroyOnLoad(gameObject);// Stay between scene

		playerStats = gameObject.GetComponent<PlayerStats>();// Get sibling component
	}
    #endregion

	private GameObject player;
	private PlayerMotor playerMotor;
	[HideInInspector]
	public PlayerStats playerStats;
	private bool canInteract = true;

	public bool invicible = false;

	private void Start()
	{
		playerStats.ResetHealth();
	}

	public void FetchComponents()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerMotor = player.GetComponent<PlayerMotor>();
		playerStats.healthUI = FindObjectOfType<HealthUI>();
	}

	public void PlayerDie()
	{
		if (!canInteract || invicible) return;// Invicibility when die

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

	public void SpawnPlayer(Vector3 spawnPos, EPlayerPosition playerPosition = EPlayerPosition.LIE)
	{
		player.transform.position = spawnPos;
		playerMotor.SetPlayerPosition(playerPosition);
	}

	public void ResetPlayer()
	{
		canInteract = true;
		playerStats.ResetHealth();
	}

	public void PlayerPickupHeart(int healthAmount)
    {
		playerStats.RestoreHealth(healthAmount);
    }
}
