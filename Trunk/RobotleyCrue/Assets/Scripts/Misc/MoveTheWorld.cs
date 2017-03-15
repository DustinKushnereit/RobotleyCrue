using UnityEngine;
using System.Collections;

public class MoveTheWorld : MonoBehaviour
{
    float incrementingZ;
    public bool startMoving = false;

    void Start ()
    {	
	}
	
	void Update ()
    {
        if (startMoving)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + incrementingZ * 5.0f * Time.deltaTime);

            if(incrementingZ > -.2f)
                incrementingZ -= 0.1f;
        }
    }
}
