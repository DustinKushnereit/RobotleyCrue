using UnityEngine;
using System.Collections;

public class BlueMovement : MonoBehaviour {

    private float mTimer;
    private const float MAX_TIME = 0.3f;
    private int phase;

    void Start()
    {
        mTimer = 0.0f;
        phase = 0;
    }

    void Update()
    {
        mTimer += Time.deltaTime;

        if (mTimer >= MAX_TIME)
        {
            if(phase == 0)
                transform.Translate(0, -1, 0);
            else if(phase == 3)
                transform.Translate(0, 1, -1);

            phase++;

            if (phase > 3)
                phase = 0;

            mTimer = 0.0f;
        }
    }
}
