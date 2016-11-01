using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
	void Start ()
    {
	
	}
	
	void Update ()
    {
        float angleZ = transform.rotation.eulerAngles.y + (90 * Time.deltaTime);

        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, angleZ, transform.rotation.eulerAngles.z);
    }
}
