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
    private const float m_MAXTIMETILSHOOT = 6.0f;
    private float m_TimeTilShoot;

    public GameObject [] greenEnemies;
    private GameObject enemyLists;

    //Equalizer
    public GameObject equalizerParticles;
    float scaleHeight = 40.0f;
    List<GameObject> equalizerPieces;

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
        m_TimeTilShoot = 2.0f;

        greenEnemies = GameObject.FindGameObjectsWithTag("GreenEnemy");

        enemyLists = GameObject.FindGameObjectWithTag("EnemyLists");
    }

    void Update()
    {
        //moveToPlayer();

        timer = Mathf.Clamp(timer + Time.deltaTime, 0.0f, 1.0f / 5.0f);

        if (player.transform.position.z >= 105)
            Destroy(this.gameObject);

        if (m_CanShoot)
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
            gunCoolDown();

        equalizer();
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
        if(collider.tag == "PlayerBulletAOE")
        {
            TakeDamage(5);

            GameObject explosion = (GameObject)Instantiate(gotHitExplosion, transform.position, gotHitExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
        }
        if (collider.tag == "PlayerBulletSingle")
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
        if (collider.tag == "PlayerGrenade")
        {
            TakeDamage(100);

            GameObject explosion = (GameObject)Instantiate(gotHitExplosion, transform.position, gotHitExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
        }
        else if (collider.tag == "PlayerAbility")
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
            //float randomNum = Random.Range(1.0f, m_MAXTIMETILSHOOT);
            m_TimeTilShoot = 2.0f;
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

        if (m_health <= 0)
        {
            player.GetComponent<Player>().beatsPowerSlider.value = player.GetComponent<Player>().beatsPowerSlider.value + 1;

            GetComponent<ItemDrop>().calculateHealthDrop();

            Explode();
            deleteFromEnemyLists();
            Destroy(this.gameObject);
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
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

        equalizerPieces = new List<GameObject>(GameObject.FindGameObjectsWithTag("EqualizerPiece"));

        if (GameObject.FindGameObjectsWithTag("EqualizerPiece").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("EqualizerPiece").Length; i++)
            {
                Vector3 prevScale = equalizerPieces[i].transform.localScale;
                Vector3 enemyPos = equalizerPieces[i].transform.position;
                prevScale.z = Mathf.Lerp(prevScale.z, spectrum[i] * scaleHeight, Time.deltaTime * 10);
                equalizerPieces[i].transform.localScale = prevScale;
                equalizerPieces[i].transform.position = enemyPos;
            }
        }
    }
}
