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

    private List<float> xPositions;
    private List<float> yPositions;

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

    GameObject enemyLists;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyLists = GameObject.FindGameObjectWithTag("EnemyLists");

        spawnPosition = new Vector3(4.0f, spawnPosition.y, player.transform.position.z + 30);

        totalEnemies = 0;
        enemiesKilled = 0;
        Invoke("StartWave", waveSpawnDelay);

        //Set the xpositions
        //xPositions = new List<float>();
        //xPositions.Add(4.0f);
        //xPositions.Add(0.5f);
        //xPositions.Add(-3.0f);
        //xPositions.Add(-6.5f);
        //xPositions.Add(-10.0f);

        xPositions = new List<float>();
        xPositions.Add(1.25f);
        xPositions.Add(-2.25f);
        xPositions.Add(-5.75f);
        xPositions.Add(-9.25f);

        //Set the ypositions
        yPositions = new List<float>();
        yPositions.Add(14.5f);
        yPositions.Add(11.0f);
        yPositions.Add(7.5f);
        yPositions.Add(4.0f);
    }

    void Update()
    {
        //spawnPosition = new Vector3(Random.insideUnitCircle.x, spawnPosition.y, Random.insideUnitCircle.y).normalized * spawnRadius;

        if (player.transform.position.z <= 95)
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

        //if (finalBoss && spawnOnce)
        //{
        //    if (explosion == null)
        //    {
        //        Instantiate(bossTypes[0], spawnPosition, bossTypes[0].transform.rotation);
        //        spawnOnce = false;
        //    }
        //}

        //if (littleBossSpawn)
        //{
        //    explosion = (GameObject)Instantiate(bossSpawnIndicator, spawnPosition, bossSpawnIndicator.transform.rotation);
        //    Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);

        //    littleBossSpawn = false;
        //    littleBossSpawnOnce = true;
        //}

        //if (littleBossSpawnOnce)
        //{
        //    if (explosion == null)
        //    {
        //        Instantiate(bossTypes[0], spawnPosition, bossTypes[0].transform.rotation);
        //        littleBossSpawnOnce = false;
        //    }
        //}
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
            //bossWave = true;
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
            //spawnPosition = new Vector3(Random.insideUnitCircle.x, spawnPosition.y, Random.insideUnitCircle.y).normalized * spawnRadius;
            int rand = Random.Range(0, xPositions.Count);
            float zPos = player.transform.position.z + 30;

            if (incrementer % 4 == 0)
            {
                spawnPosition = new Vector3(xPositions[rand], yPositions[0], zPos);

                instantiateEnemy(0, spawnPosition);
            }
            else if (incrementer % 4 == 1)
            {
                spawnPosition = new Vector3(xPositions[rand], yPositions[1], zPos);

                instantiateEnemy(1, spawnPosition);
            }
            else if (incrementer % 4 == 2)
            {
                spawnPosition = new Vector3(xPositions[rand], yPositions[2], zPos);

                instantiateEnemy(2, spawnPosition);
            }
            else if (incrementer % 4 == 3)
            {
                spawnPosition = new Vector3(xPositions[rand], yPositions[3], zPos);

                instantiateEnemy(3, spawnPosition);
            }

            Invoke("SpawnUnit", enemySpawnDelay);
            incrementer++;
            totalEnemies++;
        }
    }

    void instantiateEnemy(int index, Vector3 pos)
    {
        GameObject go = Instantiate(enemyTypes[index], pos, enemyTypes[index].transform.rotation) as GameObject;

        if(index == 0)
        {
            enemyLists.GetComponent<EnemyLists>().m_GreenEnemies.Add(go);
        }
        else if (index == 1)
        {
            enemyLists.GetComponent<EnemyLists>().m_RedEnemies.Add(go);
        }
        else if (index == 2)
        {
            enemyLists.GetComponent<EnemyLists>().m_YellowEnemies.Add(go);
        }
        else if (index == 3)
        {
            enemyLists.GetComponent<EnemyLists>().m_BlueEnemies.Add(go);
        }
    }



    bool CheckCount()
    {
        if (GameObject.FindGameObjectsWithTag("GreenEnemy").Length <= 0 && GameObject.FindGameObjectsWithTag("RedEnemy").Length <= 0 &&
            GameObject.FindGameObjectsWithTag("YellowEnemy").Length <= 0 && GameObject.FindGameObjectsWithTag("BlueEnemy").Length <= 0 && 
            GameObject.FindGameObjectsWithTag("Boss").Length <= 0)
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
