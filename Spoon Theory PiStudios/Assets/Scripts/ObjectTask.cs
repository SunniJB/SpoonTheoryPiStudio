using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class ObjectTask : MonoBehaviour
{
    public string interactionPromptLowSpoons, interactionPromptHighSpoons, audioName;

    public Task task;
    public GameObject finishedState;

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

    [SerializeField] AnimationClip actionAnim;
    private Animator objectAnimator;

    float animationSpeed;

    int _spoonCostPositive;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

        task.objectTask = this;
        task.outlineObject = outline;
        task.inProgress = false;

        if (gameObject.TryGetComponent<Animator>(out Animator animator))
        {
            objectAnimator = animator;
        }
        if (gameObject.transform.Find("finishedState"))
        {
            finishedState = gameObject.transform.Find("finishedState").gameObject;
            finishedState.SetActive(false);
        }

        if (objectAnimator != null)
        {
            AnimationClip[] clips;
            clips = objectAnimator.runtimeAnimatorController.animationClips;

            foreach (AnimationClip clip in clips)
            {
                if (clip.name == actionAnim.name)
                {
                    if (task.spoonCost < 0)
                    {
                        animationSpeed = clip.length / (task.spoonCost * -1);
                    }
                    else
                    {
                        animationSpeed = clip.length / task.spoonCost;
                    }
                    break;
                }
            }
        }
    }

    private void Update()
    {
        if (interactor == null) return;

        if (task.inProgress && !finished)
        {
            Progress();
        }
    }

    public void Interact(CharacterInteractor _interactor)
    {
        interactor = _interactor;

        if (interactor.doingTask) return;

        interactor.doingTask = true;
        task.inProgress = true;
        _spoonCostPositive = Mathf.Abs(task.spoonCost);
        GameObject clon = Instantiate(progressSliderPrefab, sliderPos);
        progressSlider = clon.GetComponent<Slider>();
        progressSlider.maxValue = _spoonCostPositive;
        progressSlider.value = 0;

        if (objectAnimator != null) objectAnimator.SetTrigger("play");

        AudioManager.GetInstance().Play(audioName, 1f);
    }

    void Progress()
    {
        if (interactor.numberOfSpoons <= 0 && task.spoonCost > 0)
        {
            interactor.NoSpoonsForTask();
            return;
        }

        if (spoonsTaken >= _spoonCostPositive)
        {
            Finish();
            return;
        }

        if (Input.GetKey(KeyCode.F) && Vector3.Distance(interactor.transform.position, transform.position) <= interactor.FInteractionDistance + 2)
        {
            if (!AudioManager.GetInstance().CheckPlaying(audioName)) AudioManager.GetInstance().Resume(audioName);
         
            if (objectAnimator != null) objectAnimator.speed = animationSpeed;

            timer -= Time.deltaTime;

            progressSlider.value += Time.deltaTime;

            if(timer <= 0)
            {
                if (task.spoonCost < 0)
                {
                    interactor.numberOfSpoons++;
                    if(interactor.lowSpoons) interactor.CheckIfStillLowSpoons();
                    if(interactor.halfSpoons) interactor.CheckIfStillHalfSpoons();
                }
                else interactor.numberOfSpoons--;

                spoonsTaken++;

                //Check if you have run out of spoons in mid task
                if (interactor.numberOfSpoons <= 0) interactor.ZeroSpoons();

                timer = 1;
            }
        }
        else
        {
            if (objectAnimator != null) objectAnimator.speed = 0f;

            AudioManager.GetInstance().Pause(audioName);
        }
    }

    public void Finish()
    {
        if (!GameManager.GetInstance().tutorialFinished && task.taskName == "Tutorial Task") TutorialManager.GetInstance().completeTaskFinished = true;

        //Finished task
        interactor.hygiene -= task.hygieneCost;
        interactor.hunger -= task.hungerCost;
        interactor.happiness -= task.happinessCost;
        interactor.workPerformance -= task.workPerformanceCost;
        interactor.money -= task.moneyCost;

        Destroy(progressSlider.gameObject);

        interactor.FinishTask(task, gameObject);
        if (gameObject.transform.Find("finishedState"))
        { finishedState.SetActive(true); }
        
        AudioManager.GetInstance().Stop(audioName);
        AudioManager.GetInstance().Play("success");

        finished = true;
        outline.enabled = false;
    }
}
