using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonDefender : MonoBehaviour
{
    public GameObject cannonballPrefab;     // Prefab of the cannonball
    public Transform firePoint;             // The point from where the cannonball is fired
    public float attackRange = 10f;         // Range within which the cannon can fire
    public float attackRate = 1f;           // Time between shots
    public float cannonballSpeed = 15f;     // Speed of the cannonball

    private Transform targetEnemy;          // Reference to the current enemy target
    private float nextAttackTime = 0f;

    void Update()
    {
        // If an enemy target exists and is in range, fire at it
        if (targetEnemy != null)
        {
            float distance = Vector3.Distance(transform.position, targetEnemy.position);
            if (distance <= attackRange)
            {
                // Check if it's time to fire
                if (Time.time >= nextAttackTime)
                {
                    nextAttackTime = Time.time + 1f / attackRate;
                    FireCannon();
                }
            }
            else
            {
                targetEnemy = null;  // Lose target if out of range
            }
        }

        // Find the nearest enemy if no target
        if (targetEnemy == null)
        {
            FindNearestEnemy();
        }
    }

    // Method to find the nearest enemy within attack range
    private void FindNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        float closestDistance = Mathf.Infinity;

        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetEnemy = collider.transform;
                }
            }
        }
    }

    // Fire the cannonball towards the enemy
    private void FireCannon()
    {
        // Instantiate a cannonball at the firePoint
        GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component to apply force
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        if (rb != null && targetEnemy != null)
        {
            // Calculate the direction towards the enemy and apply force
            Vector3 direction = (targetEnemy.position - firePoint.position).normalized;
            rb.velocity = direction * cannonballSpeed;
        }
    }
}

