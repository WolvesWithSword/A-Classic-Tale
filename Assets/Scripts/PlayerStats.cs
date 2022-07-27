using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public HealthUIManager healthUIManager;

    private void Start()
    {
        healthUIManager.UpdateHealth(health);
    }

    public void TakeDamage()
    {
        health -= 1;
        healthUIManager.UpdateHealth(health);
    }
}
