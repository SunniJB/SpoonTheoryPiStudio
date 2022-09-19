using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Mirror : MonoBehaviour, F_IInteractable //In inheritance, the objects are the same "class." In interfaces, they just have to share a characteristic
{
    public bool Interacted => false;

    [SerializeField] private string _prompt; //In private variables, it is common practice to preface the name with an underscore

    [SerializeField] Task _task;
    public Task Task => _task;  //When working with interfaces, you have to use a lambda operator to assign a value. So it has to be => instead of =.

    public string InteractionPrompt => Task.description;

    private void Awake()
    {
        Task.outlineObject = GetComponent<Outline>();
        Task.outlineObject.enabled = false;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void InProgress(F_CharacterInteractor interactor)
    {
        StartCoroutine(Progress(interactor));
    }
    public IEnumerator Progress(F_CharacterInteractor interactor)
    {
        bool finished = false;
        float timer = Task.spoonCost;
        float timerSec = 1;

        while (!finished && Input.GetKey(KeyCode.F))
        {
            timer -= Time.deltaTime;
            timerSec -= Time.deltaTime;

            if(timerSec <= 0)
            {
                timerSec = 1;
                interactor.numberOfSpoons--;
            }

            if(timer <= 0) finished = true;

            //update ui to show progress

            yield return 0;
        }

        if (finished)
        {
            Finished(interactor);
        }
    }

    public void Finished(F_CharacterInteractor interactor)
    {
        interactor.FinishTask(this, gameObject);
    }


}
