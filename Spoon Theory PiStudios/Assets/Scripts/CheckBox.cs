using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckBox : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TaskManager taskManager;

    public Task task;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChange(bool value)
    {
        if (value)
        {
            taskManager.currentNumberofTasksPinned++;
        }
        else
        {
            taskManager.currentNumberofTasksPinned--;
        }

        task.outlineObject.enabled = value;

        taskManager.CheckTasksPinned();
    }
}