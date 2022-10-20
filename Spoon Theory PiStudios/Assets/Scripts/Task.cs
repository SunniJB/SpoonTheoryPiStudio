using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tasks", menuName = "New Task", order = 1)]
public class Task : ScriptableObject
{
    public string taskName = "Task name";

    [Tooltip("message to promp in UI")]
    public string description = "Task description";

    public int spoonCost, hygieneCost, moneyCost, hungerCost, happinessCost, workPerformanceCost;

    public Task followingTask;

    [HideInInspector] public Outline outlineObject;

    [HideInInspector] public ObjectTask objectTask;

    [HideInInspector] public Task parent;

    [HideInInspector] public bool inProgress = false;

    public void FollowingTask(Task currentTask, Task nextTask)
    {
        currentTask.inProgress = false;

        currentTask.spoonCost = nextTask.spoonCost;
        currentTask.hygieneCost = nextTask.hygieneCost;
        currentTask.moneyCost = nextTask.moneyCost;
        currentTask.hungerCost = nextTask.hungerCost;
        currentTask.happinessCost = nextTask.happinessCost;
        currentTask.workPerformanceCost = nextTask.workPerformanceCost;

        currentTask.objectTask = nextTask.objectTask;
        currentTask.outlineObject = nextTask.outlineObject;
        currentTask.outlineObject.enabled = true;

        nextTask.objectTask.task = currentTask;
    }

    public void FollowingTask(Task nextTask)
    {
        nextTask.parent = this;

        nextTask.inProgress = false;

        nextTask.outlineObject.enabled = true;
    }

    public Task GetFirstParent()
    {
        Task firstParent, aux;

        aux = this.parent;

        if (aux == null) return this;
        else
        {
            do
            {
                firstParent = aux;
                aux = firstParent.parent;
            }
            while (aux != null);

            return firstParent;
        }
    }
}
