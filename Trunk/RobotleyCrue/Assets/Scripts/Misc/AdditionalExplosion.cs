using UnityEngine;
using System.Collections;

public class AdditionalExplosion : MonoBehaviour
{
    bool doOnce = true;
    public GameObject particleSystemExplosion;

    void Start ()
    {
	
	}
	
	void Update ()
    {
	    if(doOnce)
        {
            doOnce = false;
            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
        }
	}
}
