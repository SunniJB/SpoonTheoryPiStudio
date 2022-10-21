using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Image avatarImg, pausePanel, characterUIPanel, controlsPanel;

    public bool pause;

    [SerializeField] CharacterInteractor characterInteractor;

    [SerializeField] string audioName;

    public static LevelManager instance;
    public static LevelManager GetInstance() { return instance; }

    public GameObject wakeUpButton;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.gameObject.SetActive(false);
        controlsPanel.gameObject.SetActive(false);
        pause = false;
        Time.timeScale = 1;

        characterInteractor.RefreshStatsFromManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            AudioManager.GetInstance().Play(audioName, 1f);
            pausePanel.gameObject.SetActive(pause);
            characterUIPanel.gameObject.SetActive(!pause);

            if (pause)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }
        if (!GameManager.GetInstance().tutorialFinished) //Do not lock cursor in tutorial
        {
            LockCursor();
        }
    }

    public void LockCursor()
    {
        if (wakeUpButton != null)
        {
            if (!pause && Input.GetMouseButton(0) && Cursor.lockState != CursorLockMode.Locked && !characterInteractor.taskCanvasEnabled && !wakeUpButton.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else if (!pause && Input.GetMouseButton(0) && Cursor.lockState != CursorLockMode.Locked && !characterInteractor.taskCanvasEnabled && wakeUpButton == null)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void ShowControls()
    {
        controlsPanel.gameObject.SetActive(true);
        pausePanel.gameObject.SetActive(false);
    }

    public void BackToPauseMenu()
    {
        pausePanel.gameObject.SetActive(true);
        controlsPanel.gameObject.SetActive(false);
    }

    public void GoToMenu()
    {
        GameManager.GetInstance().MenuScene();
    }

    public void GoToWork()
    {
        GameManager.GetInstance().UpdateGameManagerStats(characterInteractor.money, characterInteractor.hunger, characterInteractor.numberOfSpoons, characterInteractor.hygiene, characterInteractor.happiness, characterInteractor.workPerformance);
        characterInteractor.hasSleptToday = false;
        GameManager.GetInstance().WorkScene();
    }
}
