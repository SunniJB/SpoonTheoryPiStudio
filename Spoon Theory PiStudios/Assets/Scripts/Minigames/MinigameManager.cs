using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Complete(int _spoonCost, float _totalTime)
    {
        float workPerform = (100 / (_totalTime / 10)) - 5 + GameManager.GetInstance().happiness;
        if (workPerform > 50) workPerform = 50;
        float money = 10.9f * (workPerform / 10);
        GameManager.GetInstance().money += money;
        GameManager.GetInstance().spoons -= _spoonCost;
    }

    public void Skip()
    {
        Complete(Random.Range(2, 4), Random.Range(10, 35));
    }
}
