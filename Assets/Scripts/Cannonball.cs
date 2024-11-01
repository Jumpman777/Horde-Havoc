using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 5f; // Damage dealt by the bullet

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Deal damage to the enemy by calling TakeDamage on the Enemy script
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Destroy the bullet after hitting the enemy
            Destroy(gameObject);
        }
        else
        {
            // Destroy the bullet after a short time if it doesn't hit an enemy
            Destroy(gameObject, 2f);
        }
    }
}


