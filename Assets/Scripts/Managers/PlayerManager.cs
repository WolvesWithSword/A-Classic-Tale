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
	private PlayerAppearance playerAppearance;
	[HideInInspector]
	public PlayerStats playerStats;
	private bool canInteract = true;

	public bool invicible = false;
	public float invicibleTime = 3f;

	private void Start()
	{
		playerStats.ResetHealth();
	}

	public void FetchComponents()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerAppearance = player.GetComponent<PlayerAppearance>();
		playerMotor = player.GetComponent<PlayerMotor>();
		playerStats.healthUI = FindObjectOfType<HealthUI>();
	}

	public void PlayerDie()
	{
		if (!canInteract || invicible) return;// Invicibility when die

		CameraShake.Instance.StartShake(0.6f, 0.08f, 70);
		if (GameManager.Instance.isInBossFight)
		{
			StartCoroutine(TakeDamageInBossFight());
		}
		else
		{
			playerMotor.Die();
			canInteract = false;
		}
		playerStats.TakeDamage();
		PrintScreen();
	}

	private void PrintScreen()
	{
		if (playerStats.IsDead())
		{
			GameManager.Instance.ShowGameOverScreen();
		}
		else if (!GameManager.Instance.isInBossFight)
		{
			GameManager.Instance.ShowRestartScreen();
		}
	}

	private IEnumerator TakeDamageInBossFight()
	{
		canInteract = false;
		playerAppearance.MakePlayerFlash();
		yield return new WaitForSeconds(invicibleTime);
		canInteract = true;
		playerAppearance.StopPlayerFlash();
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

	public void PushPlayerTo(Vector3 position, float speed, EPlayerPosition playerPos = EPlayerPosition.NONE)
	{
		playerMotor.PushPlayerTo(position, speed, playerPos);
	}

	public void InteractWith(GameObject interacted)
	{
		if(interacted.GetComponent<IInteractable>() != null)
		{
			interacted.GetComponent<IInteractable>().Interact();
		}
	}

	public void SetPlayerPosition(EPlayerPosition playerPos)
	{
		playerMotor.SetPlayerPosition(playerPos);
	}

	public void BlockPlayerMovementFor(float blockedTime)
	{
		playerMotor.BlockMovementFor(blockedTime);
	}

	public void BlockPlayerMovement(bool blockMovement)
	{
		playerMotor.BlockMovement(blockMovement);
	}

}
