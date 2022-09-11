using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    List<Task> displayedTasks;
    List<Task> completedTasks = new List<Task>();

    [SerializeField] Task[] morningTasks, eveningTasks;

    List<CheckBox> checkboxes = new List<CheckBox>();

    [SerializeField] GameObject taskPrefab;

    [SerializeField] int maxTasksPinned = 5;
    public int currentNumberofTasksPinned = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.dayTime == GameManager.DayTime.Morning)
        {
            displayedTasks = new List<Task>(morningTasks);
        }
        else if (GameManager.Instance.dayTime == GameManager.DayTime.Evening)
        {
            displayedTasks = new List<Task>(eveningTasks);
        }

        int i = 0;
        foreach (Task task in displayedTasks)
        {
            GameObject clon = Instantiate(taskPrefab, this.transform);
            checkboxes.Add(clon.GetComponent<CheckBox>());
            checkboxes[i].task = task;
            checkboxes[i].text.text = task.taskName;
            checkboxes[i].taskManager = this;
            i++;
        }
    }

    public void CheckTasksPinned()
    {
        if (currentNumberofTasksPinned >= maxTasksPinned)
        {
            foreach (CheckBox cb in checkboxes)
            {
                Toggle toggle = cb.GetComponent<Toggle>();
                if (!toggle.isOn)
                {
                    toggle.interactable = false;
                }
            }
        }

        else
        {
            foreach (CheckBox cb in checkboxes)
            {
                Toggle toggle = cb.GetComponent<Toggle>();
                toggle.interactable = true;
            }
        }
    }

    public void TaskCompleted(Task task)
    {
        completedTasks.Add(task);
        displayedTasks.Remove(task);

        foreach(CheckBox cb in checkboxes)
        {
            if (cb.task == task)
            {
                Destroy(cb.gameObject);
                checkboxes.Remove(cb);
                return;
            }
        }
    }
}
