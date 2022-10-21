using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInteractor : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayer, defaultLayer;

    [Header("UI Stats")]
    public PromptUI promptUI;
    public Slider spoonSlider;
    [SerializeField] Slider hygieneSlider;
    [SerializeField] Slider hungerSlider;
    [SerializeField] Slider happinessSlider;
    [SerializeField] TMP_Text MoneyText;
    [SerializeField] TMP_Text day;
    [SerializeField] TMP_Text timeOfDay;

    [SerializeField] Transform interactionPoint;
    public float clickInteractionDistance = 5, FInteractionDistance = 3;

    CharacterMovement1stPerson characterMovement;
    public GameObject bed;

    [Header("Tasks")]
    [SerializeField] Image taskCanvas;
    [SerializeField] GameObject UIPanel;
    [SerializeField] TaskManager taskManager;
    public bool taskCanvasEnabled;

    [Header("Player stats")]
    public int numberOfSpoons;
    public float hygiene;
    public float happiness;
    public float hunger;
    public float money;
    public float workPerformance;
    public int dayCount;
    public bool hasSleptToday; //Is set to false by the level manager when you go to work, is set to false when you're low on spoons, is set to true by SleepInBed when you go to sleep.

    [Header("Stats modifiers")]
    [SerializeField] AnimationCurve speedMultiplierCurve;
    [HideInInspector] public float speedMultiplier;
    [SerializeField] AnimationCurve headbobbingCurve;
    [HideInInspector] public float headBobbingMultiplier;
    [SerializeField] AnimationCurve vignetteCurve;
    [SerializeField] AnimationCurve caCurve;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement1stPerson>();
    }
    // Start is called before the first frame update
    void Start()
    {
        spoonSlider.maxValue =  numberOfSpoons;
        spoonSlider.value = numberOfSpoons;
        hasSleptToday = true;
        taskCanvas.gameObject.SetActive(false);

        StartCoroutine(HalfSpoons());
        StartCoroutine(LowSpoons());
    }

    // Update is called once per frame
    void Update()
    {
        //if T pressed toggle task canvas and character movement
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (TutorialManager.GetInstance() != null && TutorialManager.GetInstance().tutorialStates == TutorialManager.TutorialStates.Start) return;

            taskCanvasEnabled = !taskCanvasEnabled;
            taskCanvas.gameObject.SetActive(taskCanvasEnabled);
            taskManager.pinnedTasksPanel.gameObject.SetActive(!taskCanvasEnabled);
            characterMovement.canMove = !taskCanvasEnabled;
            UIPanel.SetActive(!taskCanvasEnabled);

            if(taskCanvasEnabled) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }

        FInteraction();

        UpdateSpoonSlider();

        UpdateStatSliders();

        UpdateStatsModifiers();

        if (numberOfSpoons < 4)
        {
            hasSleptToday = false;
            promptUI.SetUpText("I'm getting tired. I guess I could sleep.");
        }
    }

    private void FInteraction()
    {
        Collider[] interactionHit = Physics.OverlapSphere(interactionPoint.position, FInteractionDistance, interactableLayer, QueryTriggerInteraction.Collide);

        if (interactionHit.Length <= 0) return;

        ObjectTask[] interactableObject = new ObjectTask[interactionHit.Length];

        //bed.GetComponent<SleepInBed>().tasks = interactableObject; Trying very hard here

        for (int i = 0; i < interactionHit.Length; i++)
        {
            interactableObject[i] = interactionHit[i].GetComponent<ObjectTask>();

            if (interactableObject[i] != null && Input.GetKeyDown(KeyCode.F) && interactableObject[i].outline.enabled && !interactableObject[i].task.inProgress && !interactableObject[i].finished)
            {
                if (numberOfSpoons >= numberOfSpoons / 2)
                {
                    promptUI.SetUpText(interactableObject[i].interactionPromptLowSpoons);
                } else
                {
                    promptUI.SetUpText(interactableObject[i].interactionPromptHighSpoons);
                }

                interactableObject[i].Interact(this);

                break;
            }
        }

        opencloseDoor opencloseDoor = interactionHit[0].GetComponent<opencloseDoor>();

        if (opencloseDoor != null && Input.GetKeyDown(KeyCode.F))
        {
            if (TutorialManager.GetInstance() != null && TutorialManager.GetInstance().tutorialStates != TutorialManager.TutorialStates.Finish) return;
            opencloseDoor.OpenCloseDoor();
            //GameManager.Instance.WorkScene();
        }

        
        SleepInBed sleepInBed = interactionHit[0].GetComponent<SleepInBed>();

        if (sleepInBed != null && Input.GetKeyDown(KeyCode.F) && !hasSleptToday)
        {
            if (TutorialManager.GetInstance() != null && TutorialManager.GetInstance().tutorialStates != TutorialManager.TutorialStates.Finish) return;
            sleepInBed.GoToSleep();
            //GameManager.Instance.WorkScene();
        } else if (sleepInBed != null && Input.GetKeyDown(KeyCode.F) && hasSleptToday)
        {
            promptUI.SetUpText("I can't go to sleep yet.");
        }


    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, FInteractionDistance);
    }

    private void UpdateSpoonSlider()
    {
        if (numberOfSpoons < 0)
        {
            numberOfSpoons = 0;
            ZeroSpoons();
        }
        
        spoonSlider.value = numberOfSpoons;
    }

    public void FinishTask(Task task, GameObject interactableObject)
    {
        promptUI.Close();

        taskManager.TaskCompleted(task);

        interactableObject.layer = defaultLayer;

        Debug.Log(task.name + " finished");
    }

    public void ZeroSpoons()
    {
        Debug.Log("You ran out of spoons");
    }

    private void UpdateStatSliders()
    {
        hygieneSlider.value = (float)hygiene / 10;
        hungerSlider.value = (float)hunger / 10;
        happinessSlider.value = (((float)hygiene + (float)hunger) / 2 + (float)happiness) / 20;

        MoneyText.text = "�" + money.ToString("000");
        if (day != null)
        {
            day.text = "Day: " + (1 + dayCount).ToString();
        }

        timeOfDay.text = GameManager.GetInstance().dayTime.ToString();
    }

    void UpdateStatsModifiers()
    {
        speedMultiplier = speedMultiplierCurve.Evaluate(spoonSlider.value / spoonSlider.maxValue);
        headBobbingMultiplier = headbobbingCurve.Evaluate(spoonSlider.value / spoonSlider.maxValue);

        PostProcessingManager.GetInstance().VignetteValues(vignetteCurve.Evaluate(spoonSlider.value / spoonSlider.maxValue), Color.black);
        PostProcessingManager.GetInstance().ChromaticAberrationValues(caCurve.Evaluate(spoonSlider.value / spoonSlider.maxValue));
    }

    public void RefreshStatsFromManager()
    {
        money = GameManager.GetInstance().money;
        numberOfSpoons = GameManager.GetInstance().spoons;
        hygiene = GameManager.GetInstance().hygiene;
        workPerformance = GameManager.GetInstance().workPerformance;
        hunger = GameManager.GetInstance().hunger;
        happiness = GameManager.GetInstance().happiness;
        dayCount = GameManager.GetInstance().dayCount;

        if (hygiene < 0) 
            hygiene = 0;
        if (hunger < 0)
            hunger = 0;
    }

    IEnumerator HalfSpoons()
    {
        yield return new WaitUntil(() => spoonSlider.value / spoonSlider.maxValue < 0.5f);

    }
    IEnumerator LowSpoons()
    {
        yield return new WaitUntil(() => spoonSlider.value / spoonSlider.maxValue < 0.25f);

    }
}
