using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public HealthUIManager healthUIManager;

    private void Start()
    {
        healthUIManager = GameObject.FindObjectOfType<HealthUIManager>();
        healthUIManager.UpdateHealthUI(health);
    }

    public void TakeDamage()
    {
        health -= 1;
        healthUIManager.UpdateHealthUI(health);
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
