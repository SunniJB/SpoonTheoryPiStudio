using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Image avatarImg, pausePanel, characterUIPanel, controlsPanel;

    public bool pause, shouldLockCursor;

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
            if (controlsPanel.gameObject.activeInHierarchy) return;

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
                Time.timeScale = 1;
                if (shouldLockCursor)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }

        if (wakeUpButton != null)
        {
            if (wakeUpButton.activeInHierarchy)
            {
                shouldLockCursor = false;
            }
        }
        if (GameManager.GetInstance().tutorialFinished == false)
        {
            shouldLockCursor = false;
        }

        LockCursor();
    }

    public void LockCursor()
    {
         if (!pause && Input.GetMouseButton(0) && Cursor.lockState != CursorLockMode.Locked && !characterInteractor.taskCanvasEnabled && shouldLockCursor)
         {
            Debug.Log("Cursor got locked");
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
        Time.timeScale = 1;
        GameManager.GetInstance().MenuScene();
    }

    public void GoToWork()
    {
        GameManager.GetInstance().UpdateGameManagerStats(characterInteractor.money, characterInteractor.hunger, characterInteractor.numberOfSpoons, characterInteractor.hygiene, characterInteractor.happiness, characterInteractor.workPerformance);
        characterInteractor.hasSleptToday = false;
        GameManager.GetInstance().WorkScene();
    }
}
