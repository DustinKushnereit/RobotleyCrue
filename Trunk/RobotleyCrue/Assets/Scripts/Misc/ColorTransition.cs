using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ColorTransition : MonoBehaviour
{
    public Material floorMaterialColor;
    public Material wallMaterialColor;
    public Material pillarMaterialColor;

    Color floorColor;
    Color wallColor;
    Color pillarColor;

    void Start ()
    {
        floorMaterialColor.SetColor("_TintColor", new Color(0.0f, 0.99f, 0.6f, 0.8f));
        floorColor = floorMaterialColor.GetColor("_TintColor");

        wallMaterialColor.SetColor("_TintColor", new Color(0.9f, 0.1f, 1.0f, 0.5f));
        wallColor = wallMaterialColor.GetColor("_TintColor");

        pillarMaterialColor.SetColor("_TintColor", new Color(0.0f, 1.0f, 0.5f, 0.5f));
        pillarColor = pillarMaterialColor.GetColor("_TintColor");
    }

    void Update()
    {
        lerpGreen();
        lerpBlue();
    }
    
    void resetColor()
    {
        floorMaterialColor.SetColor("_TintColor", new Color(0.0f, 0.99f, 0.6f, 0.8f));
        floorColor = floorMaterialColor.GetColor("_TintColor");

        wallMaterialColor.SetColor("_TintColor", new Color(0.9f, 0.1f, 1.0f, 0.5f));
        wallColor = wallMaterialColor.GetColor("_TintColor");

        pillarMaterialColor.SetColor("_TintColor", new Color(0.0f, 1.0f, 0.5f, 0.5f));
        pillarColor = pillarMaterialColor.GetColor("_TintColor");
    }

    void lerpRed()
    {
        floorColor.r = Mathf.Lerp(1.0f, 0.1f, Mathf.PingPong(Time.time, 1));
        floorMaterialColor.SetColor("_TintColor", floorColor);
    }

    void lerpBlue()
    {
        wallColor.b = Mathf.Lerp(1.0f, 0.1f, Mathf.PingPong(Time.time, 1));
        wallMaterialColor.SetColor("_TintColor", wallColor);
    }

    void lerpGreen()
    {
        floorColor.g = Mathf.Lerp(1.0f, 0.1f, Mathf.PingPong(Time.time, 1));
        floorMaterialColor.SetColor("_TintColor", floorColor);

        pillarColor.g = Mathf.Lerp(1.0f, 0.1f, Mathf.PingPong(Time.time, 1));
        pillarMaterialColor.SetColor("_TintColor", pillarColor);
    }
}
