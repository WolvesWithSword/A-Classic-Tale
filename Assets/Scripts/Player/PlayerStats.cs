using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private int maxHealth = 3;

    [HideInInspector]
    public HealthUI healthUI;

    [SerializeField]
    private bool hasAxe;
    public bool HasAxe
    {
        get { return hasAxe; }
        set { hasAxe = value; }
    }

    private void Start()
    {
        healthUI = FindObjectOfType<HealthUI>();
        healthUI.UpdateHealthUI(currentHealth);
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
        healthUI.UpdateHealthUI(currentHealth);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthUI.UpdateHealthUI(currentHealth);
    }

    public void RestoreHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthUI.UpdateHealthUI(currentHealth);
    }

    public bool IsFullLife()
    {
        return currentHealth == maxHealth;
    }
}
