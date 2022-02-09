using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static int currentLevel;

    public static int ballCount;

    public static int totalCircles;

    public static Color currentColor;

    void Start()
    {
        if (PlayerPrefs.GetInt("firstTime1", 0) == 0)
        {
            PlayerPrefs.SetInt("firstTime1", 1);
            PlayerPrefs.SetInt("C_Level", 1);
        }
        UpgradeLevel();
    }

    public void UpgradeLevel()
    {
        currentLevel = PlayerPrefs.GetInt("C_Level", 1);

        if (currentLevel > 0 && currentLevel <= 10)
        {
            ballCount = 3;
            totalCircles = currentLevel + 2;
        }
        else if (currentLevel > 10 && currentLevel <= 25)
        {
            ballCount = 4;
            totalCircles = currentLevel + 2;
        }
        else if (currentLevel > 25)
        {
            ballCount = 5;
            totalCircles = currentLevel + 2;
            BallHandler.rotationSpeed = 95 + currentLevel;
            BallHandler.rotationTime = 3 - (float)(currentLevel * 0.01);
        }
    }

    public void MakeHurdles1()
    {
        GameObject gameObject = GameObject.Find("Circle" + BallHandler.currentCircleNo);

        int index = UnityEngine.Random.Range(1, 3);

        gameObject.transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.transform.GetChild(index).gameObject.GetComponent<MeshRenderer>().material.color = currentColor;
        gameObject.transform.GetChild(index).gameObject.tag = "red";
    }

    public void MakeHurdles2()
    {
        GameObject gameObject = GameObject.Find("Circle" + BallHandler.currentCircleNo);

        int[] array = new int[]
        {
            UnityEngine.Random.Range(1,3),
            UnityEngine.Random.Range(15,17),
        };

        for (int i = 0; i < array.Length; i++)
        {
            gameObject.transform.GetChild(array[i]).gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.transform.GetChild(array[i]).gameObject.GetComponent<MeshRenderer>().material.color = currentColor;
            gameObject.transform.GetChild(array[i]).gameObject.tag = "red";
        }
    }

    public void MakeHurdles3()
    {
        GameObject gameObject = GameObject.Find("Circle" + BallHandler.currentCircleNo);

        int[] array = new int[]
        {
            UnityEngine.Random.Range(1,3),
            UnityEngine.Random.Range(4,6),
            UnityEngine.Random.Range(18,20),
        };

        for (int i = 0; i < array.Length; i++)
        {
            gameObject.transform.GetChild(array[i]).gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.transform.GetChild(array[i]).gameObject.GetComponent<MeshRenderer>().material.color = currentColor;
            gameObject.transform.GetChild(array[i]).gameObject.tag = "red";
        }
    }

    public void MakeHurdles4()
    {
        GameObject gameObject = GameObject.Find("Circle" + BallHandler.currentCircleNo);

        int[] array = new int[]
        {
            UnityEngine.Random.Range(1,3),
            UnityEngine.Random.Range(4,6),
            UnityEngine.Random.Range(15,17),
            UnityEngine.Random.Range(22,24),
        };

        for (int i = 0; i < array.Length; i++)
        {
            gameObject.transform.GetChild(array[i]).gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.transform.GetChild(array[i]).gameObject.GetComponent<MeshRenderer>().material.color = currentColor;
            gameObject.transform.GetChild(array[i]).gameObject.tag = "red";
        }
    }

    public void MakeHurdles5()
    {
        GameObject gameObject = GameObject.Find("Circle" + BallHandler.currentCircleNo);

        int[] array = new int[]
        {
            UnityEngine.Random.Range(1,3),
            UnityEngine.Random.Range(4,6),
            UnityEngine.Random.Range(15,17),
            UnityEngine.Random.Range(8,10),
            UnityEngine.Random.Range(15,17),
        };

        for (int i = 0; i < array.Length; i++)
        {
            gameObject.transform.GetChild(array[i]).gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.transform.GetChild(array[i]).gameObject.GetComponent<MeshRenderer>().material.color = currentColor;
            gameObject.transform.GetChild(array[i]).gameObject.tag = "red";
        }
    }
}
