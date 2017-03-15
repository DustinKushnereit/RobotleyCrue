using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Equalizer : MonoBehaviour
{
    GameObject obj;
    int numOfObjects = 20;
    float radius = 5.0f;
    GameObject[] cubes;

    //int numberOfEnemies = 0;
    List<GameObject> enemiesBlue;
    List<GameObject> enemiesGreen;
    List<GameObject> enemiesRed;
    List<GameObject> enemiesYellow;
    float scaleHeight = 60.0f;

    void Start ()
    {
        //To create your own eqaulizer circle
        /*for(int i = 0; i < numOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Instantiate(obj, pos, Quaternion.identity);
            cubes = GameObject.FindGameObjectsWithTag("cubes");
        }*/

        //Get enemies on screen
        //getEnemies();
        

    }
	
	void Update ()
    {
        //getEnemies();

        //float[] samples = new float[1024];
        //GetComponent<AudioSource>().GetOutputData(samples, 0);
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

        enemiesBlue = new List<GameObject>(GameObject.FindGameObjectsWithTag("BlueEnemy"));
        enemiesGreen = new List<GameObject>(GameObject.FindGameObjectsWithTag("GreenEnemy"));
        enemiesRed = new List<GameObject>(GameObject.FindGameObjectsWithTag("RedEnemy"));
        enemiesYellow = new List<GameObject>(GameObject.FindGameObjectsWithTag("YellowEnemy"));

        if (GameObject.FindGameObjectsWithTag("BlueEnemy").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("BlueEnemy").Length; i++)
            {
                Vector3 prevScale = enemiesBlue[i].transform.localScale;
                Vector3 enemyPos = enemiesBlue[i].transform.position;
                prevScale.y = Mathf.Lerp(prevScale.y, spectrum[1] * scaleHeight, Time.deltaTime * 10);
                enemiesBlue[i].transform.localScale = prevScale;
                enemiesBlue[i].transform.position = enemyPos;
            }
        }

        if (GameObject.FindGameObjectsWithTag("GreenEnemy").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("GreenEnemy").Length; i++)
            {
                Vector3 prevScale = enemiesGreen[i].transform.localScale;
                Vector3 enemyPos = enemiesGreen[i].transform.position;
                prevScale.y = Mathf.Lerp(prevScale.y, spectrum[1] * scaleHeight, Time.deltaTime * 10);
                enemiesGreen[i].transform.localScale = prevScale;
                enemiesGreen[i].transform.position = enemyPos;
            }
        }

        if (GameObject.FindGameObjectsWithTag("RedEnemy").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("RedEnemy").Length; i++)
            {
                Vector3 prevScale = enemiesRed[i].transform.localScale;
                Vector3 enemyPos = enemiesRed[i].transform.position;
                prevScale.y = Mathf.Lerp(prevScale.y, spectrum[1] * scaleHeight, Time.deltaTime * 10);
                enemiesRed[i].transform.localScale = prevScale;
                enemiesRed[i].transform.position = enemyPos;
            }
        }

        if (GameObject.FindGameObjectsWithTag("YellowEnemy").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("YellowEnemy").Length; i++)
            {
                Vector3 prevScale = enemiesYellow[i].transform.localScale;
                Vector3 enemyPos = enemiesYellow[i].transform.position;
                prevScale.y = Mathf.Lerp(prevScale.y, spectrum[1] * scaleHeight, Time.deltaTime * 10);
                enemiesYellow[i].transform.localScale = prevScale;
                enemiesYellow[i].transform.position = enemyPos;
            }
        }
    }

    void getEnemies()
    {
        int numBlue = 0;
        int numRed = 0;
        int numGreen = 0;
        int numYel = 0;

        if (GameObject.FindGameObjectsWithTag("GreenEnemy").Length > 0)
        {
            //for (int i = 0; i < GameObject.FindGameObjectsWithTag("GreenEnemy").Length; i++)
            {
                enemiesGreen = new List<GameObject> (GameObject.FindGameObjectsWithTag("GreenEnemy"));
                numGreen = GameObject.FindGameObjectsWithTag("GreenEnemy").Length;
            }
        }

        /*if (GameObject.FindGameObjectsWithTag("BlueEnemy").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("BlueEnemy").Length; i++)
            {
                enemies.Add(GameObject.FindGameObjectsWithTag("BlueEnemy")[i]);
                numberOfEnemies++;
            }
        }*/

        if (GameObject.FindGameObjectsWithTag("BlueEnemy").Length > 0)
        {
            enemiesBlue = new List<GameObject>(GameObject.FindGameObjectsWithTag("BlueEnemy"));
            numBlue = GameObject.FindGameObjectsWithTag("BlueEnemy").Length;
        }

        if (GameObject.FindGameObjectsWithTag("YellowEnemy").Length > 0)
        {
            enemiesRed = new List<GameObject>(GameObject.FindGameObjectsWithTag("YellowEnemy"));
            numYel = GameObject.FindGameObjectsWithTag("YellowEnemy").Length;
        }

        if (GameObject.FindGameObjectsWithTag("RedEnemy").Length > 0)
        {
            enemiesYellow = new List<GameObject>(GameObject.FindGameObjectsWithTag("RedEnemy"));
            numRed = GameObject.FindGameObjectsWithTag("RedEnemy").Length;
        }

        //numberOfEnemies = numBlue + numGreen + numRed + numYel;
    }
}
