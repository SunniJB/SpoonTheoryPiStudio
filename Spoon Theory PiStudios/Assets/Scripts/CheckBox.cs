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
    public string audioName;

    string taskName;

    // Start is called before the first frame update
    void Start()
    {
        pinImg.enabled = false;
        checkImg.enabled = false;
    }

    public void OnChange(bool value)
    {
        if (task.inProgress || task.objectTask.finished) return;

        if (task.moneyCost > taskManager.interactor.money)
        {
            taskName = task.taskName;
            text.text = "You don't have enough money";
            Invoke("ReturnTextToTaskName", 1f);
            return;
        }

        PlayCheckSound();

        if (value)
        {
            if (TutorialManager.GetInstance() != null)
                if(task.taskName == "Tutorial Task") TutorialManager.GetInstance().taskmenuFinished = true;

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

        if(task.outlineObject != null) task.outlineObject.enabled = value;
    }

    void ReturnTextToTaskName()
    {
        text.text = taskName;
    }

    void PlayCheckSound()
    {
        AudioManager.GetInstance().Play(audioName, 1f);
    }
}
