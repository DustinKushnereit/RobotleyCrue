using UnityEngine;
using System.Collections;

public class TintScreen : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    
    public void OnGUI()
    {
        Texture2D texture = new Texture2D(1, 1);
        Color currentBlendColor = new Color(1, 0, 0, 1);
        Color fromColor = new Color(1, 0, 0, 1);
        Color toColor = new Color(1, 0, 0, 0);
        float speed = 5;
        texture.SetPixel(0, 0, Color.white);
        currentBlendColor = Color.Lerp( currentBlendColor, toColor, Time.deltaTime * speed );
        GUI.color = currentBlendColor;
        GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), texture, ScaleMode.ScaleToFit); 
    }
}