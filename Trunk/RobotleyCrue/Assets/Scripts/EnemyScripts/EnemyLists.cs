using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyLists : MonoBehaviour {

    [HideInInspector]
    public List<GameObject> m_GreenEnemies;
    [HideInInspector]
    public List<GameObject> m_RedEnemies;
    [HideInInspector]
    public List<GameObject> m_YellowEnemies;
    [HideInInspector]
    public List<GameObject> m_BlueEnemies;

    [HideInInspector]
    public List<float> m_xPositions;
    [HideInInspector]
    public List<float> m_yPositions;

    [HideInInspector]
    public List<float> m_xEndPositions;
    [HideInInspector]
    public List<float> m_yEndPositions;

	// Use this for initialization
	void Start () {

        m_GreenEnemies = new List<GameObject>();
        m_RedEnemies = new List<GameObject>();
        m_YellowEnemies = new List<GameObject>();
        m_BlueEnemies = new List<GameObject>();

        m_xPositions = new List<float>();
        m_xPositions.Add(1.25f);
        m_xPositions.Add(-2.25f);
        m_xPositions.Add(-5.75f);
        m_xPositions.Add(-9.25f);

        m_xEndPositions = new List<float>();
        m_xEndPositions.Add(0.75f);
        m_xEndPositions.Add(-2.25f);
        m_xEndPositions.Add(-5.25f);
        m_xEndPositions.Add(-8.25f);

        m_yPositions = new List<float>();
        m_yPositions.Add(14.5f);
        m_yPositions.Add(11.0f);
        m_yPositions.Add(7.5f);
        m_yPositions.Add(4.0f);

        m_yEndPositions = new List<float>();
        m_yEndPositions.Add(12.5f);
        m_yEndPositions.Add(9.5f);
        m_yEndPositions.Add(6.5f);
        m_yEndPositions.Add(3.5f);

	}
}
