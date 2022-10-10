using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInteractor : MonoBehaviour
{
    Camera cam;
    [SerializeField] LayerMask interactableLayer, defaultLayer;

    [Header("UI Stats")]
    public PromptUI promptUI;
    [SerializeField] Slider spoonSlider;
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

    private void Awake()
    {
        cam = Camera.main;
        characterMovement = GetComponent<CharacterMovement1stPerson>();
    }
    // Start is called before the first frame update
    void Start()
    {
        numberOfSpoons = Random.Range(10, 31);
        spoonSlider.maxValue = spoonSlider.value = numberOfSpoons;

        taskCanvas.gameObject.SetActive(false);
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

        if (sleepInBed != null && Input.GetKeyDown(KeyCode.F))
        {
            if (TutorialManager.GetInstance() != null && TutorialManager.GetInstance().tutorialStates != TutorialManager.TutorialStates.Finish) return;
            sleepInBed.GoToSleep();
            //GameManager.Instance.WorkScene();
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
        hygieneSlider.value = Mathf.Clamp((float)hygiene / 10, 0, 1);
        hungerSlider.value = Mathf.Clamp((float)hunger / 10, 0, 1);
        happinessSlider.value = Mathf.Clamp((((float)hygiene + (float)hunger) / 2 + (float)happiness) / 20, 0, 1);

        MoneyText.text = "£" + money.ToString("000");
        day.text = "Day: " + (1 + dayCount).ToString();
        timeOfDay.text = GameManager.GetInstance().dayTime.ToString();
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
    }
}
