using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public int goalMoneyAmount;
    public GameObject winPanel;

    private void Start()
    {
        GameManager.GetInstance().moneyGoal = goalMoneyAmount;
    }

    private void Update()
    {
        if (IsGoalAchieved())
        {
            GoalAchieved();
        }
    }

    public void GoalAchieved()
    {
        //what happens when you achieve the goal
        winPanel.SetActive(true);
        Time.timeScale = 0;
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
}
