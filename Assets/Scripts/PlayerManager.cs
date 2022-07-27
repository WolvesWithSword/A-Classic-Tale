using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	PlayerMotor playerMotor;
	PlayerStats playerStats;

	private bool canInteract; 

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
		playerMotor.StopMoving();
		playerMotor.CanMove(false);
		playerStats.TakeDamage();
		GameManager.Instance.ShowRestartScreen();
		canInteract = false;
	}

	public void PlayerRevive()
    {
		canInteract = true;
		playerMotor.CanMove(true);
	}
}
