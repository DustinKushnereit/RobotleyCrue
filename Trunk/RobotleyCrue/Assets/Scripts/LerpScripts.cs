using UnityEngine;
using System.Collections;

public class LerpScripts : MonoBehaviour {

    public float startYPos;
    public float endYPos;
    public float lerpTime;

    private bool isUP;

    private float mTimer;
	// Use this for initialization
	void Start () {

        isUP = true;

        mTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 newPos = transform.localPosition; 

        if(isUP)
        {
            mTimer += lerpTime;
            if (mTimer < 1.1f)
            {
                newPos.y = startYPos + mTimer * (endYPos - startYPos);
            }
            else
            {
                mTimer = 0.0f;
                isUP = !isUP;
            }
        }
        else
        {
            mTimer += lerpTime;
            if (mTimer < 1.1f)
            {
                newPos.y = endYPos + mTimer * (startYPos - endYPos);
            }
            else
            {
                mTimer = 0.0f;
                isUP = !isUP;
            }
        }


        transform.localPosition = newPos;
	}
}
