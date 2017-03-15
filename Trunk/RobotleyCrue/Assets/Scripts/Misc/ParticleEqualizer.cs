using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ParticleEqualizer : MonoBehaviour
{
    List<GameObject> enemiesBlue;
    List<GameObject> enemiesGreen;
    List<GameObject> enemiesRed;
    List<GameObject> enemiesYellow;
    float scaleHeight = 120.0f;

    void Start()
    {
    }

    void Update()
    {
        /*float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

        enemiesBlue = new List<GameObject>(GameObject.FindGameObjectsWithTag("BlueEnemy"));
        enemiesGreen = new List<GameObject>(GameObject.FindGameObjectsWithTag("GreenEnemy"));
        enemiesRed = new List<GameObject>(GameObject.FindGameObjectsWithTag("RedEnemy"));
        enemiesYellow = new List<GameObject>(GameObject.FindGameObjectsWithTag("YellowEnemy"));

        if (GameObject.FindGameObjectsWithTag("BlueEnemy").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("BlueEnemy").Length; i++)
            {
                enemiesBlue[i].GetComponent<Enemy>().equalizer(spectrum, i, scaleHeight);
            }
        }

        //if (GameObject.FindGameObjectsWithTag("GreenEnemy").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("GreenEnemy").Length; i++)
            {
                Debug.Log("here " + i);
                enemiesGreen[i].GetComponent<Enemy>().equalizer(spectrum, i, scaleHeight);
                Debug.Log("here2 " + i);
                //enemiesGreen[i].GetComponent<Enemy>().equalizer();
            }
        }

        if (GameObject.FindGameObjectsWithTag("RedEnemy").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("RedEnemy").Length; i++)
            {
                enemiesRed[i].GetComponent<Enemy>().equalizer(spectrum, i, scaleHeight);
            }
        }

        if (GameObject.FindGameObjectsWithTag("YellowEnemy").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("YellowEnemy").Length; i++)
            {
                enemiesYellow[i].GetComponent<Enemy>().equalizer(spectrum, i, scaleHeight);
            }
        }*/
    }
}
