using UnityEngine;
using System.Collections;

public class LightWall : MonoBehaviour
{
	Renderer renderer;
    
	void Start () 
	{
		renderer = GetComponent<Renderer>();
	}
	
	void Update () 
	{
	
		//if (Input.GetKeyDown(KeyCode.L))
		{
			Color final = Color.white * Mathf.LinearToGammaSpace(4);
			renderer.material.SetColor("_EmissionColor", final);
			DynamicGI.SetEmissive(renderer, final);
		}

    }
}
