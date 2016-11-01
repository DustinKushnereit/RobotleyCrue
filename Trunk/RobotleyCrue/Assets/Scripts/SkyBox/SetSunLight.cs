using UnityEngine;
using System.Collections;

public class SetSunLight : MonoBehaviour
{
	public Renderer lightwall;
	Material sky;

	public Renderer water;
	public Transform stars;
	public Transform worldProbe;
    public GameObject playerCamera;
    
	void Start () 
	{
		sky = RenderSettings.skybox;
	}
    
	void Update () 
	{
        stars.transform.Rotate(new Vector3(-0.5f, 0, -0.2f) * Time.deltaTime * 1);

		Color final = Color.white * Mathf.LinearToGammaSpace(5);
		lightwall.material.SetColor("_EmissionColor", final);
		DynamicGI.SetEmissive(lightwall, final);
	
		Vector3 tvec = playerCamera.transform.position;
		worldProbe.transform.position = tvec;

		water.material.mainTextureOffset = new Vector2(Time.time / 100, 0);
		water.material.SetTextureOffset("_DetailAlbedoMap", new Vector2(0, Time.time / 80));
	}
}
