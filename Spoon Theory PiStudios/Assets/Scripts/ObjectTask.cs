using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTask : MonoBehaviour
{
    public string interactionPrompt;

    public bool finished, inProgress;

    public Task task;

    Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Interact(F_CharacterInteractor interactor)
    {
        StartCoroutine(Progress(interactor));
    }

    IEnumerator Progress(F_CharacterInteractor interactor)
    {
        inProgress = true;
        int spoonsTaken = 0;

        while (Input.GetKey(KeyCode.F) && spoonsTaken < task.spoonCost && Vector3.Distance(interactor.transform.position, transform.position) <= interactor.FInteractionDistance)
        {
            interactor.numberOfSpoons--;
            spoonsTaken++;

            //Check if you have run out of spoons in mid task
            if(interactor.numberOfSpoons <= 0)
            {
                interactor.ZeroSpoons();
                StopCoroutine(Progress(interactor));
                break;
            }

            yield return new WaitForSeconds(1);
        }

        //Finished task

        interactor.hygiene -= task.hygieneCost;
        interactor.hunger -= task.hungerCost;
        interactor.happiness -= task.happinesCost;
        interactor.workPerformance -= task.workPerformanceCost;

        finished = true;
        outline.enabled = false;
    }
}
