using UnityEngine;
using System.Collections;

public class GreenMovement : MonoBehaviour
{
    private float mTimer;
    private const float MAX_TIME = 0.6f;

    void Start()
    {
        mTimer = 0.0f;
    }

    void Update()
    {
        mTimer += Time.deltaTime;

        if(mTimer >= MAX_TIME)
        {
            transform.Translate(0, 0, -1);

            mTimer = 0.0f;
        }
    }
}
