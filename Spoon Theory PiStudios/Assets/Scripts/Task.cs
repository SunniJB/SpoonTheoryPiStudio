using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tasks", menuName = "New Task", order = 1)]
public class Task : ScriptableObject
{
    public string taskName = "Task name", description = "Task description";

    public int spoonCost;
    //public bool pinned, completed;
    public enum Difficulty {Easy, Medium, Hard}
    public Difficulty difficulty;
}
