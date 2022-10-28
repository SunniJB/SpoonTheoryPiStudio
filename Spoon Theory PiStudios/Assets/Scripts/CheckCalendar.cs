using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckCalendar : MonoBehaviour
{
    public TMP_Text currentMoney, moneyNeeded, daysLeft;
    public GameObject goalPanel;

    public void CheckGoal()
    {
        currentMoney.text = GameManager.GetInstance().money.ToString("0000");
        moneyNeeded.text = GameManager.GetInstance().moneyGoal.ToString("0000");
        daysLeft.text = (GameManager.GetInstance().totalDaysBeforeLoss - GameManager.GetInstance().dayCount).ToString("00");

        Cursor.lockState = CursorLockMode.None;
        goalPanel.SetActive(true);
    }

    public void BackToGame()
    {

        Cursor.lockState = CursorLockMode.Locked;
        goalPanel.SetActive(false);
    }
}
