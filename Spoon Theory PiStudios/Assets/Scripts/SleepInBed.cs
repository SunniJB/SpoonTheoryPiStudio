using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepInBed : MonoBehaviour
{
    public CharacterInteractor characterInteractor;
    public string audioName;

    private void Start()
    {
        characterInteractor = GameObject.Find("1st person character").GetComponent<CharacterInteractor>();
    }
    public void GoToSleep()
    {
        Debug.Log("You slept");
        AudioManager.GetInstance().Play(audioName, 1f);
        GameManager.GetInstance().SetTimeMorning();
        GameManager.GetInstance().spoons = Random.Range(10, 31);
        GameManager.GetInstance().hunger -= 7;
        GameManager.GetInstance().hygiene -= 5;
        GameManager.GetInstance().dayCount++;
        characterInteractor.RefreshStatsFromManager();
    }
}
