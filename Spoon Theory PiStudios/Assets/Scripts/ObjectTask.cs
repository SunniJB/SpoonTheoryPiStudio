using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTask : MonoBehaviour
{
    public string interactionPrompt;

    public bool finished;

    public Task task;

    [HideInInspector]
    public Outline outline;

    F_CharacterInteractor interactor;
    Slider progressSlider;

    int spoonsTaken = 0;
    float timer = 1;

    [SerializeField] GameObject progressSliderPrefab;
    [SerializeField] Transform sliderPos;
    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
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

    public void Interact(F_CharacterInteractor _interactor)
    {
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
