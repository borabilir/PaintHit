using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public Color[] color1;
    public Color[] color2;
    public Color[] color3;

    public static Color[] colorArray;

    void Start()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        int randomC = UnityEngine.Random.Range(0, 2);
        PlayerPrefs.SetInt("ColorSelect", randomC);
        PlayerPrefs.GetInt("ColorSelect");

        var colorSelect = PlayerPrefs.GetInt("ColorSelect");

        switch (colorSelect)
        {
            case 0:
                colorArray = color1;
                break;
            case 1:
                colorArray = color2;
                break;
            case 2:
                colorArray = color3;
                break;
            default:
                break;
        }

    }
}
