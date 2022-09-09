using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    Task[] displayedTasks;

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
            displayedTasks = morningTasks;
        }
        else if (GameManager.Instance.dayTime == GameManager.DayTime.Evening)
        {
            displayedTasks = eveningTasks;
        }

        int i = 0;
        foreach (Task task in displayedTasks)
        {
            GameObject clon = Instantiate(taskPrefab, this.transform);
            checkboxes.Add(clon.GetComponent<CheckBox>());
            checkboxes[i].text.text = task.name;
            checkboxes[i].taskManager = this;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
