using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public static TutorialManager GetInstance() { return instance; }
    enum TutorialStates { Start, Move, TaskMenu, CompleteTask, Finish };
    TutorialStates tutorialStates;

    public bool startFinished, moveFinished, taskmenuFinished, completeTaskFinished, finishFinished;

    [SerializeField] GameObject movePanel, taskPanel;

    [SerializeField] Transform player;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        tutorialStates = TutorialStates.Start;

        startFinished = false;
        moveFinished = false;
        taskmenuFinished = false;
        completeTaskFinished = false;
        finishFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (tutorialStates)
        {
            case TutorialStates.Start:
                TutorialStart();
                break;
            case TutorialStates.Move:
                TutorialMove();
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

        if (startFinished) tutorialStates = TutorialStates.Move;
    }

    void TutorialMove()
    {
        movePanel.SetActive(false);
        taskPanel.SetActive(true);

        if (moveFinished) tutorialStates = TutorialStates.TaskMenu;
    }

    void TutorialTaskMenu()
    {
        taskPanel.SetActive(false);

        if (taskmenuFinished) tutorialStates = TutorialStates.CompleteTask;
    }

    void TutorialCompleteTask()
    {
        movePanel.SetActive(false);

        if (completeTaskFinished) tutorialStates = TutorialStates.Finish;
    }

    void TutorialFinish()
    {
        movePanel.SetActive(false);

        if (finishFinished) LeaveTutorial();
    }

    void LeaveTutorial()
    {
        GameManager.GetInstance().tutorialFinished = true;
    }
}
