using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image background;

    public Sprite[] sprite;

    void Start()
    {
        background.sprite = sprite[Random.Range(0, 4)];    
    }

}
