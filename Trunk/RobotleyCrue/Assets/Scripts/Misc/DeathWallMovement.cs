using UnityEngine;
using System.Collections;

public class DeathWallMovement : MonoBehaviour
{
	void Start ()
    {
	
	}
	
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.8f * 5.0f * Time.deltaTime);
    }
}
