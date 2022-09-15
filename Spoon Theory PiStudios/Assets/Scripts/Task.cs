using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tasks", menuName = "New Task", order = 1)]
public class Task : ScriptableObject
{
    public string taskName = "Task name";

    [Tooltip("message to promp in UI")]
    public string description = "Task description";

    public int spoonCost, hygieneCost, moneyCost, hungerCost, happinesCost, workPerformanceCost;

    [HideInInspector]
    public Outline outlineObject;
    //public bool pinned, completed;
    //public enum Difficulty {Easy, Medium, Hard}
    //public Difficulty difficulty;
}
