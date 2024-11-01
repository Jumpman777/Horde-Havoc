using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    public GameObject defenderType1Prefab;
    public GameObject defenderType2Prefab;
    public GameObject defenderType3Prefab;

    private GameObject selectedDefenderPrefab;

    private int defender1Count = 0;
    private int defender2Count = 0;
    private int defender3Count = 0;
    private const int maxDefenders = 3; // Max number of defenders per type

    void Update()
    {
        // On mouse click, spawn the selected defender
        if (Input.GetMouseButtonDown(0) && selectedDefenderPrefab != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Terrain"))
                {
                    SpawnDefender(hit.point);
                }
            }
        }
    }

    // Select defender type 1
    public void SelectDefenderType1()
    {
        if (defender1Count < maxDefenders)
        {
            selectedDefenderPrefab = defenderType1Prefab;
            Debug.Log("Defender Type 1 Selected");
        }
        else
        {
            Debug.Log("Max number of Defender Type 1 reached");
        }
    }

    // Select defender type 2
    public void SelectDefenderType2()
    {
        if (defender2Count < maxDefenders)
        {
            selectedDefenderPrefab = defenderType2Prefab;
            Debug.Log("Defender Type 2 Selected");
        }
        else
        {
            Debug.Log("Max number of Defender Type 2 reached");
        }
    }

    // Select defender type 3
    public void SelectDefenderType3()
    {
        if (defender3Count < maxDefenders)
        {
            selectedDefenderPrefab = defenderType3Prefab;
            Debug.Log("Defender Type 3 Selected");
        }
        else
        {
            Debug.Log("Max number of Defender Type 3 reached");
        }
    }

    // Spawn the selected defender at the clicked location
    private void SpawnDefender(Vector3 spawnPosition)
    {
        if (selectedDefenderPrefab != null)
        {
            GameObject spawnedDefender = Instantiate(selectedDefenderPrefab, spawnPosition, Quaternion.identity);

            // Check which defender is being spawned and increment the correct count
            if (selectedDefenderPrefab == defenderType1Prefab)
            {
                defender1Count++;
                Debug.Log("Defender Type 1 Spawned");
            }
            else if (selectedDefenderPrefab == defenderType2Prefab)
            {
                defender2Count++;
                AssignMovingDefenderTarget(spawnedDefender);
                Debug.Log("Defender Type 2 Spawned");
            }
            else if (selectedDefenderPrefab == defenderType3Prefab)
            {
                defender3Count++;
                AssignMovingDefenderTarget(spawnedDefender);
                Debug.Log("Defender Type 3 Spawned");
            }

            selectedDefenderPrefab = null; // Clear selection after spawning
        }
    }

    internal void OnDefenderDestroyed()
    {
        throw new NotImplementedException();
    }

    // Find the closest enemy for the moving defenders to target
    private Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                closestEnemy = enemy.transform;
                minDistance = distance;
            }
        }

        return closestEnemy;
    }

    // Assign the closest enemy as the target for the moving defender
    private void AssignMovingDefenderTarget(GameObject spawnedDefender)
    {
        MovingDefender movingDefender = spawnedDefender.GetComponent<MovingDefender>();
        if (movingDefender != null)
        {
            movingDefender.SetTarget(FindClosestEnemy());
        }
    }
}














