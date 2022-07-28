using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	PlayerMotor playerMotor;
	PlayerStats playerStats;

	private bool canInteract = true; 

	private void Start()
	{
		playerMotor = this.gameObject.GetComponent<PlayerMotor>();
		playerStats = this.gameObject.GetComponent<PlayerStats>();
	}

	private void OnTriggerEnter2D(Collider2D collided)
	{
		if (collided.tag == "Ennemy" && canInteract)
		{
			PlayerDie();
		}
	}

	public void PlayerDie()
	{
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
}
