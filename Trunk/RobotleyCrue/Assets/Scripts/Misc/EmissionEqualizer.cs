using UnityEngine;
using System.Collections;

public class EmissionEqualizer : MonoBehaviour
{
    GameObject[] buildings;
    public float gammaAmount;

    void Start()
    {
        buildings = GameObject.FindGameObjectsWithTag("Building");
    }

    void Update()
    {
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Building").Length; i++)
        {
            Color final = Color.white * Mathf.LinearToGammaSpace(gammaAmount);
            final = Color.white * Mathf.Lerp(gammaAmount, spectrum[i] * gammaAmount + 0.2f, Time.deltaTime * 20);
            buildings[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", final);
            DynamicGI.SetEmissive(buildings[i].GetComponent<Renderer>(), final);
        }
    }
}
