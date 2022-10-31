using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public float workPerform;
    float money;

    public void Complete(int _spoonCost, float _totalTime, float _dividend = 100)
    {
        workPerform = (_dividend / (_totalTime / 10)) - 5 + GameManager.GetInstance().happiness;
        if (workPerform > 50) workPerform = 50;
        money = 10.9f * (workPerform / 10);
        GameManager.GetInstance().money += money;
        GameManager.GetInstance().spoons -= _spoonCost;
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
}
