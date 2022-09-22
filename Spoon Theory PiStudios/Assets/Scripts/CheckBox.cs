using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckBox : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI text;
    [HideInInspector]
    public TaskManager taskManager;

    [HideInInspector]
    public Task task;
    public Image pinImg, checkImg;

    // Start is called before the first frame update
    void Start()
    {
        pinImg.enabled = false;
        checkImg.enabled = false;
    }

    public void OnChange(bool value)
    {
        if (value)
        {
            taskManager.PinTask(task);
            pinImg.enabled = true;
            checkImg.enabled = false;
        }
        else
        {
            taskManager.UnpinTask(task);
            pinImg.enabled = false;
            checkImg.enabled = false;
        }

        //commented bc of error as we don't have enough tasks with outline working. Do no delete, it works
        if(task.outlineObject != null) task.outlineObject.enabled = value;

        taskManager.CheckTasksPinned();
    }
}
