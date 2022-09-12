using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingGameManager : MonoBehaviour
{
    public SortingGameSides redSide, blueSide;
    [SerializeField] GameObject[] cutlery;
    public GameObject winPanel, pausePanel, timer;
    public float time;

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
        time += Time.timeScale;
        timer.GetComponent<TMPro.TextMeshProUGUI>().text = "Time: " + time.ToString();
        if (redSide.full && blueSide.full)
        {
            winPanel.SetActive(true);
            GameObject.Find("Final time").GetComponent<TMPro.TextMeshProUGUI>().text = "Your final time was: " + time.ToString();
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
        time = 0f;
        Time.timeScale = 1f;
    }
}
