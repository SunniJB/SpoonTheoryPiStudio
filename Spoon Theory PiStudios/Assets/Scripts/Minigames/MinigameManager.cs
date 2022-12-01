using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [HideInInspector] public float workPerform;
    [SerializeField] FinishMinigame finishMinigame;
    float money;

    public void Complete(int _spoonCost, float _totalTime, float _dividend = 100)
    {
        workPerform = (_dividend / (_totalTime / 10)) - 5 + GameManager.GetInstance().happiness;
        if (workPerform > 50) workPerform = 50;
        money = 10.9f * (workPerform / 10);
        GameManager.GetInstance().money += money;
        GameManager.GetInstance().spoons -= _spoonCost;

        finishMinigame.StartAnimationVoid(_spoonCost, money, this);
    }

    public void Finish()
    {
        switch(GameManager.GetInstance().ActualScene())
        {
            case "SortingMinigame":
                FindObjectOfType<SortingGameManager>().Win();
                break;
            case "SimonSays":
                FindObjectOfType<SimonSaysManager>().Win();
                break;
            case "MemoryTest":
                break;
        }
    }

    public void Skip()
    {
        Complete(Random.Range(2, 4), Random.Range(10, 35));
    }

    public float GetWorkPerform()
    {
        return workPerform;
    }

    public float GetMoney()
    {
        return money;
    }

    public void ReturnToRestaurant()
    {
        GameManager.GetInstance().WorkScene(false);
    }
}
