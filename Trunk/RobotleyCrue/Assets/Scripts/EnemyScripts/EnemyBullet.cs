using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject top;
    public GameObject bottom;

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
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            GameObject explosionLeft = (GameObject)Instantiate(leftSide, new Vector3(player.transform.position.x - 3, player.transform.position.y, player.transform.position.z + 2.0f), leftSide.transform.rotation);
            Destroy(explosionLeft, explosionLeft.GetComponent<ParticleSystem>().duration);

            GameObject explosionRight = (GameObject)Instantiate(rightSide, new Vector3(player.transform.position.x + 3, player.transform.position.y, player.transform.position.z + 2.0f), rightSide.transform.rotation);
            Destroy(explosionRight, explosionRight.GetComponent<ParticleSystem>().duration);

            GameObject explosionTop = (GameObject)Instantiate(top, new Vector3(player.transform.position.x, player.transform.position.y + 2.0f, player.transform.position.z + 2.0f), top.transform.rotation);
            Destroy(explosionTop, explosionTop.GetComponent<ParticleSystem>().duration);

            GameObject explosionBottom = (GameObject)Instantiate(bottom, new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z + 2.0f), bottom.transform.rotation);
            Destroy(explosionBottom, explosionBottom.GetComponent<ParticleSystem>().duration);

            Destroy(this.gameObject);
        }
        else if(collider.gameObject.tag.Equals("Player2"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            GameObject explosionLeft = (GameObject)Instantiate(leftSide, new Vector3(player.transform.position.x - 3, player.transform.position.y, player.transform.position.z + 2.0f), leftSide.transform.rotation);
            Destroy(explosionLeft, explosionLeft.GetComponent<ParticleSystem>().duration);

            GameObject explosionRight = (GameObject)Instantiate(rightSide, new Vector3(player.transform.position.x + 3, player.transform.position.y, player.transform.position.z + 2.0f), rightSide.transform.rotation);
            Destroy(explosionRight, explosionRight.GetComponent<ParticleSystem>().duration);

            GameObject explosionTop = (GameObject)Instantiate(top, new Vector3(player.transform.position.x, player.transform.position.y + 2.0f, player.transform.position.z + 2.0f), top.transform.rotation);
            Destroy(explosionTop, explosionTop.GetComponent<ParticleSystem>().duration);

            GameObject explosionBottom = (GameObject)Instantiate(bottom, new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z + 2.0f), bottom.transform.rotation);
            Destroy(explosionBottom, explosionBottom.GetComponent<ParticleSystem>().duration);

            Destroy(this.gameObject);
        }
        /*else if (collider.gameObject.tag.Equals("GreenEnemy") || collider.gameObject.tag.Equals("RedEnemy") || collider.gameObject.tag.Equals("YellowEnemy") || collider.gameObject.tag.Equals("BlueEnemy"))
        {
            GameObject explosion = (GameObject)Instantiate(particleSystemExplosion, transform.position, particleSystemExplosion.transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            Destroy(this.gameObject);
        }*/
    }
}
