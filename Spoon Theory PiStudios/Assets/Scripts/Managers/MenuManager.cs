using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject canvas, cutsceneBackground;

    public void Begin()
    {
        GameManager.GetInstance().dayCount = 0;
        GameManager.GetInstance().UpdateGameManagerStats(0, 5, 5, 5, 10, 0);
        GameManager.GetInstance().SetTimeMorning();
        GameManager.GetInstance().ApartmentScene();
    }

    public void Levels()
    {

    }

    public void Library()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void RunCutscene()
    {
        cutsceneBackground.SetActive(true);
        canvas.GetComponent<Animator>().SetTrigger("cutscene");
    }
}
