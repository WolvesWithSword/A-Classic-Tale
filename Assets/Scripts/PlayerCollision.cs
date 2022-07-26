using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	PlayerMotor playerMotor;

	private void Start()
	{
		playerMotor = this.gameObject.GetComponent<PlayerMotor>();
	}

	private void OnTriggerEnter2D(Collider2D collided)
	{
		if (collided.tag == "Ennemy")
		{
			playerMotor.StopMoving();
			GameManager.Instance.PlayerDie();
		}
	}
}
