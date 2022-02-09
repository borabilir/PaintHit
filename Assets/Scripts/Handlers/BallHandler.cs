using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallHandler : MonoBehaviour
{
    public static float rotationSpeed = 75;
    public static float rotationTime = 3;
    public static int currentCircleNo;
    private static int circleCount;

    public static Color oneColor;
    public GameObject ball;
    public GameObject dummyBall;
    public GameObject btn;
    public GameObject levelComplete;
    public GameObject failScreen;
    public GameObject startGameScreen;
    public GameObject circleEffect;
    public GameObject completeEffect;

    private float speed = 100;

    private int ballCount, circleNo = 0, heartNo;

    private Color[] changingColors;

    public SpriteRenderer sprite;
    public Material splashMaterial;

    private bool gameFail;
    public Image[] balls;
    public GameObject[] Hearts;

    public Text total_balls_text;
    public Text count_balls_text;

    public Text levelCompleteText;

    public AudioSource completeSound;
    public AudioSource gameFailSound;
    public AudioSource hitSound;

    void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        changingColors = ColorController.colorArray;
        oneColor = changingColors[0];
        sprite.color = oneColor;
        splashMaterial.color = oneColor;
        ChangeBallsCount();

        GameObject gameObject = Instantiate(Resources.Load("round" + UnityEngine.Random.Range(1, 4))) as GameObject;
        gameObject.transform.position = new Vector3(0, 20, 23);
        gameObject.name = "Circle" + circleNo;
        ballCount = LevelHandler.ballCount;

        currentCircleNo = circleNo;
        LevelHandler.currentColor = oneColor;

        if (heartNo == 0)
            PlayerPrefs.SetInt("hearts", 1);
        heartNo = PlayerPrefs.GetInt("hearts", 1);

        for (int i = 0; i < heartNo; i++)
            Hearts[i].SetActive(true);

        MakeHurdles();
    }

    public void HeartsLow()
    {
        heartNo--;
        PlayerPrefs.SetInt("hearts", heartNo);
        Hearts[heartNo].SetActive(false);
    }

    public void FailGame()
    {
        gameFailSound.Play();
        gameFail = true;
        Invoke(nameof(FailScreen), 1);
        btn.SetActive(false);
        StopCircle();
    }

    void StopCircle()
    {
        GameObject gameObject = GameObject.Find("Circle" + circleNo);
        gameObject.transform.GetComponent<MonoBehaviour>().enabled = false;
        if (gameObject.GetComponent<iTween>())
            gameObject.GetComponent<iTween>().enabled = false;
    }

    void FailScreen()
    {
        failScreen.SetActive(true);
    }

    public void HitBall()
    {
        if (ballCount <= 1)
        {
            StartCoroutine(HideBtn());
            Invoke(nameof(MakeANewCircle), 0.4f);
            //Disable button for some time
        }

        ballCount--;

        if (ballCount >= 0)
            balls[ballCount].enabled = false;

        GameObject gameObject = Instantiate(ball, new Vector3(0, 0, -8), Quaternion.identity);
        gameObject.GetComponent<MeshRenderer>().material.color = oneColor;
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed, ForceMode.Impulse);
        hitSound.Play();
    }

    void ChangeBallsCount()
    {
        ballCount = LevelHandler.ballCount;
        dummyBall.GetComponent<MeshRenderer>().material.color = oneColor;

        total_balls_text.text = string.Empty + LevelHandler.totalCircles;
        count_balls_text.text = string.Empty + circleCount;

        for (int i = 0; i < balls.Length; i++)
            balls[i].enabled = false;

        for (int i = 0; i < ballCount; i++)
        {
            balls[i].enabled = true;
            balls[i].color = oneColor;
        }
    }

    void MakeANewCircle()
    {
        if (circleCount >= LevelHandler.totalCircles && !gameFail)
        {
            completeSound.Play();
            StartCoroutine(LevelCompleteScreen());
        }
        else
        {
            StartCoroutine(CircleEffect());
            GameObject[] array = GameObject.FindGameObjectsWithTag("circle");
            GameObject circle = GameObject.Find("Circle" + circleNo);

            for (int i = 0; i < 24; i++)
            {
                circle.transform.GetChild(i).gameObject.SetActive(false);
            }
            circle.transform.GetChild(24).gameObject.GetComponent<MeshRenderer>().material.color = BallHandler.oneColor;

            if (circle.GetComponent<iTween>())
            {
                circle.GetComponent<iTween>().enabled = false;
            }
            foreach (var target in array)
            {
                iTween.MoveBy(target, iTween.Hash(new object[]
                {
                "y",
                -2.98f,
                "easetype",
                iTween.EaseType.spring,
                "time",
                0.5
                }));
            }
            circleNo++;
            currentCircleNo = circleNo;

            GameObject gameObject = Instantiate(Resources.Load("round" + UnityEngine.Random.Range(1, 4))) as GameObject;
            gameObject.transform.position = new Vector3(0, 20, 23);
            gameObject.name = "Circle" + circleNo;

            ballCount = LevelHandler.ballCount;
            oneColor = changingColors[circleNo % 8];
            sprite.color = oneColor;
            splashMaterial.color = oneColor;

            LevelHandler.currentColor = oneColor;
            circleCount++;
            MakeHurdles();
            ChangeBallsCount();
        }
    }

    void MakeHurdles()
    {
        if (circleNo == 1)
        {
            FindObjectOfType<LevelHandler>().MakeHurdles1();
        }
        if (circleNo == 2)
        {
            FindObjectOfType<LevelHandler>().MakeHurdles2();
        }
        if (circleNo == 3)
        {
            FindObjectOfType<LevelHandler>().MakeHurdles3();
        }
        if (circleNo == 4)
        {
            FindObjectOfType<LevelHandler>().MakeHurdles4();
        }
        if (circleNo == 5)
        {
            FindObjectOfType<LevelHandler>().MakeHurdles5();
        }
    }

    IEnumerator HideBtn()
    {
        if(!gameFail)
        {
            btn.SetActive(false);
            yield return new WaitForSeconds(1);
            btn.SetActive(true);
        }
    }

    IEnumerator LevelCompleteScreen()
    {
        gameFail = true;

        completeEffect.SetActive(true);

        if (GameObject.Find("Circle0"))
        {
            completeEffect.transform.position = GameObject.Find("Circle0").transform.position;
        }
        else if (GameObject.Find("Circle1"))
        {
            completeEffect.transform.position = GameObject.Find("Circle1").transform.position;
        }
        else if (GameObject.Find("Circle2"))
        {
            completeEffect.transform.position = GameObject.Find("Circle2").transform.position;
        }

        GameObject oldCirlce = GameObject.Find("Circle" + circleNo);
        for (int i = 0; i < 24; i++)
        {
            oldCirlce.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }
        oldCirlce.transform.GetChild(24).gameObject.GetComponent<MeshRenderer>().material.color = oneColor;
        oldCirlce.transform.GetComponent<MonoBehaviour>().enabled = false;
        if (oldCirlce.GetComponent<iTween>())
            oldCirlce.GetComponent<iTween>().enabled = false;
        btn.SetActive(false);
        yield return new WaitForSeconds(2);
        levelComplete.SetActive(true);
        levelCompleteText.text = string.Empty + LevelHandler.currentLevel;
        GameObject[] oldCirlces = GameObject.FindGameObjectsWithTag("circle");
        foreach (GameObject gameObject in oldCirlces)
        {
            Destroy(gameObject.gameObject);
        }
        yield return new WaitForSeconds(1);
        completeEffect.SetActive(false);
        int currentLevel = PlayerPrefs.GetInt("C_Level");
        currentLevel++;
        PlayerPrefs.SetInt("C_Level", currentLevel);
        GameObject.FindObjectOfType<LevelHandler>().UpgradeLevel();
        ResetGame();
        levelComplete.SetActive(false);
        startGameScreen.SetActive(true);
        gameFail = false;
    }

    public void DeleteAllCircles()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("circle");
        foreach (GameObject gameObject in array)
        {
            Destroy(gameObject.gameObject);
        }
        gameFail = false;
        FindObjectOfType<LevelHandler>().UpgradeLevel();
        ResetGame();
    }

    IEnumerator CircleEffect()
    {
        yield return new WaitForSeconds(.4f);
        circleEffect.SetActive(true);
        yield return new WaitForSeconds(.8f);
        circleEffect.SetActive(false);
    }

}
