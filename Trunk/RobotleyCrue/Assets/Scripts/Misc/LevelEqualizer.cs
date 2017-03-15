using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelEqualizer : MonoBehaviour
{
    GameObject[] cubes;
    float scaleHeight = 90.0f;

    void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("Equalizer");
    }

    void Update()
    {
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Equalizer").Length; i++)
        {
            Vector3 prevScale = cubes[i].transform.localScale;
            prevScale.y = Mathf.Lerp(prevScale.y, spectrum[i] * scaleHeight, Time.deltaTime * 30);
            cubes[i].transform.localScale = prevScale;
        }
    }
}
