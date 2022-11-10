using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepInBed : MonoBehaviour
{
    public CharacterInteractor characterInteractor;
    public string audioName;

    public GameObject sleepPanel;
    public TMPro.TextMeshProUGUI moneyEarned, moneyToGoal, newDay, dayMood;

    public ObjectTask[] tasks;
    public TaskManager taskManager;

    private void Start()
    {
        characterInteractor = GameObject.Find("1st person character").GetComponent<CharacterInteractor>();
    }

    public void GoToSleep()
    {
        GameManager gm = GameManager.GetInstance();

        gm.workedAlready = false;
        characterInteractor.UpdateGameManagerStats();
        AudioManager.GetInstance().Play(audioName, 1f);
        gm.SetTimeMorning();
        gm.spoons = Random.Range(10, 25);
        gm.hunger -= 7;
        gm.hygiene -= 5;
        gm.dayCount++;
        characterInteractor.RefreshStatsFromManager();
        characterInteractor.hasSleptToday = true;

        if (GameManager.GetInstance().spoons < 17)
            dayMood.text = "Today is a bad day, you have less spoons";
        else
            dayMood.text = "Today is a good day, you have more spoons";

        UpdateSleepPanel();
    }

    public void UpdateSleepPanel()
    {
        moneyEarned.text = "Current money: " + GameManager.GetInstance().money.ToString("0");
        moneyToGoal.text = "Money to goal: " + ((GameManager.GetInstance().moneyGoal - GameManager.GetInstance().money).ToString("0"));
        newDay.text = "It is now day " + (GameManager.GetInstance().dayCount + 1) + ". Hunger and hygiene have decreased.";

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
