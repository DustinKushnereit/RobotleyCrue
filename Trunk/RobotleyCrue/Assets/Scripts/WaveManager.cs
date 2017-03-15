using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    int currWave;
    int enemyCount;
    bool bossWave = false;
    bool waveEnd = false;
    bool waveStart;
    public float enemySpawnDelay;
    public float waveSpawnDelay;
    public int waveLength;
    public int timeBetweenRounds;
    float totalEnemies;
    int enemiesKilled;
    bool finalBoss = false;
    bool finalBossActivated = false;
    bool startTimer = false;
    bool startNextWaveTimer = false;

    public List<GameObject> enemyTypes;
    public List<GameObject> bossTypes;
    bool spawnOnce = true;

    public float spawnRadius = 5.0f;

    private int incrementer = 0;

    GameObject player;

    Vector3 spawnPosition;

    public GameObject bossSpawnIndicator;

    GameObject explosion;

    bool littleBossSpawn = false;
    bool littleBossSpawnOnce = false;

    void Start()
    {
        spawnPosition = new Vector3(Random.insideUnitCircle.x, spawnPosition.y, Random.insideUnitCircle.y).normalized * spawnRadius;

        totalEnemies = 0;
        enemiesKilled = 0;
        Invoke("StartWave", waveSpawnDelay);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        spawnPosition = new Vector3(Random.insideUnitCircle.x, spawnPosition.y, Random.insideUnitCircle.y).normalized * spawnRadius;

        if (player.transform.position.z <= 80)
        {
            checkBossSpawn();
        }
    }

    void checkBossSpawn()
    {
        if (!finalBoss)
        {
            if (!waveEnd)
            {
                if (incrementer >= waveLength)
                    StopSpawning();
            }
            else if (waveStart && CheckCount())
            {
                EndWave();
            }
        }

        if (!finalBossActivated)
        {
            waveLength = 0;
            waveEnd = true;
            finalBoss = true;
            finalBossActivated = true;

            spawnOnce = true;

            explosion = (GameObject)Instantiate(bossSpawnIndicator, spawnPosition, bossSpawnIndicator.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
        }

        if (finalBoss && spawnOnce)
        {
            if (explosion == null)
            {
                Instantiate(bossTypes[0], spawnPosition, bossTypes[0].transform.rotation);
                spawnOnce = false;
            }
        }

        if (littleBossSpawn)
        {
            explosion = (GameObject)Instantiate(bossSpawnIndicator, spawnPosition, bossSpawnIndicator.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);

            littleBossSpawn = false;
            littleBossSpawnOnce = true;
        }

        if (littleBossSpawnOnce)
        {
            if (explosion == null)
            {
                Instantiate(bossTypes[0], spawnPosition, bossTypes[0].transform.rotation);
                littleBossSpawnOnce = false;
            }
        }
    }

    /*public bool checkWinLoss()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        bool lossStatus = false;

        if (player.GetComponent<Player>().isActive && obj.gameObject.GetComponent<PlayerScript>().isAlive)
        {
            return false;
        }
        else
            lossStatus = true;

        Debug.Log(lossStatus);

        return lossStatus;
    }*/

    void StartWave()
    {
        enemiesKilled = 0;
        
        waveLength = ((currWave + 1) * 2) + 1;

        waveEnd = false;
        currWave++;
        totalEnemies = 0;
        incrementer = 0;
        waveStart = true;

        if (currWave % 5 == 0)
        {
            bossWave = true;
        }
        else
        {
            bossWave = false;
        }
        
        if (!bossWave)
        {
            SpawnUnit();
        }
        else
        {
            Invoke("SpawnBoss", 5);
            totalEnemies = 1;
        }
    }

    void SpawnBoss()
    {
        littleBossSpawn = true;
        incrementer = waveLength;
        totalEnemies++;
    }

    void SpawnUnit()
    {
        if (!waveEnd)
        {
            spawnPosition = new Vector3(Random.insideUnitCircle.x, spawnPosition.y, Random.insideUnitCircle.y).normalized * spawnRadius;

            if (incrementer % 3 == 0)
                Instantiate(enemyTypes[1], spawnPosition, enemyTypes[1].transform.rotation);
            else
                Instantiate(enemyTypes[0], spawnPosition, enemyTypes[0].transform.rotation);

            Invoke("SpawnUnit", enemySpawnDelay);
            incrementer++;
            totalEnemies++;
        }
    }

    bool CheckCount()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && GameObject.FindGameObjectsWithTag("Boss").Length <= 0)
        {
            startNextWaveTimer = true;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    void StopSpawning()
    {
        waveEnd = true;
    }

    void EndWave()
    {
        waveStart = false;
        waveEnd = false;
        Invoke("StartWave", timeBetweenRounds);
    }
    
    public void enemyKilled()
    {
        enemiesKilled++;
    }

}
