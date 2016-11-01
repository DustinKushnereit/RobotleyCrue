using UnityEngine;
using System.Collections;

public class TwoStepMovement : MonoBehaviour
{
    private const float MAX_TIME_TO_MOVE = 1.0f;
    private const float MOVEMENT_SPEED = 0.5f;
    private float moveTimer;
    private int steps;
    private const float forardSpeed = 0.5f;

    private const float MAXDISTANCE = 10.0f;
    private float MAXY;
    private const float LERPSPEED = 0.1f;
    private float lerpTimer;
    private float startYPos;
    private float startXPos;
    private float endXPos;

    bool hasNotReachedPlayer = true;
    GameObject player;
    GameObject enemyLists;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyLists = GameObject.FindGameObjectWithTag("EnemyLists");

        steps = 0;
        moveTimer = 0.0f;
        MAXY = enemyLists.GetComponent<EnemyLists>().m_yEndPositions[3];
        startYPos = transform.position.y;
        startXPos = transform.position.x;

        if (startXPos == enemyLists.GetComponent<EnemyLists>().m_xPositions[0])
        {
            endXPos = enemyLists.GetComponent<EnemyLists>().m_xEndPositions[0];
        }
        else if (startXPos == enemyLists.GetComponent<EnemyLists>().m_xPositions[1])
        {
            endXPos = enemyLists.GetComponent<EnemyLists>().m_xEndPositions[1];
        }
        else if (startXPos == enemyLists.GetComponent<EnemyLists>().m_xPositions[2])
        {
            endXPos = enemyLists.GetComponent<EnemyLists>().m_xEndPositions[2];
        }
        else if (startXPos == enemyLists.GetComponent<EnemyLists>().m_xPositions[3])
        {
            endXPos = enemyLists.GetComponent<EnemyLists>().m_xEndPositions[3];
        }
    }
    
    void Update()
    {
        moveTimer += Time.deltaTime;

        keepAtDistance();

        if (hasNotReachedPlayer)
        {
            if (moveTimer >= MAX_TIME_TO_MOVE)
            {
                moveTimer = 0.0f;
                moveEnemy();
            }
        }
        else
        {
            moveWithPlayer();
        }
    }

    void keepAtDistance()
    {
        //float distance = Vector3.Distance(transform.position, player.transform.position);

        if (transform.position.z <= player.transform.position.z + 10.0f)
        {
            hasNotReachedPlayer = false;
        }
    }

    private void moveEnemy()
    {
        Vector3 newPos = transform.position;

        lerpTimer += LERPSPEED;
        if (lerpTimer < 1.1f)
        {
            newPos.y = startYPos + lerpTimer * (MAXY - startYPos);
        }

        if (steps != 2)
        {
            newPos.z -= forardSpeed;
        }
        else
        {
            newPos.z += MOVEMENT_SPEED;
            steps = -1;
        }

        steps++;

        if (lerpTimer < 1.1f)
            newPos.x = startXPos + lerpTimer * (endXPos - startXPos);
        //else
            //newPos.x = endXPos;

        bool inList = false;
        for (int i = 0; i < enemyLists.GetComponent<EnemyLists>().m_BlueEnemies.Count; i++)
        {
            if (newPos.x.ToString("F2") == enemyLists.GetComponent<EnemyLists>().m_BlueEnemies[i].transform.position.x.ToString("F2")
                && newPos.y.ToString("F2") == enemyLists.GetComponent<EnemyLists>().m_BlueEnemies[i].transform.position.y.ToString("F2")
                && newPos.z.ToString("F2") == enemyLists.GetComponent<EnemyLists>().m_BlueEnemies[i].transform.position.z.ToString("F2"))
            {
                inList = true;
            }
        }

        if (!inList)
            transform.position = newPos;
    }

    void moveWithPlayer()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + 10.0f);
    }
}
