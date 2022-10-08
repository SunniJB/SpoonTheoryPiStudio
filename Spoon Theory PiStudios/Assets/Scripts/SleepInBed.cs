using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepInBed : MonoBehaviour
{
    public CharacterInteractor characterInteractor;
    public string audioName;

    public GameObject sleepPanel;
    public TMPro.TextMeshProUGUI moneyEarned, moneyToGoal, newDay; 

    private void Start()
    {
        characterInteractor = GameObject.Find("1st person character").GetComponent<CharacterInteractor>();
    }

    public void GoToSleep()
    {
        AudioManager.GetInstance().Play(audioName, 1f);
        GameManager.GetInstance().SetTimeMorning();
        GameManager.GetInstance().spoons = Random.Range(10, 31);
        GameManager.GetInstance().hunger -= 7;
        GameManager.GetInstance().hygiene -= 5;
        GameManager.GetInstance().dayCount++;
        characterInteractor.RefreshStatsFromManager();

        UpdateSleepPanel();
    }

    public void UpdateSleepPanel()
    {
        moneyEarned.text = "Current money: " + GameManager.GetInstance().money;
        moneyToGoal.text = "Money to goal: " + (GameManager.GetInstance().moneyGoal - GameManager.GetInstance().money);
        newDay.text = "It is now day " + GameManager.GetInstance().dayCount + ". Hunger and hygiene have decreased.";

        sleepPanel.GetComponent<Animator>().SetTrigger("goingToSleep");

        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
    }

    public void WakeUp()
    {
        sleepPanel.GetComponent<Animator>().SetTrigger("wakingUp");
        Debug.Log("Button clicked");

        Cursor.lockState = CursorLockMode.Locked;
    }
}
