using UnityEngine;
using System.Collections;

public class YellowMovement : MonoBehaviour
{
    bool moveDown = false;
    bool moveRight = false;
    bool moveUp = false;
    bool moveLeft = false;

    private const float MAX_TIME = 0.3f;
    private float mTimer;
    private int phase;

    void Start ()
    {
        mTimer = 0;
        phase = 0;

        if (Random.Range(0, 2) == 0)
            moveLeft = true;
        else
            moveRight = true;

        if (Random.Range(0, 2) == 0)
            moveDown = true;
        else
            moveUp = true;
    }
	
	void Update ()
    {
        if (transform.position.x <= -10.0f && moveLeft)
        {
            moveLeft = false;
            moveRight = true;
        }

        if (transform.position.x >= 6.0f && moveRight)
        {
            moveLeft = true;
            moveRight = false;
        }

        if (transform.position.y <= 5.0f && moveDown)
        {
            moveUp = true;
            moveDown = false;
        }

        if (transform.position.y >= 12.0f && moveUp)
        {
            moveUp = false;
            moveDown = true;
        }

        mTimer += Time.deltaTime;

        if (mTimer >= MAX_TIME)
        {
            if (phase == 0)
                transform.Translate(0, 0, -1);
            else if (phase == 1)
            {
                if (moveUp)
                {
                    transform.Translate(0, 1, 0);
                }
                else if (moveDown)
                {
                    transform.Translate(0, -1, 0);
                }
            }
            else if (phase == 2)
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

            phase++;

            if (phase > 3)
                phase = 0;

            mTimer = 0.0f;
        }
    }
}
