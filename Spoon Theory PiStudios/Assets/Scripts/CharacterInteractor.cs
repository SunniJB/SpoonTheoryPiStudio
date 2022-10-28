using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInteractor : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayer, defaultLayer;
    [SerializeField] LevelManager levelManager;

    [Header("UI STATS")]
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

    [HideInInspector] public CharacterMovement1stPerson characterMovement;
    public GameObject bed;

    [Header("TASKS")]
    [SerializeField] Image taskCanvas;
    [SerializeField] GameObject UIPanel;
    [SerializeField] TaskManager taskManager;
    public bool taskCanvasEnabled;

    [Header("PLAYER STATS")]
    public int numberOfSpoons;
    public float hygiene;
    public float happiness;
    public float hunger;
    public float money;
    public float workPerformance;
    public int dayCount;
    public bool hasSleptToday; //Is set to false by the level manager when you go to work, is set to false when you're low on spoons, is set to true by SleepInBed when you go to sleep.
    [HideInInspector] public bool halfSpoons, lowSpoons;

    [Header("STATS MODIFIERS")]
    [SerializeField] AnimationCurve speedMultiplierCurve;
    [HideInInspector] public float speedMultiplier;
    [SerializeField] AnimationCurve headbobbingCurve;
    [HideInInspector] public float headBobbingMultiplier;
    [SerializeField] AnimationCurve vignetteCurve;
    [SerializeField] AnimationCurve caCurve;

    [Header("PLAYER SOUNDS")]
    [SerializeField] string femaleBreathingSound;
    [SerializeField] AnimationCurve breathingVolume;
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
    }

    // Update is called once per frame
    void Update()
    {
        //if T pressed toggle task canvas and character movement
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (TutorialManager.GetInstance() != null && TutorialManager.GetInstance().tutorialStates == TutorialManager.TutorialStates.Start) return;

            if (TutorialManager.GetInstance() == null && (levelManager.pause || levelManager.shopPanelEnabled)) return;

            taskCanvasEnabled = !taskCanvasEnabled;
            taskCanvas.gameObject.SetActive(taskCanvasEnabled);
            taskManager.pinnedTasksPanel.gameObject.SetActive(!taskCanvasEnabled);
            characterMovement.canMove = !taskCanvasEnabled;
            characterMovement.moving = !taskCanvasEnabled;
            UIPanel.SetActive(!taskCanvasEnabled);

            if(taskCanvasEnabled) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }

        FInteraction();

        UpdateSpoonSlider();

        UpdateStatSliders();

        UpdateStatsModifiers();

        if(!halfSpoons && spoonSlider.value / spoonSlider.maxValue <= 0.5f)
        {
            HalfSpoons();
        }

        if(!lowSpoons && spoonSlider.value / spoonSlider.maxValue <= 0.25f)
        {
            LowSpoons();
        }

        /*if (AudioManager.GetInstance().CheckPlaying(femaleBreathingSound)) */AudioManager.GetInstance().SoundVolume(femaleBreathingSound, breathingVolume.Evaluate(spoonSlider.value / spoonSlider.maxValue));
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

        if (opencloseDoor != null && Input.GetKeyDown(KeyCode.F) && GameManager.GetInstance().dayTime == GameManager.DayTime.Morning)
        {
            if (TutorialManager.GetInstance() != null && TutorialManager.GetInstance().tutorialStates != TutorialManager.TutorialStates.Finish) return;

            if (TutorialManager.GetInstance() == null && numberOfSpoons < 5) return;
            
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

        CheckCalendar checkCalendar = interactionHit[0].GetComponent<CheckCalendar>();

        if (checkCalendar != null && Input.GetKeyDown(KeyCode.F))
        {
            if (TutorialManager.GetInstance() != null && TutorialManager.GetInstance().tutorialStates != TutorialManager.TutorialStates.Finish) return;

            checkCalendar.CheckGoal();
        }

        MinigameEnvironment minigameEnvironment = interactionHit[0].GetComponent<MinigameEnvironment>();

        if (minigameEnvironment != null && Input.GetKeyDown(KeyCode.F))
        {
            minigameEnvironment.GoToScene();
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

        MoneyText.text = "£" + money.ToString("000");
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

    void HalfSpoons()
    {
        halfSpoons = true;
        Debug.Log("half spoons");
    }
    public void CheckIfStillHalfSpoons()
    {
        if (spoonSlider.value / spoonSlider.maxValue > 0.5f) halfSpoons = false;
    }
    void LowSpoons()
    {
        Debug.Log("low spoons");

        lowSpoons = true;

        AudioManager.GetInstance().Play(femaleBreathingSound);

        hasSleptToday = false;
        promptUI.SetUpText("I'm getting tired. I guess I could sleep.");
    }
    public void CheckIfStillLowSpoons()
    {
        if (spoonSlider.value / spoonSlider.maxValue > 0.25f) lowSpoons = false;
        AudioManager.GetInstance().Stop(femaleBreathingSound);
    }
}
