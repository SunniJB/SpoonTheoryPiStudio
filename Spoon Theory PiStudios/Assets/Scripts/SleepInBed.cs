using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepInBed : MonoBehaviour
{
    public CharacterInteractor characterInteractor;

    private void Start()
    {
        characterInteractor = GameObject.Find("1st person character").GetComponent<CharacterInteractor>();
    }
    public void GoToSleep()
    {
        GameManager.GetInstance().SetTimeMorning();
        GameManager.GetInstance().spoons = Random.Range(10, 31);
        GameManager.GetInstance().hunger -= 7;
        GameManager.GetInstance().hygiene -= 5;
        GameManager.GetInstance().dayCount++;
        characterInteractor.RefreshStatsFromManager();
    }
}
