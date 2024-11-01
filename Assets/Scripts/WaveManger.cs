using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyWave
    {
        public GameObject enemyPrefab;
        public int enemyCount; // Number of enemies in this wave
        public float spawnRate; // How fast enemies spawn
        public float healthMultiplier; // Multiplier to enemy health for this wave
        public float damageMultiplier; // Multiplier to enemy damage for this wave
    }

    public List<EnemyWave> waves = new List<EnemyWave>(); // List of waves
    public Transform[] spawnPoints; // Spawn points for enemies
    public float timeBetweenWaves = 10f; // Time between waves

    private int currentWaveIndex = 0;
    private float nextWaveTime = 0f;
    private bool waveInProgress = false;
    private float elapsedTime = 0f; // Time tracker for difficulty scaling

    void Update()
    {
        // Check if it's time to spawn the next wave
        if (Time.time >= nextWaveTime && !waveInProgress)
        {
            if (currentWaveIndex < waves.Count)
            {
                StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                nextWaveTime = Time.time + timeBetweenWaves; // Time until the next wave
                currentWaveIndex++; // Move to the next wave
            }
        }

        // Adjust game difficulty based on elapsed time
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 60f) // For example, every 60 seconds the game becomes harder
        {
            timeBetweenWaves -= 0.5f; // Shorten the time between waves as difficulty increases
            elapsedTime = 0f; // Reset the elapsed time
        }
    }

    IEnumerator SpawnWave(EnemyWave wave)
    {
        waveInProgress = true;

        // Spawn each enemy in the wave
        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab, wave.healthMultiplier, wave.damageMultiplier);
            yield return new WaitForSeconds(1f / wave.spawnRate); // Control spawn rate
        }

        waveInProgress = false;
    }

    void SpawnEnemy(GameObject enemyPrefab, float healthMultiplier, float damageMultiplier)
    {
        // Pick a random spawn point
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Instantiate the enemy at the selected spawn point
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Adjust enemy attributes
        EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();
        enemyScript.damage *= damageMultiplier; // Increase enemy damage
    }
}

