using UnityEngine;
using System.Collections;

public class FlashBangExplosion : MonoBehaviour
{
    public GameObject particleSystemExplosion;
    public GameObject particleSystemExplosion2;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Floor"))
        {
            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z), particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);

            GameObject explosion2 = (GameObject)Instantiate(particleSystemExplosion2, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), particleSystemExplosion2.transform.rotation);
            Destroy(explosion2, explosion2.GetComponent<ParticleSystem>().duration);
        }
        /*else
        {
            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);

            GameObject explosion2 = (GameObject)Instantiate(particleSystemExplosion2, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), particleSystemExplosion2.transform.rotation);
            Destroy(explosion2, explosion2.GetComponent<ParticleSystem>().duration);
        }*/
    }
}
