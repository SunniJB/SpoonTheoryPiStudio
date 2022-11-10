using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject canvas, cutsceneBackground;

    [SerializeField] GameObject optionsPanel, cutScene, levelPanel;
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

    }

    public void IsaacLevel()
    {

    }

    public void Library()
    {

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
