using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float m_health;
    //float m_damage;
    public float movementSpeed = 3.0f;
    public bool isRanged;

    public GameObject player;
    GameObject player2;
    public GameObject particleSystemExplosion;
    public GameObject gotHitExplosion;
    GameObject enemy;
    Vector3 moveDirection;

    float timer;

    //NavMeshAgent navComp;

    GameObject waveManager;

    //shooting 
    public GameObject bullet;
    private GameObject gun;
    private bool m_CanShoot;
    private const float m_MAXTIMETILSHOOT = 2.0f;
    private float m_TimeTilShoot;

    public GameObject [] greenEnemies;
    private GameObject enemyLists;

    //Equalizer
    public GameObject equalizerParticles;
    float scaleHeight = 40.0f;
    List<GameObject> equalizerPieces;

    //movement
    bool hasNotReachedPlayer = true;
    private const float MAX_TIME_TO_MOVE = 0.1f;
    private float moveTimer;

    float t = 0;
    float duration = 2;

    //New Enemy Energy Blast
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject bottom;
    public GameObject top;
    public GameObject enrgyWaveExplosion;
    public GameObject energyCollapseExplosion;
    bool explodeAndDamage = false;
    float timeToDamage = 1.0f;
    bool doOnce = true;

    //Overseer
    public bool isOverseer;
    bool startTimer = false;
    float overSeerRespawnTimer = 5.0f;
    float startingHealth;
    bool isDead = false;
    public bool amYellow;
    public bool amGreen;
    public bool amRed;
    public bool amBlue;

    //Spawn Effect
    Color originalColor;
    float spawnTimer;
    bool initialSpawn = false;
    Vector3 originalScale;

    void Start()
    {
        //m_health = 10;
        //m_damage = 10;
        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        gun = transform.FindChild("Gun").gameObject;

        //navComp = GetComponent<NavMeshAgent>();
        //navComp.speed = movementSpeed;

        m_CanShoot = false;

        if(this.tag != "GreenEnemy")
            m_TimeTilShoot = m_MAXTIMETILSHOOT;
        else
            m_TimeTilShoot = m_MAXTIMETILSHOOT + 1;

        greenEnemies = GameObject.FindGameObjectsWithTag("GreenEnemy");

        enemyLists = GameObject.FindGameObjectWithTag("EnemyLists");

        startingHealth = m_health;

        if (!isOverseer)
        {
            initialSpawn = true;
            originalScale = transform.localScale;
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            originalColor = GetComponent<Renderer>().material.GetColor("_Color");

            Color final = new Color(1, 0.2f, 0.8f, 1);
            GetComponent<Renderer>().material.SetColor("_Color", final);
        }
    }

    void Update()
    {
        //moveToPlayer();

        timer = Mathf.Clamp(timer + Time.deltaTime, 0.0f, 1.0f / 5.0f);

        if (player.transform.position.z >= 115)
            Destroy(this.gameObject);

        if(initialSpawn && !isOverseer)
        {
            spawnTimer += Time.deltaTime;
            
            Vector3 prevScale = transform.localScale;
            prevScale = Vector3.Lerp(prevScale, new Vector3(transform.localScale.x + spawnTimer, transform.localScale.y + spawnTimer, transform.localScale.z + spawnTimer), Time.deltaTime * 2.0f);
            transform.localScale = prevScale;

            if (spawnTimer >= 2.0f)
            {
                GetComponent<Renderer>().material.SetColor("_Color", originalColor);
                transform.localScale = originalScale;
                initialSpawn = false;
            }
        }

        /*if (m_CanShoot)
        {
            int rand = Random.Range(0, 101);

            if (rand <= 25)
                isRanged = true;
            else
                isRanged = false;

            if (isRanged)
                ShootGun();
            else
                m_CanShoot = false;
        }
        else
            gunCoolDown();*/

        if (!isOverseer)
        {
            if(explodeAndDamage)
            {
                timeToDamage += Time.deltaTime;

                Vector3 prevScale = transform.localScale;
                prevScale = Vector3.Lerp(prevScale, transform.localScale / timeToDamage, Time.deltaTime * 10.0f);
                transform.localScale = prevScale;
                Color final = new Color(1, 0.2f, 0.8f, 1);
                GetComponent<Renderer>().material.SetColor("_Color", final);

                if (timeToDamage >= 1.0f && doOnce)
                {
                    doOnce = false;
                    GameObject explosion = (GameObject)Instantiate(energyCollapseExplosion, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), energyCollapseExplosion.transform.rotation);
                    Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
                }

                if (timeToDamage >= 2.0f)
                {
                    //To fire the beam at the player
                    Vector3 relativePos = player.transform.position - new Vector3(gun.transform.position.x, gun.transform.position.y - 0.5f, gun.transform.position.z);
                    Quaternion newRotation = Quaternion.LookRotation(relativePos);
                    gun.transform.rotation = newRotation;

                    GameObject explosion = (GameObject)Instantiate(enrgyWaveExplosion, new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z), gun.transform.rotation);
                    Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
                }

                if (timeToDamage >= 2.5f)
                {
                    GameObject.FindGameObjectWithTag("CameraPlayer1").GetComponent<CameraFollowPlayer>().StartCoroutine("Shake");

                    GameObject explosionLeft = (GameObject)Instantiate(leftSide, new Vector3(player.transform.position.x - 2.5f, player.transform.position.y, player.transform.position.z + 2.0f), leftSide.transform.rotation);
                    Destroy(explosionLeft, explosionLeft.GetComponent<ParticleSystem>().duration);

                    GameObject explosionRight = (GameObject)Instantiate(rightSide, new Vector3(player.transform.position.x + 2.5f, player.transform.position.y, player.transform.position.z + 2.0f), rightSide.transform.rotation);
                    Destroy(explosionRight, explosionRight.GetComponent<ParticleSystem>().duration);

                    GameObject explosionBottom = (GameObject)Instantiate(bottom, new Vector3(player.transform.position.x, player.transform.position.y - 1.0f, player.transform.position.z + 2.0f), bottom.transform.rotation);
                    Destroy(explosionBottom, explosionBottom.GetComponent<ParticleSystem>().duration);

                    GameObject explosionTop = (GameObject)Instantiate(top, new Vector3(player.transform.position.x, player.transform.position.y + 2.0f, player.transform.position.z + 2.0f), bottom.transform.rotation);
                    Destroy(explosionTop, explosionTop.GetComponent<ParticleSystem>().duration);

                    if (amGreen)
                    {
                        player.GetComponent<Player>().TakeDamage(0.06f * player.GetComponent<Player>().MAXHEALTH);
                        player2.GetComponent<Player>().TakeDamage(0.06f * player.GetComponent<Player>().MAXHEALTH);
                    }

                    if (amRed || amYellow)
                    {
                        player.GetComponent<Player>().TakeDamage(0.1f * player.GetComponent<Player>().MAXHEALTH);
                        player2.GetComponent<Player>().TakeDamage(0.1f * player.GetComponent<Player>().MAXHEALTH);
                    }

                    if (amBlue)
                    {
                        player.GetComponent<Player>().TakeDamage(0.14f * player.GetComponent<Player>().MAXHEALTH);
                        player2.GetComponent<Player>().TakeDamage(0.14f * player.GetComponent<Player>().MAXHEALTH);
                    }

                    Destroy(this.gameObject);
                }
            }

            if(!explodeAndDamage)
                movement();

            //if (transform.position.y < 5)
                //transform.position = new Vector3(transform.position.x, 5, transform.position.z);

            //equalizer();

            //lerpMe();
        }

        if(startTimer)
        {
            overSeerRespawnTimer -= Time.deltaTime;

            if(overSeerRespawnTimer <= 2)
            {
                Vector3 relativePos = player.transform.position - gun.transform.position;
                Quaternion newRotation = Quaternion.LookRotation(relativePos);
                gun.transform.localRotation = newRotation;

                GameObject explosion = (GameObject)Instantiate(enrgyWaveExplosion, gun.transform.position, gun.transform.localRotation);
                Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);

                GameObject explosion2 = (GameObject)Instantiate(enrgyWaveExplosion, transform.position, enrgyWaveExplosion.transform.localRotation);
                Destroy(explosion2, explosion2.GetComponent<ParticleSystem>().duration);
            }

            if(overSeerRespawnTimer <= 0)
            {
                startTimer = false;
                overSeerRespawnTimer = 5.0f;
                isDead = false;
                GetComponent<MeshRenderer>().enabled = true;
                //this.gameObject.SetActive(true);

                if (amYellow)
                {
                    waveManager.GetComponent<WaveManager>().canSpawnYellow = true;
                }

                if (amGreen)
                {
                    waveManager.GetComponent<WaveManager>().canSpawnGreen = true;
                }

                if (amRed)
                {
                    waveManager.GetComponent<WaveManager>().canSpawnRed = true;
                }

                if (amBlue)
                {
                    waveManager.GetComponent<WaveManager>().canSpawnBlue = true;
                }
            }
        }
    }

    void movement()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.04f);

        moveTimer += Time.deltaTime;

        checkDistance();

        if (hasNotReachedPlayer)
        {
            if (moveTimer >= MAX_TIME_TO_MOVE)
            {
                moveTimer = 0.0f;

                if (transform.position.y > 8.5f)
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            }
        }
        else
        {
            explodeAtPlayer();
        }
    }

    void checkDistance()
    {
        if (transform.position.z <= player.transform.position.z + 7.0f)
        {
            hasNotReachedPlayer = false;
        }
    }

    void explodeAtPlayer()
    {
        explodeAndDamage = true;
    }

    void lerpMe()
    {
        //t += Time.deltaTime / duration;
        //Vector3 prevPos = transform.position;
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(6.0f, 7.0f, Mathf.PingPong(Time.time, 1)), transform.position.z);
        //transform.position = prevPos;
    }

    void moveToPlayer()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 2.0f);
        //navComp.SetDestination(playerPos);

        float distance = Vector3.Distance(transform.position, playerPos);

        if (distance <= 2.0f && !player.GetComponent<Player>().m_Invincible)
        {
            player.GetComponent<Player>().TakeDamage(1);
            player2.GetComponent<Player>().TakeDamage(1);
        }

        int randomNum = Random.Range(0, 3);

        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (randomNum == 0)
            direction = (player.transform.position - transform.position).normalized;

        if(randomNum == 1)
            direction = (player2.transform.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2.0f);

        if(transform.position.z <= player.transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, waveManager.transform.position.z);
        }
    }

    public GameObject getClosestGreenEnemy()
    {
        greenEnemies = GameObject.FindGameObjectsWithTag("GreenEnemy");

        if (greenEnemies.Length > 1)
        {
            float currentDistance = Vector3.Distance(greenEnemies[1].transform.position, transform.position);
            GameObject closestTarget = greenEnemies[1];

            //Loop through each player except the first one
            for (int i = 1; i < greenEnemies.Length; i++)
            {
                float tempDistance = Vector3.Distance(greenEnemies[i].transform.position, transform.position);
                if (tempDistance < currentDistance && greenEnemies[i] != this.gameObject)
                {
                    currentDistance = tempDistance;
                    closestTarget = greenEnemies[i];
                }
            }
            return closestTarget;
        }
        return this.gameObject;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "PlayerBulletAOE" && !isDead)
        {
            TakeDamage(5);

            GameObject explosion = (GameObject)Instantiate(gotHitExplosion, transform.position, gotHitExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
        }
        if (collider.tag == "PlayerBulletSingle" && !isDead)
        {
            TakeDamage(10);

            GameObject explosion = (GameObject)Instantiate(gotHitExplosion, transform.position, gotHitExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
        }
        /*if (collider.tag == "Player2Bullet")
        {
            TakeDamage(5);

            GameObject explosion = (GameObject)Instantiate(gotHitExplosion, transform.position, gotHitExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
        }*/
        if (collider.tag == "PlayerGrenade" && !isDead && !isOverseer)
        {
            TakeDamage(100);

            GameObject explosion = (GameObject)Instantiate(gotHitExplosion, transform.position, gotHitExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
        }
        else if (collider.tag == "PlayerAbility" && !isDead && !isOverseer)
        {
            TakeDamage(100);
            player.GetComponent<Player>().beatsPowerSlider.value = 0;
        }
    }

    public void gunCoolDown()
    {
        m_TimeTilShoot -= Time.deltaTime;

        if(m_TimeTilShoot <= 0.0f)
        {
            m_CanShoot = true;

            if (this.tag != "GreenEnemy")
                m_TimeTilShoot = m_MAXTIMETILSHOOT;
            else
                m_TimeTilShoot = m_MAXTIMETILSHOOT + 1;
        }
    }

    public void ShootGun()
    {
        Vector3 relativePos = player.transform.position - gun.transform.position;
        Vector3 relativePos2 = player2.transform.position - gun.transform.position;
        float distance = relativePos.magnitude;
        float distance2 = relativePos2.magnitude;

        Quaternion newRotation;
        if(distance < distance2)
            newRotation = Quaternion.LookRotation(relativePos);
        else
            newRotation = Quaternion.LookRotation(relativePos2);

        gun.transform.localRotation = newRotation;
        //print(gun.transform.rotation);

        GameObject bulletInstance = Instantiate(bullet, gun.transform.position, gun.transform.localRotation) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().AddForce(gun.transform.forward * 28.0f, ForceMode.VelocityChange);//28

        /*Rigidbody bulletClone;
        bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as Rigidbody;
        bulletClone.AddForce(bullet.transform.forward * 28.0f);*/

        m_CanShoot = false;
    }

    public void Explode()
    {
        GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
    }

    public void TakeDamage(float amount)
    {
        m_health -= amount;

        if (m_health <= 0 && !isOverseer)
        {
            player.GetComponent<Player>().beatsPowerSlider.value = player.GetComponent<Player>().beatsPowerSlider.value + 1;

            GetComponent<ItemDrop>().calculateHealthDrop();

            Explode();
            deleteFromEnemyLists();
            Destroy(this.gameObject);
        }
        else if(m_health <= 0 && isOverseer)
        {
            GetComponent<MeshRenderer>().enabled = false;
            //this.gameObject.SetActive(false);

            if (amYellow)
            {
                waveManager.GetComponent<WaveManager>().canSpawnYellow = false;
            }

            if (amGreen)
            {
                waveManager.GetComponent<WaveManager>().canSpawnGreen = false;
            }

            if (amRed)
            {
                waveManager.GetComponent<WaveManager>().canSpawnRed = false;
            }

            if (amBlue)
            {
                waveManager.GetComponent<WaveManager>().canSpawnBlue = false;
            }

            isDead = true;
            m_health = startingHealth;
            startTimer = true;

            GameObject explosion = (GameObject)Instantiate(enrgyWaveExplosion, transform.position, enrgyWaveExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
        }
    }


    private void deleteFromEnemyLists()
    {
        //if (transform.position.y >= enemyLists.GetComponent<EnemyLists>().m_yEndPositions[0])
        //{
            enemyLists.GetComponent<EnemyLists>().m_GreenEnemies.Remove(this.gameObject);
        //}
        //else if (transform.position.y >= enemyLists.GetComponent<EnemyLists>().m_yEndPositions[1])
        //{
            enemyLists.GetComponent<EnemyLists>().m_RedEnemies.Remove(this.gameObject);
        //}
        //else if (transform.position.y >= enemyLists.GetComponent<EnemyLists>().m_yEndPositions[2])
        //{
            enemyLists.GetComponent<EnemyLists>().m_YellowEnemies.Remove(this.gameObject);
        //}
        //else if (transform.position.y >= enemyLists.GetComponent<EnemyLists>().m_yEndPositions[3])
        //{
            enemyLists.GetComponent<EnemyLists>().m_BlueEnemies.Remove(this.gameObject);
        //}
    }

    /*public void equalizer()
    {
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

        GameObject explosion = (GameObject)Instantiate(equalizerParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.0f), equalizerParticles.transform.rotation);
        
        Vector3 prevScale = explosion.transform.localScale;
        prevScale.y = Mathf.Lerp(prevScale.y, spectrum[1] * scaleHeight, Time.deltaTime * 10);
        explosion.transform.localScale = prevScale;
        
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
    }*/

    public void equalizer()
    {
        float[] spectrum = new float[512]; ;//AudioListener.GetSpectrumData(1024, 0, FFTWindow.Rectangular);
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        equalizerPieces = new List<GameObject>(GameObject.FindGameObjectsWithTag("EqualizerPiece"));

        if (GameObject.FindGameObjectsWithTag("EqualizerPiece").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("EqualizerPiece").Length; i++)
            {
                Vector3 prevScale = equalizerPieces[i].transform.localScale;
                Vector3 enemyPos = equalizerPieces[i].transform.position;
                prevScale.z = Mathf.Lerp(prevScale.z, (spectrum[i] * scaleHeight) / 6, Time.deltaTime * 10);
                equalizerPieces[i].transform.localScale = prevScale;
                equalizerPieces[i].transform.position = enemyPos;
            }
        }
    }
}
