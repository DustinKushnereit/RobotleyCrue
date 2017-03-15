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

    //Equalizer
    int numOfSamples = 512; //Min: 64, Max: 8192
    float[] freqData;
    float[] band;
    GameObject[] g;
    int totalK;
    public GameObject yellowSphere;
    public GameObject yellowBeam;
    public GameObject greenSphere;
    public GameObject greenBeam;
    public GameObject redSphere;
    public GameObject redBeam;
    public GameObject blueSphere;
    public GameObject blueBeam;
    public GameObject spawnEffect;

    //Overseer
    public bool canSpawnYellow = true;
    public bool canSpawnRed = true;
    public bool canSpawnBlue = true;
    public bool canSpawnGreen = true;

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
        yPositions.Add(12.0f);
        yPositions.Add(9.0f);
        yPositions.Add(6.0f);
        //yPositions.Add(4.5f);


        //Equalizer
        freqData = new float[numOfSamples];
        int n = freqData.Length;
        int k = 0;

        for (int j = 0; j < freqData.Length; j++)
        {
            n = n / 2;
            if (n <= 0)
                break;
            k++;
        }

        totalK = k;

        band = new float[k + 1];
        g = new GameObject[k + 1];
        
        for (int i = 0; i < band.Length; i++)
        {
            band[i] = 0;
            

            if (i == 0)
            {
                g[i] = GameObject.Instantiate(blueSphere);
            }
            if (i == 1)
            {
                g[i] = GameObject.Instantiate(greenSphere);
            }
            if (i == 2)
            {
                g[i] = GameObject.Instantiate(redSphere);
            }
            if (i == 3)
            {
                g[i] = GameObject.Instantiate(yellowSphere);
            }

            if(i == 4)
            {
                g[i] = GameObject.Instantiate(yellowSphere);
                g[i].SetActive(false);
                g[i].tag = "RedSphere";
            }

            if (i == 5)
            {
                g[i] = GameObject.Instantiate(blueBeam);
                g[i].tag = "RedMusicParticle";
            }
            if (i == 6)
            {
                g[i] = GameObject.Instantiate(greenBeam);
                g[i].tag = "RedMusicParticle";
            }
            if (i == 7)
            {
                g[i] = GameObject.Instantiate(redBeam);
                g[i].tag = "RedMusicParticle";
            }
            if (i == 8)
            {
                g[i] = GameObject.Instantiate(yellowBeam);
                g[i].tag = "RedMusicParticle";
            }

            if ( i > 8)
                g[i] = GameObject.Instantiate(yellowBeam);

            if (i < 5)
                g[i].transform.position = new Vector3(-8 + (i * 3f), 13, 0);
            else
                g[i].transform.position = new Vector3(-8 + ((i - 5) * 3f), 15, 0);
            //g[i].layer = 5;
            //g[i].tag = "Untagged";
            //g[i].AddComponent<ScrollingUVs>();
            //g[i].GetComponent<ScrollingUVs>().uvAnimationRate = new Vector2(0.5f, 0);

            if (i == 9)
                g[i].SetActive(false);
        }

        InvokeRepeating("check", 0.0f, 1.0f / 10.0f); // update at 15 fps
    }

    private void check()
    {
        AudioListener.GetSpectrumData(freqData, 0, FFTWindow.Rectangular);

        int k = 0;
        int crossover = 2;

        for (int i = 0; i < freqData.Length; i++)
        {
            float d = freqData[i];
            float b = band[k];

            if (d > b)
                band[k] = d;
            else
                band[k] = b;

            if (i > (crossover - 3))
            {
                crossover *= 2;

                if(g[k].tag == "Enemy" && g[k].activeSelf)
                    g[k].transform.position = new Vector3(g[k].transform.position.x, band[k] * 32 + 18, player.transform.position.z + 20);
                if(g[5].activeSelf && canSpawnBlue)
                    g[5].transform.position = new Vector3(g[5].transform.position.x, 16, player.transform.position.z + 20);
                if (g[6].activeSelf && canSpawnGreen)
                    g[6].transform.position = new Vector3(g[6].transform.position.x, 16, player.transform.position.z + 20);
                if (g[7].activeSelf && canSpawnRed)
                    g[7].transform.position = new Vector3(g[7].transform.position.x, 16, player.transform.position.z + 20);
                if (g[8].activeSelf && canSpawnYellow)
                    g[8].transform.position = new Vector3(g[8].transform.position.x, 16, player.transform.position.z + 20);

                band[k] = 0;
                k++;
            } 
        }
    }

    void Update()
    {
        //spawnPosition = new Vector3(Random.insideUnitCircle.x, spawnPosition.y, Random.insideUnitCircle.y).normalized * spawnRadius;

        if (player.transform.position.z <= 95)
        {
            checkBossSpawn();
        }

        if(!canSpawnBlue)
            g[5].transform.position = new Vector3(g[5].transform.position.x, g[5].transform.position.y, -1000);
        if (!canSpawnGreen)
            g[6].transform.position = new Vector3(g[6].transform.position.x, g[6].transform.position.y, -1000);
        if (!canSpawnRed)
            g[7].transform.position = new Vector3(g[7].transform.position.x, g[7].transform.position.y, -1000);
        if (!canSpawnYellow)
            g[8].transform.position = new Vector3(g[8].transform.position.x, g[8].transform.position.y, -1000);
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
    }

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
            int randY = Random.Range(0, yPositions.Count);
            int randx = Random.Range(0, xPositions.Count);
            float zPos = player.transform.position.z + 40;

            int spawnRange = Random.Range(0, 9);

            if (spawnRange % 4 == 0 && canSpawnGreen)//green
            {
                spawnPosition = new Vector3(xPositions[randx], yPositions[randY], zPos);

                GameObject spawnExplosion = (GameObject)Instantiate(spawnEffect, new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z - 6.0f), spawnEffect.transform.rotation);
                Destroy(spawnExplosion, spawnExplosion.GetComponent<ParticleSystem>().duration);

                instantiateEnemy(0, spawnPosition);
            }
            else if (spawnRange % 4 == 1 && canSpawnYellow)//yellow
            {
                spawnPosition = new Vector3(xPositions[randx], yPositions[randY], zPos);

                GameObject spawnExplosion = (GameObject)Instantiate(spawnEffect, new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z - 6.0f), spawnEffect.transform.rotation);
                Destroy(spawnExplosion, spawnExplosion.GetComponent<ParticleSystem>().duration);

                instantiateEnemy(1, spawnPosition);
            }
            else if (spawnRange % 4 == 2 && canSpawnRed)//red
            {
                spawnPosition = new Vector3(xPositions[randx], yPositions[randY], zPos);

                GameObject spawnExplosion = (GameObject)Instantiate(spawnEffect, new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z - 6.0f), spawnEffect.transform.rotation);
                Destroy(spawnExplosion, spawnExplosion.GetComponent<ParticleSystem>().duration);

                instantiateEnemy(2, spawnPosition);
            }
            else if (spawnRange % 4 == 3 && canSpawnBlue)//blue
            {
                spawnPosition = new Vector3(xPositions[randx], yPositions[randY], zPos);

                GameObject spawnExplosion = (GameObject)Instantiate(spawnEffect, new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z - 6.0f), spawnEffect.transform.rotation);
                Destroy(spawnExplosion, spawnExplosion.GetComponent<ParticleSystem>().duration);

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

        if (index == 0)
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
