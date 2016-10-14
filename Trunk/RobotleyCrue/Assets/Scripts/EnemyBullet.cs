using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    public GameObject particleSystemExplosion;

    float timeRemaining = 8.0f;

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
        if (collider.gameObject.tag.Equals("Player"))
        {
            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }
        else if (collider.gameObject.tag != ("Enemy"))
        {
            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }
    }
}
