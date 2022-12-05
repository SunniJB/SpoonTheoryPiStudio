using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Image avatarImg, pausePanel, characterUIPanel, controlsPanel, shopPanel, optionsPanel;
    [SerializeField] string[] pauseTips = {"Look at your bed and press F to go to sleep.", "Blue tasks give you some spoons.", "You can adjust the sound volume in the settings.", "If you run out of spoons, you can't go to work.", "Blue objects can be examined closer.", "A yellow outline means an object can be interacted with." };
    [SerializeField] GameObject pauseTipText;
    public bool pause, shouldLockCursor;

    public bool shopPanelEnabled;

    [SerializeField] CharacterInteractor characterInteractor;

    [SerializeField] string audioName;

    public GameObject wakeUpButton;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.gameObject.SetActive(false);
        controlsPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(false);
        shopPanelEnabled = false;

        pause = false;
        Time.timeScale = 1;

        characterInteractor.RefreshStatsFromManager();
    }

    // Update is called once per frame
    void Update()
    {
        PauseMenu();

        ShopMenu();

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

    void PauseMenu()
    {
        
        if (controlsPanel.gameObject.activeInHierarchy) return;
        
        if (optionsPanel.gameObject.activeInHierarchy) return;

        if (characterInteractor.taskCanvasEnabled) return;

        if (TutorialManager.GetInstance() != null) return;

        if (shopPanelEnabled) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseTipText.GetComponent<TMP_Text>().text = pauseTips[Random.Range(0, pauseTips.Length)];

            if (characterInteractor.inspecting)
            {
                characterInteractor.FinishInspecting(); return;
            }

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
    }

    void ShopMenu()
    {
        if (pause) return;

        if (characterInteractor.taskCanvasEnabled) return;

        if(Input.GetKeyDown(KeyCode.P))
        {
            shopPanelEnabled = !shopPanelEnabled;

            shopPanel.gameObject.SetActive(shopPanelEnabled);
            characterInteractor.characterMovement.canMove = !shopPanelEnabled;
            characterInteractor.characterMovement.moving = !shopPanelEnabled;
        }
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

    public void ShowOptions()
    {
        optionsPanel.gameObject.SetActive(true);
        pausePanel.gameObject.SetActive(false);
    }

    public void BackToPauseMenu()
    {
        pausePanel.gameObject.SetActive(true);
        controlsPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(false);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        GameManager.GetInstance().tutorialFinished = false;
        GameManager.GetInstance().MenuScene();
    }

    public void GoToWork()
    {
        GameManager.GetInstance().UpdateGameManagerStats(characterInteractor.money, characterInteractor.hunger, characterInteractor.numberOfSpoons, characterInteractor.hygiene, characterInteractor.happiness, characterInteractor.workPerformance);
        characterInteractor.hasSleptToday = false;
        GameManager.GetInstance().WorkScene();
    }
}
