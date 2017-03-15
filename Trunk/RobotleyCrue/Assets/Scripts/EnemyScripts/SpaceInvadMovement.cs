using UnityEngine;
using System.Collections;

public class SpaceInvadMovement : MonoBehaviour
{
    private const float MAX_TIME_TO_MOVE = 1.0f;
    private const float MOVEMENT_SPEED = 1.0f;
    private float moveTimer;

    private const float MAXDISTANCE = 10.0f;
    private const float MAXY = 9.5f;
    private const float LERPSPEED = 0.1f;
    private const float LERPSPEED2 = 0.01f;
    private float lerpTimer;
    private float lerpTimer2;
    private float startYPos;

    private bool goingRight;
    private int index;

    bool hasNotReachedPlayer = true;
    GameObject player;
    GameObject enemyLists;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyLists = GameObject.FindGameObjectWithTag("EnemyLists");

        goingRight = true;
        lerpTimer2 = 0.0f;

        for (int i = 0; i < enemyLists.GetComponent<EnemyLists>().m_xPositions.Count; i++)
        {
            if (transform.position.x == enemyLists.GetComponent<EnemyLists>().m_xPositions[i])
            {
                index = i;
                break;
            }
        }

        startYPos = transform.position.y;
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
        lerpTimer2 += LERPSPEED2;

        if (lerpTimer < 1.1f)
        {
            newPos.y = startYPos + lerpTimer * (MAXY - startYPos);
        }

        if (goingRight && index + 1 < enemyLists.GetComponent<EnemyLists>().m_xPositions.Count)
        {
            index += 1;
            newPos.x = enemyLists.GetComponent<EnemyLists>().m_xPositions[index];
        }
        else if (index - 1 > -1)
        {
            goingRight = false;

            index -= 1;
            newPos.x = enemyLists.GetComponent<EnemyLists>().m_xPositions[index];

            if (index == 0)
            {
                goingRight = true;
            }
        }

        if (lerpTimer < 1.1f)
            newPos.x = enemyLists.GetComponent<EnemyLists>().m_xPositions[index] + lerpTimer * (enemyLists.GetComponent<EnemyLists>().m_xEndPositions[index] - enemyLists.GetComponent<EnemyLists>().m_xPositions[index]);
        else
            newPos.x = enemyLists.GetComponent<EnemyLists>().m_xEndPositions[index];

        if (index == 0 || index == enemyLists.GetComponent<EnemyLists>().m_xPositions.Count - 1)
        {
            newPos.z -= MOVEMENT_SPEED;
        }

        bool inList = false;
        for (int i = 0; i < enemyLists.GetComponent<EnemyLists>().m_RedEnemies.Count; i++)
        {
            if (newPos.x.ToString("F2") == enemyLists.GetComponent<EnemyLists>().m_RedEnemies[i].transform.position.x.ToString("F2")
                && newPos.y.ToString("F2") == enemyLists.GetComponent<EnemyLists>().m_RedEnemies[i].transform.position.y.ToString("F2")
                && newPos.z.ToString("F2") == enemyLists.GetComponent<EnemyLists>().m_RedEnemies[i].transform.position.z.ToString("F2"))
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
