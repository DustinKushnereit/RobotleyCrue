using UnityEngine;
using System.Collections;

public class GeneralMovement : MonoBehaviour
{
    float incrementingZ;

    void Start ()
    {
	
	}
	
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + incrementingZ * 5.0f * Time.deltaTime);

        if (incrementingZ > -.2f)
            incrementingZ -= 0.1f;

        if (transform.position.z < GameObject.FindGameObjectWithTag("Player").transform.position.z - 1.0f)
            Destroy(this.gameObject);
    }
}
