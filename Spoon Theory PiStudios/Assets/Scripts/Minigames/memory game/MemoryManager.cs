using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager instance;

    public int spoonCost;
    public MinigameManager _mm;

    public Sprite[] flippedLibrary = new Sprite[5];
    public GameObject[] flipPlates = new GameObject[10];
    public GameObject firstClickObj, winPanel;
    public Sprite clickOne = null, clickTwo = null;
    public int wins;
    public TMP_Text minTxt, workTxt, moneTxt, finalTimeTxt;

    private float totalTime;
    private float workPerform, money;
    private bool timeIsOn, moneygiven, clickedStart;
    private float timerSec, timerMin;
    private GameObject tempFp;
    private int[] numbers = new int[10];
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (timeIsOn)
        timerSec += Time.deltaTime;

        if (timerSec >= 60f)
        {
            timerSec += -60;
            timerMin += 1;
        }

        //secTxt.text = timerSec.ToString();
        minTxt.text = timerMin.ToString("0") + ":" + timerSec.ToString("f1");

        if (wins == 5)
        {
            //What happens when you win goes here!!
            FinishMinigame();
        }
    }

    private void Shuffle()
    {
        for (int i = 0; i < flipPlates.Length; i++)
        {
            int rando = Random.Range(i, flipPlates.Length);
            tempFp = flipPlates[rando];
            flipPlates[rando] = flipPlates[i];
            flipPlates[i] = tempFp;
        }
    }

    public void RestartMemory()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    public void OnClick()
    {
        clickedStart = true;
        moneygiven = false;
        timeIsOn = true;
        Time.timeScale = 1;

        for (int i = 1; i <= 10; i++)
        {
            numbers[i - 1] = Mathf.CeilToInt((float)i / 2);
        }

        Shuffle();

        for (int i = 0; i < 10; i++)
        {
            flipPlates[i].GetComponent<ClickMemory>().images[1] = flippedLibrary[numbers[i] - 1];
        }
    }

    public void GoHome()
    {
        GameManager.GetInstance().SetTimeAfternoon();
        GameManager.GetInstance().ApartmentScene();
    }

    public void FinishMinigame()
    {
        timeIsOn = false;
        winPanel.SetActive(true);
        totalTime = timerMin * 60 + timerSec;

        if (!moneygiven)
        {
            if(clickedStart)
            _mm.Complete(spoonCost, totalTime, 60);


            finalTimeTxt.text = "Your final time was : " + timerMin.ToString("0") + ":" + timerSec.ToString("f1");
            workTxt.text = "Performance Review: " + _mm.GetWorkPerform().ToString("00") + "/50";
            if (_mm.workPerform < 12.5)
            {
                workTxt.text = "You didn't do great. There is always tomorrow.";
            }
            if (_mm.workPerform > 12.5f && _mm.workPerform < 37.5f)
            {
                workTxt.text = "You did some good work today.";
            }
            if (_mm.workPerform > 37.5f)
            {
                workTxt.text = "Wow, you did great!";
            }
            moneTxt.text = "you earned: £" + _mm.GetMoney().ToString("000");
            moneygiven = true;
        }
    }
}
