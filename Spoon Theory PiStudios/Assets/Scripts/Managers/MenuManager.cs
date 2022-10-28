using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject canvas, cutsceneBackground;

    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject buttons;

    [SerializeField] Slider sfx, music;

    private void Start()
    {
        buttons.SetActive(true);
        optionsPanel.SetActive(false);

        sfx.value = AudioManager.GetInstance().startVolume;
        music.value = AudioManager.GetInstance().startVolume;
    }

    public void Begin()
    {
        GameManager.GetInstance().dayCount = 0;
        GameManager.GetInstance().UpdateGameManagerStats(0, 5, Random.Range(10, 31), 5, 10, 0);
        GameManager.GetInstance().SetTimeMorning();
        GameManager.GetInstance().ApartmentScene();
    }

    public void Levels()
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

    public void SFXSlider(float value)
    {
        AudioManager.GetInstance().AudioVolume(value, false);
    }
    public void MusicSlider(float value)
    {
        AudioManager.GetInstance().AudioVolume(value, true);
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
        cutsceneBackground.SetActive(true);
        canvas.GetComponent<Animator>().SetTrigger("cutscene");
    }
}
