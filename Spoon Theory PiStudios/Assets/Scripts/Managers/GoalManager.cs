using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public int goalMoneyAmount;
    public GameObject winPanel, losePanel;

    private void Start()
    {
        if (GameManager.GetInstance().isIsaac)
        {
            goalMoneyAmount = 1850;
        }
        else
        {
            goalMoneyAmount = 1364;
        }
        GameManager.GetInstance().moneyGoal = goalMoneyAmount;
    }

    private void Update()
    {
        if (IsGoalAchieved())
        {
            GoalAchieved();
        }

        if (NoTimeLeft())
        {
            TimeRanOut();
        }
    }

    public void GoalAchieved()
    {
        //what happens when you achieve the goal
        winPanel.SetActive(true);
        AudioManager.GetInstance().StopAllSounds();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void TimeRanOut()
    {
        //what happens when the player has run out of time
        losePanel.SetActive(true);
        Time.timeScale = 0;
        AudioManager.GetInstance().StopAllSounds();
        Cursor.lockState = CursorLockMode.None;
    }

    public bool IsGoalAchieved()
    {
        // checks if the goal has been achieved
        if (GameManager.GetInstance().money >= goalMoneyAmount)
            return true;
        else
            return false;
    }

    public bool NoTimeLeft()
    {
        // checks if time has run out
        if (GameManager.GetInstance().totalDaysBeforeLoss - GameManager.GetInstance().dayCount <= 0)
            return true;
        else
            return false;
    }
}
