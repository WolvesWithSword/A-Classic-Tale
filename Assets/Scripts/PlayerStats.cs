using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int health;
    [HideInInspector]
    public HealthUI healthUI;

    private void Start()
    {
        healthUI = FindObjectOfType<HealthUI>();
        healthUI.UpdateHealthUI(health);
    }

    public void TakeDamage()
    {
        health -= 1;
        healthUI.UpdateHealthUI(health);
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void setHealth(int health)
    {
        this.health = health;
        healthUI.UpdateHealthUI(health);
    }
}
