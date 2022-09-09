using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingGameManager : MonoBehaviour
{
    public SortingGameSides redSide, blueSide;
    [SerializeField] GameObject[] cutlery;
    public GameObject winPanel;

    private void Start()
    {
        redSide = GameObject.Find("RedSide").GetComponent<SortingGameSides>();
        blueSide = GameObject.Find("BlueSide").GetComponent<SortingGameSides>();
    }

    private void Update()
    {
        if (redSide.full && blueSide.full)
        {
            winPanel.SetActive(true);
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
    }
}
