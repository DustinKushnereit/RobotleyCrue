using UnityEngine;
using System.Collections;

public class PanningTextures : MonoBehaviour
{
    //Changes the textures
    //Makes it look like a flikering billboard
    public Texture[] textures;
    public float changeInterval = 0.33F;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (textures.Length == 0)
            return;

        int index = Mathf.FloorToInt(Time.time / changeInterval);
        index = index % textures.Length;
        rend.material.mainTexture = textures[index];
    }
}
