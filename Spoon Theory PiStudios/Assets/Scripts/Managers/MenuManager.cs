using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject canvas, cutsceneBackground;

    [SerializeField] GameObject optionsPanel, cutScene, levelPanel, library;
    [SerializeField] GameObject buttons;

    private void Start()
    {
        buttons.SetActive(true);
        optionsPanel.SetActive(false);
        cutScene.SetActive(false);
        levelPanel.SetActive(false);
    }

    public void Begin()
    {
        GameManager.GetInstance().dayCount = 0;
        GameManager.GetInstance().UpdateGameManagerStats(0, 5, Random.Range(10, 25), 5, 10, 0);
        GameManager.GetInstance().SetTimeMorning();
        GameManager.GetInstance().ApartmentScene();
    }

    public void Levels()
    {
        levelPanel.SetActive(true);
    }

    public void LevelsOut()
    {
        levelPanel.SetActive(false);
    }

    public void ChloeLevel()
    {
        GameManager gm = GameManager.GetInstance();

        gm.dayCount = 0;
        gm.isIsaac = false;
        gm.tutorialFinished = true;
        gm.SetTimeMorning();
        gm.ApartmentScene();

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void IsaacLevel()
    {
        GameManager gm = GameManager.GetInstance();

        gm.dayCount = 0;
        gm.isIsaac = true;
        gm.UpdateGameManagerStats(0, 5, Random.Range(10, 25), 5, 10, 0);
        gm.SetTimeMorning();
        gm.tutorialFinished = true;
        gm.ApartmentScene();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Library()
    {
        library.SetActive(true);
    }

    public void Options()
    {
        buttons.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OptionsBack()
    {
        optionsPanel.SetActive(false);
        buttons.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void RunCutscene()
    {
        cutScene.SetActive(true);
        canvas.GetComponent<Animator>().SetTrigger("cutscene");
    }
}
