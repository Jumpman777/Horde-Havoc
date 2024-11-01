using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDefender : MonoBehaviour
{
    public float speed = 3f; // Speed of the moving defender
    private Transform targetEnemy; // Reference to the enemy
    public float detectionRange = 10f; // Range to detect enemies

    void Update()
    {
        // If an enemy target exists, move toward it
        if (targetEnemy != null)
        {
            MoveTowardsEnemy();
        }
        else
        {
            // Find the nearest enemy if there is no target
            FindNearestEnemy();
        }
    }

    // Method to find the nearest enemy
    private void FindNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        float closestDistance = Mathf.Infinity;

        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy")) // Check if the collider is an enemy
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetEnemy = collider.transform; // Set the nearest enemy as the target
                }
            }
        }
    }

    // Method to move the defender towards the enemy
    private void MoveTowardsEnemy()
    {
        if (targetEnemy != null) // Ensure there is a target
        {
            // Move the defender toward the target enemy
            Vector3 direction = (targetEnemy.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

            // Update the defender's position
            transform.position = newPosition;
        }
    }

    // Set the enemy target for the moving defender
    public void SetTarget(Transform enemy)
    {
        targetEnemy = enemy; // Assign the new target enemy
    }
}





