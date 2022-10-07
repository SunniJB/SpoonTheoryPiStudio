using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public static TutorialManager GetInstance() { return instance; }
    enum TutorialStates { Start, TaskMenu, CompleteTask, Finish };
    TutorialStates tutorialStates;

    [SerializeField] Outline door;

    public bool startFinished, taskmenuFinished, completeTaskFinished, finishFinished;

    [SerializeField] GameObject movePanel, taskPanel, completeTaskPanel, finishTutorial1, finishTutorial2;

    [SerializeField] Transform player;

    private void Awake()
    {
        // Singleton
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tutorialStates = TutorialStates.Start;

        startFinished = false;
        taskmenuFinished = false;
        completeTaskFinished = false;
        finishFinished = false;

        movePanel.SetActive(false);
        taskPanel.SetActive(false);
        completeTaskPanel.SetActive(false);
        finishTutorial1.SetActive(false);
        finishTutorial2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (tutorialStates)
        {
            case TutorialStates.Start:
                TutorialStart();
                break;

            case TutorialStates.TaskMenu:
                TutorialTaskMenu();
                break;

            case TutorialStates.CompleteTask:
                TutorialCompleteTask();
                break;

            case TutorialStates.Finish:
                TutorialFinish();
                break;
        }
    }

    void TutorialStart()
    {
        movePanel.SetActive(true);
        taskPanel.SetActive(false);

        if (player.position.z < movePanel.transform.position.z) startFinished = true;

        if (startFinished) tutorialStates = TutorialStates.TaskMenu;
    }

    void TutorialTaskMenu()
    {
        movePanel.SetActive(false);
        taskPanel.SetActive(true);

        if (taskmenuFinished) tutorialStates = TutorialStates.CompleteTask;
    }

    void TutorialCompleteTask()
    {
        completeTaskPanel.SetActive(true);
        taskPanel.SetActive(false);

        if (completeTaskFinished) { tutorialStates = TutorialStates.Finish; Invoke("Tutorial2Text", 10); door.enabled = true; }
    }

    void TutorialFinish()
    {
        completeTaskPanel.SetActive(false);
        finishTutorial1.SetActive(true);

        if (player.position.z > finishTutorial1.transform.position.z) finishTutorial1.SetActive(false);
        
        if (player.position.z > finishTutorial2.transform.position.z && finishTutorial2.activeInHierarchy) Destroy(finishTutorial2);

        if (finishFinished) LeaveTutorial();
    }

    void Tutorial2Text()
    {
        finishTutorial2.SetActive(true);
    }

    void LeaveTutorial()
    {
        GameManager.GetInstance().tutorialFinished = true;
        GameManager.GetInstance().ApartmentScene();
    }
}
