using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Ensure you have this if using Unity's UI system

public class Enemy : MonoBehaviour
{
    public float maxHealth = 50f;   // Maximum health
    private float currentHealth;    // Current health
    public Slider healthBar;        // Reference to the health bar UI slider

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Method to take damage and check for death
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Update the health bar to reflect the current health
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }

    // Destroy the enemy upon death
    private void Die()
    {
        Destroy(gameObject);
    }
}

