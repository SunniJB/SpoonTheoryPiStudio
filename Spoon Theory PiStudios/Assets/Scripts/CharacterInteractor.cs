using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] Transform interactionPoint;
    public float clickInteractionDistance = 5, FInteractionDistance = 3;

    CharacterMovement1stPerson characterMovement;

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

        for (int i = 0; i < interactionHit.Length; i++)
        {
            interactableObject[i] = interactionHit[i].GetComponent<ObjectTask>();

            if (interactableObject[i] != null && Input.GetKeyDown(KeyCode.F) && interactableObject[i].outline.enabled && !interactableObject[i].task.inProgress && !interactableObject[i].finished)
            {
                promptUI.SetUpText(interactableObject[i].interactionPrompt);

                interactableObject[i].Interact(this);

                break;
            }
        }

        opencloseDoor opencloseDoor = interactionHit[0].GetComponent<opencloseDoor>();

        if (opencloseDoor != null && Input.GetKeyDown(KeyCode.F))
        {
            opencloseDoor.OpenCloseDoor();
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
        hygieneSlider.value = (float)hygiene / 10;
        hungerSlider.value = (float)hunger / 10;
        happinessSlider.value = (((float)hygiene + (float)hunger) / 2 + (float)happiness) / 20;
    }

    public void RefreshStatsFromManager()
    {
        money = GameManager.Instance.money;
        numberOfSpoons = GameManager.Instance.spoons;
        hygiene = GameManager.Instance.hygiene;
        workPerformance = GameManager.Instance.workPerformance;
        hunger = GameManager.Instance.hunger;
        happiness = GameManager.Instance.happiness;
    }
}
