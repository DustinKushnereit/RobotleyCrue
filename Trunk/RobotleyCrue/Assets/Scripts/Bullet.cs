using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public GameObject particleSystemExplosion;

    float timeRemaining = 4.0f;

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0.0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        /*if (collider.gameObject.tag.Equals("M16") && this.tag.Equals("PlayerBullet"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().m16 = true;
            player.GetComponent<Player>().shotgun = false;

            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }

        if (collider.gameObject.tag.Equals("Shotgun") && this.tag.Equals("PlayerBullet"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().shotgun = true;
            player.GetComponent<Player>().m16 = false;

            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }

        if (collider.gameObject.tag.Equals("M16") && this.tag.Equals("Player2Bullet"))
        {
            GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
            player2.GetComponent<Player>().m16 = true;
            player2.GetComponent<Player>().shotgun = false;

            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }

        if (collider.gameObject.tag.Equals("Shotgun") && this.tag.Equals("Player2Bullet"))
        {
            GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
            player2.GetComponent<Player>().shotgun = true;
            player2.GetComponent<Player>().m16 = false;

            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }*/

        if (collider.gameObject.tag.Equals("Continue"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().Continue = true;

            GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
            player2.GetComponent<Player>().Continue = true;

            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }

        if (collider.gameObject.tag.Equals("Enemy"))
        {
            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }
        else if (collider.gameObject.tag != ("PlayerWeapon"))
        {
            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }

        if (collider.gameObject.tag.Equals("EnemyBullet"))
        {
            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.tag.Equals("HealthPickUp"))
        {
            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
            Destroy(collider.gameObject);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().TakeDamage(-2);

            GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
            player2.GetComponent<Player>().TakeDamage(-2);
        }
    }
}
