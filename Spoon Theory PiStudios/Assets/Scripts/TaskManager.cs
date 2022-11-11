using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    //list of tasks available for the player
    [SerializeField] Task[] morningTasks, eveningTasks, hardEveningTasks, tutorialTask;

    public List<Task> displayedTasks;
    List<CheckBox> checkboxes = new List<CheckBox>();
    [SerializeField] Transform[] columns;

    List<Task> completedTasks = new List<Task>();

    public List<Task> pinnedTasks = new List<Task>();
    List<GameObject> pinnedTaskGameObjects = new List<GameObject>();

    //prefabs a instanciar
    [SerializeField] GameObject taskPrefab, pinTaskPrefab;

    [SerializeField] int maxTasksPinned = 5;
    public int currentNumberofTasksPinned = 0;

    public Image pinnedTasksPanel;

    [SerializeField] Color easy, medium, hard, positive;

    [SerializeField] int mediumDif, hardDif;

    public CharacterInteractor interactor;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.GetInstance().tutorialFinished)
        {
            displayedTasks = new List<Task>(tutorialTask);
        }
        else
        {
            //depending of the time of the day the displayed tasks are different
            if (GameManager.GetInstance().dayTime == GameManager.DayTime.Morning)
            {
                displayedTasks = new List<Task>(morningTasks);
            }
            else if (GameManager.GetInstance().dayTime == GameManager.DayTime.Evening && interactor.spoonSlider.value < interactor.spoonSlider.maxValue / 2)
            {
                displayedTasks = new List<Task>(eveningTasks);
            }
            else if (GameManager.GetInstance().dayTime == GameManager.DayTime.Evening && interactor.spoonSlider.value > interactor.spoonSlider.maxValue / 2)
            {
                displayedTasks = new List<Task>(hardEveningTasks);
            }
        }
        int tasksPerColumn = displayedTasks.Count / columns.Length;
        int rest = displayedTasks.Count % columns.Length;

        //create the checkboxes for the correct displayed tasks
        int i = 0;
        foreach (Task task in displayedTasks)
        {
            GameObject clon = Instantiate(taskPrefab);
            checkboxes.Add(clon.GetComponent<CheckBox>());

            checkboxes[i].task = task;

            checkboxes[i].text.text = task.taskName;
            checkboxes[i].text.enableAutoSizing = true;

            //assign colors based on difficulty
            if (task.spoonCost < 0) checkboxes[i].text.color = positive;
            else
            {
                if (task.spoonCost < mediumDif) checkboxes[i].text.color = easy;
                else if (task.spoonCost < hardDif) checkboxes[i].text.color = medium;
                else checkboxes[i].text.color = hard;
            }

            checkboxes[i].taskManager = this;

            Transform parent;

            if (i < tasksPerColumn) parent = columns[0];
            else if (i < (tasksPerColumn * 2 + rest / 2)) parent = columns[1];
            else parent = columns[2];

            //if (i < displayedTasks.Count / 3) parent = columns[0];
            //else if (i <= Mathf.CeilToInt(displayedTasks.Count / 3 * 2)) parent = columns[1];
            //else parent = columns[2];

            clon.transform.SetParent(parent);

            i++;
        }
    }

    private void Update()
    {
        if (!GameManager.GetInstance().tutorialFinished)
        {
            displayedTasks = new List<Task>(tutorialTask);
        }
        else
        {
            //depending of the time of the day the displayed tasks are different
            if (GameManager.GetInstance().dayTime == GameManager.DayTime.Morning)
            {
                displayedTasks = new List<Task>(morningTasks);
            }
            else if (GameManager.GetInstance().dayTime == GameManager.DayTime.Evening)
            {
                displayedTasks = new List<Task>(eveningTasks);
            }
        }
    }

    /// <summary>
    /// check if max number of pinned task is reached.
    /// if so make the non pinned uninteractable
    /// else make them all interactable
    /// </summary>
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
                if(!completedTasks.Contains(cb.task) || !cb.task.inProgress)
                    toggle.interactable = true;
            }
        }

        AutoSizePinnedTasks();
    }

    /// <summary>
    /// when a task is completed added to the completed tasks list
    /// remove it from displayed tasks
    /// and destroy its checkbox
    /// </summary>
    public void TaskCompleted(Task task)
    {
        if (task.followingTask != null)
        {
            //task.FollowingTask(task, task.followingTask);
            task.FollowingTask(task.followingTask);
        }
        else
        {
            Task _task = task.GetFirstParent();

            completedTasks.Add(_task);
            //displayedTasks.Remove(task);

            UnpinTask(_task);
            foreach (CheckBox cb in checkboxes)
            {
                if (cb.task == _task)
                {
                    cb.pinImg.enabled = false;
                    cb.checkImg.enabled = true;

                    cb.GetComponent<Toggle>().interactable = false;
                    break;
                }
            }
        }
    }

    public void PinTask(Task task)
    {
        pinnedTasks.Add(task);
        currentNumberofTasksPinned++;
        GameObject clon = Instantiate(pinTaskPrefab, pinnedTasksPanel.transform);
        pinnedTaskGameObjects.Add(clon);
        clon.GetComponent<TextMeshProUGUI>().text = task.name;

        CheckTasksPinned(); 
    }

    public void UnpinTask(Task task)
    {
        pinnedTasks.Remove(task);
        currentNumberofTasksPinned--;
        foreach(GameObject go in pinnedTaskGameObjects)
        {
            TextMeshProUGUI text = go.GetComponent<TextMeshProUGUI>();
            if(text.text == task.name)
            {
                Destroy(go);
                pinnedTaskGameObjects.Remove(go);
                break;
            }
        }

        CheckTasksPinned();
    }

    void AutoSizePinnedTasks()
    {
        if (pinnedTasks.Count >= 3)
        {
            foreach (GameObject go in pinnedTaskGameObjects)
            {
                TextMeshProUGUI text = go.GetComponent<TextMeshProUGUI>();

                text.enableAutoSizing = true;
            }
        }
        else
        {
            foreach (GameObject go in pinnedTaskGameObjects)
            {
                TextMeshProUGUI text = go.GetComponent<TextMeshProUGUI>();

                text.enableAutoSizing = false;

                text.fontSize = 40;
            }
        }
    }
}
