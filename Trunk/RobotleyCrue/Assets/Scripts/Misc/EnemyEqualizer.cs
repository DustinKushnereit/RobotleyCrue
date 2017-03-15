using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class EnemyEqualizer : MonoBehaviour
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
    List<GameObject> enemies;
    float scaleHeight = 20.0f;
    float scaler;

    float lerpTime = 1f;
    float currentLerpTime;
    float[] spectrum;

    //New Spectrum
    public int numOfSamples = 512; //Min: 64, Max: 8192
    float[] freqData;
    float[] band;
    //GameObject[] g;

    void Start()
    {
        freqData = new float[numOfSamples];

        int n = freqData.Length;

        int k = 0;
        for (int j = 0; j < freqData.Length; j++)
        {
            n = n / 2;
            if (n <= 0)
                break;
            k++;
        }

        band = new float[k + 1];
        //g = new GameObject[k + 1];


        for (int i = 0; i < band.Length; i++)
        {
            band[i] = 0;
            //g[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //g[i].GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
            //g[i].transform.position = new Vector3(i, 0, 0);
        }

        InvokeRepeating("check", 0.0f, 1.0f / 15.0f); // update at 15 fps
    }

    void Update()
    {
        /*float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }

        if (currentLerpTime > 1)
        {
            currentLerpTime = 0;
        }

        currentLerpTime += Time.deltaTime;

        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        
        float perc = currentLerpTime / lerpTime;

        //getEnemies();

        //float[] samples = new float[1024];
        //GetComponent<AudioSource>().GetOutputData(samples, 0);
        //spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);
        spectrum = AudioListener.GetOutputData(1024, 0);  // this gives much  better values.

        enemiesBlue = new List<GameObject>(GameObject.FindGameObjectsWithTag("BlueEnemy"));
        enemiesGreen = new List<GameObject>(GameObject.FindGameObjectsWithTag("GreenEnemy"));
        enemiesRed = new List<GameObject>(GameObject.FindGameObjectsWithTag("RedEnemy"));
        enemiesYellow = new List<GameObject>(GameObject.FindGameObjectsWithTag("YellowEnemy"));

        int spawnRange = Random.Range(0, 9);
        float spectrumRange1 = spectrum[spawnRange];// + spectrum[4];
        float spectrumRange2 = spectrum[12];// + spectrum[44];
        float spectrumRange3 = spectrum[22];// + spectrum[24];
        float spectrumRange4 = spectrum[32];// + spectrum[34];

        if (GameObject.FindGameObjectsWithTag("BlueEnemy").Length > 0 && enemiesBlue != null)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("BlueEnemy").Length; i++)
            {
                Vector3 prevPos = enemiesBlue[i].transform.position;
                //scaler = spectrum[i] * scaleHeight + 6;
                //if (scaler > 15)
                //    scaler = 15;
                //prevPos.y = Mathf.Lerp(prevPos.y, scaler, Time.deltaTime * 10);
                prevPos.y = Mathf.Lerp(prevPos.y, spectrumRange1 * scaleHeight + 6, perc);
                enemiesBlue[i].transform.position = prevPos;
            }
        }

        if (GameObject.FindGameObjectsWithTag("GreenEnemy").Length > 0 && enemiesGreen != null)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("GreenEnemy").Length; i++)
            {
                Vector3 prevPos = enemiesGreen[i].transform.position;
                prevPos.y = Mathf.Lerp(prevPos.y, spectrumRange2 * scaleHeight + 6, perc);
                enemiesGreen[i].transform.position = prevPos;
            }
        }

        if (GameObject.FindGameObjectsWithTag("RedEnemy").Length > 0 && enemiesRed != null)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("RedEnemy").Length; i++)
            {
                Vector3 prevPos = enemiesRed[i].transform.position;
                prevPos.y = Mathf.Lerp(prevPos.y, spectrumRange3 * scaleHeight + 6, perc);
                enemiesRed[i].transform.position = prevPos;
            }
        }

        if (GameObject.FindGameObjectsWithTag("YellowEnemy").Length > 0 && enemiesYellow != null)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("YellowEnemy").Length; i++)
            {
                Vector3 prevPos = enemiesYellow[i].transform.position;

                prevPos.y = Mathf.Lerp(prevPos.y, spectrumRange4 * scaleHeight + 6, perc);
                //MoveObject(enemiesYellow[i], prevPos.y + 1, spectrum[i] * scaleHeight + 3, 3.0f);
                enemiesYellow[i].transform.position = prevPos;
            }
        }
    }

    IEnumerator MoveObject(GameObject obj, float startPos, float endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        Vector3 prevPos = obj.transform.position;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            prevPos.y = Mathf.Lerp(startPos, endPos, i);
            yield return null; 
        }*/
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
                enemiesGreen = new List<GameObject>(GameObject.FindGameObjectsWithTag("GreenEnemy"));
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

    private void check()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0 && enemies != null)
        {
            int k = GameObject.FindGameObjectsWithTag("Enemy").Length;
            band = new float[k];

            k = 0;

            AudioListener.GetSpectrumData(freqData, 0, FFTWindow.Rectangular);

            int crossover = 2;

            for (int i = 0; i < freqData.Length; i++)
            {
                float d = freqData[i];
                float b = band[k];

                if (d > b)
                    band[k] = d;
                else
                    band[k] = b;

                //band[k] = (d > b) ? d : b;
                if (i > (crossover - 3))
                {
                    k++;
                    crossover *= 2;
                    //Vector3 prevPos = enemiesBlue[j].transform.position;
                    //prevPos.y = Mathf.Lerp(prevPos.y, spectrumRange1 * scaleHeight + 6, perc);
                    //enemiesBlue[i].transform.position = prevPos;
                    Vector3 tmp = new Vector3(enemies[k].transform.position.x, band[k] * 32, enemies[k].transform.position.z);
                    enemies[k].transform.position = tmp;
                    band[k] = 0;
                }
            }
        }
    }

}