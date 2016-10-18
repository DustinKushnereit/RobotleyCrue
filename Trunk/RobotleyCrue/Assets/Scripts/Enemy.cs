﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    float m_health;
    float m_damage;
    public float movementSpeed = 3.0f;
    public bool isRanged;

    GameObject player;
    public GameObject particleSystemExplosion;
    public GameObject gotHitExplosion;
    GameObject enemy;
    Vector3 moveDirection;

    float timer;

    NavMeshAgent navComp;

    //shooting 
    public GameObject bullet;
    public GameObject gun;
    private bool m_CanShoot;
    private const float m_MAXTIMETILSHOOT = 6.0f;
    private float m_TimeTilShoot;

    void Start()
    {
        m_health = 10;
        m_damage = 10;
        player = GameObject.FindGameObjectWithTag("Player");

        navComp = GetComponent<NavMeshAgent>();
        navComp.speed = movementSpeed;

        m_CanShoot = false;
        m_TimeTilShoot = 6.0f;
    }

    void Update()
    {
        moveToPlayer();

        timer = Mathf.Clamp(timer + Time.deltaTime, 0.0f, 1.0f / 5.0f);

        if (m_CanShoot && isRanged)
            ShootGun();
        else
            gunCoolDown();
    }

    void moveToPlayer()
    {
        navComp.SetDestination(player.transform.position);

        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2.0f);

        if(transform.position.z <= player.transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + 20);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "PlayerBullet")
        {
            TakeDamage(5);

            GameObject explosion = (GameObject)Instantiate(gotHitExplosion, transform.position, gotHitExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
        }
        if (collider.tag == "PlayerGrenade")
        {
            TakeDamage(10);

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
            m_TimeTilShoot = 6.0f;
        }
    }

    public void ShootGun()
    {
        GameObject bulletInstance = Instantiate(bullet, gun.transform.position, gun.transform.rotation) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 28.0f, ForceMode.VelocityChange);//28

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

            Explode();
            Destroy(this.gameObject);
        }
    }
}
