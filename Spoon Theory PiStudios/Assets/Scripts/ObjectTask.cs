using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class ObjectTask : MonoBehaviour
{
    public string interactionPrompt;

    public Task task;

    [HideInInspector]
    public Outline outline;

    CharacterInteractor interactor;
    Slider progressSlider;

    int spoonsTaken = 0;
    float timer = 1;

    [HideInInspector]
    public bool finished = false;

    [SerializeField] GameObject progressSliderPrefab;
    [SerializeField] Transform sliderPos;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

        task.objectTask = this;
        task.outlineObject = outline;
        task.inProgress = false;
    }

    private void Update()
    {
        if (interactor == null) return;

        if(task.inProgress && !finished)
        {
            Progress();
        }
    }

    public void Interact(CharacterInteractor _interactor)
    {
        if (task.spoonCost < 0) task.spoonCost = task.spoonCost * -1;

        task.inProgress = true;
        interactor = _interactor;
        GameObject clon = Instantiate(progressSliderPrefab, sliderPos);
        progressSlider = clon.GetComponent<Slider>();
        progressSlider.maxValue = task.spoonCost;
        progressSlider.value = 0;
    }

    void Progress()
    {
        if (interactor.numberOfSpoons <= 0) return;

        if(spoonsTaken >= task.spoonCost)
        {
            //Finished task
            interactor.hygiene -= task.hygieneCost;
            interactor.hunger -= task.hungerCost;
            interactor.happiness -= task.happinesCost;
            interactor.workPerformance -= task.workPerformanceCost;

            Destroy(progressSlider.gameObject);

            interactor.FinishTask(task, gameObject);

            finished = true;
            outline.enabled = false;

            return;
        }


        if (Input.GetKey(KeyCode.F) && Vector3.Distance(interactor.transform.position, transform.position) <= interactor.FInteractionDistance + 2)
        {
            Debug.Log(task.name + " in progress");

            timer -= Time.deltaTime;

            progressSlider.value += Time.deltaTime;

            if(timer <= 0)
            {
                interactor.numberOfSpoons--;
                spoonsTaken++;

                //Check if you have run out of spoons in mid task
                if (interactor.numberOfSpoons <= 0) interactor.ZeroSpoons();

                timer = 1;
            }
        }
    }
}
