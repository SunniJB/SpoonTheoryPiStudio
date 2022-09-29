using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingGameManager : MonoBehaviour
{
    public SortingGameSides spoonGoal, forkGoal, knifeGoal;
    [SerializeField] GameObject[] cutlery;
    public GameObject winPanel, pausePanel, timer;
    private float minutes, seconds;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        pausePanel = GameObject.Find("PausePanel");
        timer = GameObject.Find("Timer");
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
            winPanel.SetActive(true);
            GameObject.Find("Final time").GetComponent<TMPro.TextMeshProUGUI>().text = "Your final time was: " + minutes.ToString() + ":" + seconds.ToString("f1");
            Time.timeScale = 0f;
        }
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
        GameManager.Instance.ApartmentScene();
    }

    public void NextMinigame()
    {
        GameManager.Instance.MemoryGameScene();
    }
}
