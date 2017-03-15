using UnityEngine;
using System.Collections;

public class RedMovement : MonoBehaviour {

    bool moveRight = false;
    bool moveLeft = false;

    private const float MAX_TIME = 0.3f;
    private float mTimer;
    private int phase;

    void Start()
    {
        mTimer = 0;
        phase = 0;

        if (Random.Range(0, 2) == 0)
            moveLeft = true;
        else
            moveRight = true;
    }

    void Update()
    {
        if (transform.position.x <= -10.0f && moveLeft)
        {
            moveLeft = false;
            moveRight = true;

            phase = 1;
        }

        if (transform.position.x >= 6.0f && moveRight)
        {
            moveLeft = true;
            moveRight = false;

            phase = 1;
        }

        mTimer += Time.deltaTime;

        if (mTimer >= MAX_TIME)
        {
            if (phase == 0)
            {
                if (moveLeft)
                {
                    transform.Translate(-1, 0, 0);
                }
                else if (moveRight)
                {
                    transform.Translate(1, 0, 0);
                }
            }
            else if (phase == 1)
            {
                transform.Translate(0, 0, -1);
                phase = 0;
            }

            //phase++;

            //if (phase > 1)
            //    phase = 0;

            mTimer = 0.0f;
        }
    }
}
