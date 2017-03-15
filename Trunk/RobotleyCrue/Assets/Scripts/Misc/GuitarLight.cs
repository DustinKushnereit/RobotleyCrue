using UnityEngine;
using System.Collections;

public class GuitarLight : MonoBehaviour
{
    public float gammaAmount;

    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color final = Color.white * Mathf.LinearToGammaSpace(gammaAmount);
        
        renderer.materials[0].SetColor("_EmissionColor", final);
        DynamicGI.SetEmissive(renderer, final);

        renderer.materials[1].SetColor("_EmissionColor", final);
        DynamicGI.SetEmissive(renderer, final);

    }
}
