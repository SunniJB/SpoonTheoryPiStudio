using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingGameManager : MonoBehaviour
{
    public SortingGameSides redSide, blueSide;
    [SerializeField] GameObject[] cutlery;
    public GameObject winPanel, pausePanel, timer;
    private float minutes, seconds;

    private void Start()
    {
        redSide = GameObject.Find("RedSide").GetComponent<SortingGameSides>();
        blueSide = GameObject.Find("BlueSide").GetComponent<SortingGameSides>();
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
        if (redSide.full && blueSide.full)
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
        redSide.full = false;
        blueSide.full = false;
        seconds = 0f;
        minutes = 0f;
        Time.timeScale = 1f;
    }

    public void ChangeScene()
    {
        GameManager.Instance.ApartmentScene();
    }
}
