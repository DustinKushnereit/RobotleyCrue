using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainTextureEqualizer : MonoBehaviour
{
    List<GameObject> buildings;
    public float subWooferAmount;
    string textureName = "_MainTex";
    Vector2 uvOffset = Vector2.zero;

    void Update()
    {
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

        buildings = new List<GameObject>(GameObject.FindGameObjectsWithTag("Building"));

        if (GameObject.FindGameObjectsWithTag("Building").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Building").Length; i++)
            {
                uvOffset.x = Mathf.Lerp(uvOffset.x, spectrum[i] * subWooferAmount, Time.deltaTime * 1);
                buildings[i].GetComponent<Renderer>().material.SetTextureOffset(textureName, uvOffset);
            }
        }
    }
}
