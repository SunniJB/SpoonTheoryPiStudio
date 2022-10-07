using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SortingGameManager : MonoBehaviour
{
    public TMP_Text workPerfTxt, moneyTxt;
    public SortingGameSides spoonGoal, forkGoal, knifeGoal;
    [SerializeField] GameObject[] cutlery;
    public GameObject winPanel, pausePanel, timer;
    private float minutes = 0, seconds;

    public MinigameManager minigameManager;

    private bool gotPaid;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        //pausePanel = GameObject.Find("PausePanel");
        //timer = GameObject.Find("Timer");
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        seconds += Time.deltaTime;
        if (seconds > 60)
        {
            minutes += 1;
            seconds = 0;
        }

        timer.GetComponent<TMPro.TextMeshProUGUI>().text = "Time: " + minutes.ToString() + ":" + seconds.ToString("f1");
        if (spoonGoal.full && knifeGoal.full && forkGoal.full)
        {
            Win();
        }
    }

    void Win()
    {
        winPanel.SetActive(true);
        GameObject.Find("Final time").GetComponent<TMPro.TextMeshProUGUI>().text = "Your final time was: " + minutes.ToString() + ":" + seconds.ToString("f1");
        Time.timeScale = 0f;

        if (gotPaid == false)
        {
            minigameManager.Complete(3, minutes * 60 + seconds);
            gotPaid = true;
        }
        workPerfTxt.text = "Performance Review: " + minigameManager.GetWorkPerform().ToString("00") + "/50";
        moneyTxt.text = "You Earned: £" + minigameManager.GetMoney().ToString("00");
    }

    public void Restart()
    {
        winPanel.SetActive(false);
        foreach (GameObject item in cutlery)
        {
            item.GetComponent<SortingGame_Cutlery>().Restart();
        }
        spoonGoal.full = false;
        forkGoal.full = false;
        knifeGoal.full = false;
        seconds = 0f;
        minutes = 0f;
        Time.timeScale = 1f;
    }

    public void ChangeScene()
    {
        GameManager.GetInstance().SetTimeAfternoon();
        GameManager.GetInstance().ApartmentScene();
    }

    public void NextMinigame()
    {
        GameManager.GetInstance().MemoryGameScene();
    }

    //private void Complete()
    //{
    //    int spoonCost = 3;
    //    float totalTime = minutes * 60 + seconds;
    //    float workPerform = (100 / (totalTime / 10)) - 5 + GameManager.GetInstance().happiness;
    //    if (workPerform > 50) workPerform = 50;
    //    float money = 10.9f * (workPerform / 10);
    //    GameManager.GetInstance().money += money;
    //    GameManager.GetInstance().spoons -= spoonCost;
    //}

    public void Skip()
    {
        gotPaid = true;
        Win();
    }
}