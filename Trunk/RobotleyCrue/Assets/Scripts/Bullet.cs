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
        if (collider.gameObject.tag.Equals("M16"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().m16 = true;

            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }
        else if (collider.gameObject.tag.Equals("Shotgun"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().shotgun = true;

            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }
        else if (collider.gameObject.tag.Equals("Continue"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().Continue = true;

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
    }
}
