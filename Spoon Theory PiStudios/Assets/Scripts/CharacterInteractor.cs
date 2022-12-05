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
    [SerializeField] Image avatar;
    public Sprite fullSpoonsAvatar;
    public Sprite highSpoonsAvatar;
    public Sprite halfSpoonsAvatar;
    public Sprite lowSpoonsAvatar;

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
    public int maxNumberOfSpoons = 25;
    public float hygiene;
    public float happiness;
    public float hunger;
    public float money;
    public float workPerformance;
    public int dayCount;
    public bool hasSleptToday, hasWorkedToday; //Is set to false by the level manager when you go to work, is set to false when you're low on spoons, is set to true by SleepInBed when you go to sleep.
    public bool doingTask;
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

    [Header("3D VIEWER")]
    [SerializeField] GameObject viewerPanel;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DragRotation dragRotation;
    GameObject spawnedObject;
    public bool inspecting;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement1stPerson>();

        RefreshStatsFromManager();

        spoonSlider.maxValue = maxNumberOfSpoons;
        hygieneSlider.maxValue = 10;
        hungerSlider.maxValue = 10;
        happinessSlider.maxValue = 20;

        spoonSlider.value = numberOfSpoons;
        hygieneSlider.value = hygiene;
        hungerSlider.value = hunger;
        happinessSlider.value = happiness;

        if (spoonSlider.value / spoonSlider.maxValue <= 0.25f) avatar.sprite = lowSpoonsAvatar;
        else if (spoonSlider.value / spoonSlider.maxValue <= 0.5f) avatar.sprite = halfSpoonsAvatar;
        else if (spoonSlider.value / spoonSlider.maxValue <= 0.75f) avatar.sprite = highSpoonsAvatar;
        else avatar.sprite = fullSpoonsAvatar;
    }
    // Start is called before the first frame update
    void Start()
    {
        hasSleptToday = true;
        taskCanvas.gameObject.SetActive(false);
        viewerPanel.SetActive(false);
        inspecting = false;
        doingTask = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfSpoons < 5)
        {
            hasSleptToday = false;
        }

        ToggleTaskMenu();

        FInteraction();

        UpdateStatSliders();

        UpdateStatsModifiers();

        if (!halfSpoons && spoonSlider.value / spoonSlider.maxValue <= 0.5f)
        {
            HalfSpoons();
        }

        if (!lowSpoons && spoonSlider.value / spoonSlider.maxValue <= 0.25f)
        {
            LowSpoons();
        }

        if (spoonSlider.value / spoonSlider.maxValue <= 0.25f) avatar.sprite = lowSpoonsAvatar;
        else if (spoonSlider.value / spoonSlider.maxValue <= 0.5f) avatar.sprite = halfSpoonsAvatar;
        else if (spoonSlider.value / spoonSlider.maxValue <= 0.75f) avatar.sprite = highSpoonsAvatar;
        else avatar.sprite = fullSpoonsAvatar;

        if (AudioManager.GetInstance().CheckPlaying(femaleBreathingSound))
        {
            AudioManager.GetInstance().SoundVolume(femaleBreathingSound, breathingVolume.Evaluate(spoonSlider.value / spoonSlider.maxValue) * PlayerPrefs.GetFloat("SoundVolume"));
        }
    }

    void ToggleTaskMenu()
    {
        //if T pressed toggle task canvas and character movement
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (TutorialManager.GetInstance() != null && TutorialManager.GetInstance().tutorialStates == TutorialManager.TutorialStates.Start) return;

            if (TutorialManager.GetInstance() == null && (levelManager.pause || levelManager.shopPanelEnabled)) return;

            if (GameManager.GetInstance().ActualScene() != "ChloesApartment" && GameManager.GetInstance().ActualScene() != "TutorialScene" && GameManager.GetInstance().ActualScene() != "IsaacsApartment") return;

            if (inspecting) return;

            taskCanvasEnabled = !taskCanvasEnabled;
            taskCanvas.gameObject.SetActive(taskCanvasEnabled);
            taskManager.pinnedTasksPanel.gameObject.SetActive(!taskCanvasEnabled);
            characterMovement.canMove = !taskCanvasEnabled;
            characterMovement.moving = !taskCanvasEnabled;
            UIPanel.SetActive(!taskCanvasEnabled);

            if (taskCanvasEnabled) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void FInteraction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (inspecting) return;

            Collider[] interactionHit = Physics.OverlapSphere(interactionPoint.position, FInteractionDistance, interactableLayer, QueryTriggerInteraction.Collide);

            if (interactionHit.Length <= 0) return;

            ObjectTask[] interactableObject = new ObjectTask[interactionHit.Length];

            int j = 0;

            for (int i = 0; i < interactionHit.Length; i++)
            {
                interactableObject[j] = interactionHit[i].GetComponent<ObjectTask>();

                if (interactableObject[j] != null && interactableObject[j].outline.enabled && !interactableObject[j].task.inProgress && !interactableObject[j].finished)
                {
                    if (halfSpoons) promptUI.SetUpText(interactableObject[j].interactionPromptLowSpoons);
                    else promptUI.SetUpText(interactableObject[j].interactionPromptHighSpoons);

                    interactableObject[j].Interact(this);

                    j++;
                    continue;
                }

                opencloseDoor opencloseDoor = interactionHit[i].GetComponent<opencloseDoor>();

                if (opencloseDoor != null && ((GameManager.GetInstance().dayTime == GameManager.DayTime.Morning && numberOfSpoons >= 5) || (GameManager.GetInstance().ActualScene() == "RestaurantScene" || GameManager.GetInstance().ActualScene() == "IsaacWorkScene")))
                {
                    if (TutorialManager.GetInstance() != null && TutorialManager.GetInstance().tutorialStates != TutorialManager.TutorialStates.Finish) continue;

                    opencloseDoor.OpenCloseDoor(this);
                    continue;
                    //GameManager.Instance.WorkScene();
                }

                InspectableObject inspectableObject = interactionHit[i].GetComponent<InspectableObject>();

                if (inspectableObject != null)
                {
                    viewerPanel.SetActive(true);
                    inspecting = true;

                    Cursor.lockState = CursorLockMode.None;

                    spawnedObject = Instantiate(inspectableObject.gameObject, spawnPoint);
                    spawnedObject.transform.position = spawnPoint.transform.position;
                    spawnedObject.transform.rotation = Quaternion.identity;
                    spawnedObject.transform.localScale = new Vector3(inspectableObject.viewerScale, inspectableObject.viewerScale, inspectableObject.viewerScale);
                    dragRotation.objectToRotate = spawnedObject.transform;
                    dragRotation.prompt.text = inspectableObject.promptText;

                    characterMovement.canMove = false;
                    characterMovement.moving = false;
                    continue;
                }

                SleepInBed sleepInBed = interactionHit[i].GetComponent<SleepInBed>();

                if (sleepInBed != null)
                {
                    if (!hasSleptToday)
                    {
                        if (TutorialManager.GetInstance() != null && TutorialManager.GetInstance().tutorialStates != TutorialManager.TutorialStates.Finish) continue;

                        sleepInBed.GoToSleep();
                        //GameManager.Instance.WorkScene();
                    }
                    else promptUI.SetUpText("I can't go to sleep yet.");
                    continue;
                }

                CheckCalendar checkCalendar = interactionHit[i].GetComponent<CheckCalendar>();

                if (checkCalendar != null)
                {
                    if (TutorialManager.GetInstance() != null && TutorialManager.GetInstance().tutorialStates != TutorialManager.TutorialStates.Finish) continue;

                    checkCalendar.CheckGoal();
                    continue;
                }

                MinigameEnvironment minigameEnvironment = interactionHit[i].GetComponent<MinigameEnvironment>();

                if (minigameEnvironment != null)
                {
                    minigameEnvironment.GoToScene();
                    continue;
                }
            }

            //bed.GetComponent<SleepInBed>().tasks = interactableObject; Trying very hard here     
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, FInteractionDistance);
    }

    public void FinishTask(Task task, GameObject interactableObject)
    {
        if(hunger <= 3f || hygiene <= 3f) happiness--;

        promptUI.Close();

        taskManager.TaskCompleted(task);

        doingTask = false;

        interactableObject.layer = defaultLayer;

        Debug.Log(task.name + " finished");
    }

    public void ZeroSpoons()
    {
        Debug.Log("You ran out of spoons");
    }

    public void FinishInspecting()
    {
        inspecting = false;
        viewerPanel.SetActive(false);
        characterMovement.canMove = true;
        dragRotation.yaw = 0;
        dragRotation.pitch = 0;
        Destroy(dragRotation.objectToRotate.gameObject);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UpdateStatSliders()
    {
        if (numberOfSpoons < 0)
        {
            numberOfSpoons = 0;
            ZeroSpoons();
        }

        hygiene = Mathf.Clamp(hygiene, 0, 10);
        hunger = Mathf.Clamp(hunger, 0, 10);
        happiness = Mathf.Clamp(happiness, 0, 20);

        spoonSlider.value = numberOfSpoons;
        hygieneSlider.value = hygiene;
        hungerSlider.value = hunger;
        happinessSlider.value = happiness + (hygiene + hunger) / 2;

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
        GameManager gm = GameManager.GetInstance();

        money = gm.money;
        numberOfSpoons = gm.spoons;
        hygiene = gm.hygiene;
        workPerformance = gm.workPerformance;
        hunger = gm.hunger;
        happiness = gm.happiness;
        dayCount = gm.dayCount;
    }

    public void UpdateGameManagerStats()
    {
        GameManager.GetInstance().UpdateGameManagerStats(money, hunger, numberOfSpoons, hygiene, happiness, workPerformance);
    }

    void HalfSpoons()
    {
        happiness--;

        halfSpoons = true;
    }
    public void CheckIfStillHalfSpoons()
    {
        if (spoonSlider.value / spoonSlider.maxValue > 0.5f) halfSpoons = false;
    }
    void LowSpoons()
    {
        happiness -= 2;

        lowSpoons = true;

        AudioManager.GetInstance().Play(femaleBreathingSound);
        

        if (hasSleptToday == false)
        {
            promptUI.SetUpText("I'm getting tired. I guess I could sleep.");
        } else
        {
            promptUI.SetUpText("I'm so tired. I'm sarting to feel dizzy.");
        }

    }
    public void CheckIfStillLowSpoons()
    {
        if (spoonSlider.value / spoonSlider.maxValue > 0.25f) lowSpoons = false;
        AudioManager.GetInstance().Stop(femaleBreathingSound);
    }

    public void NoSpoonsForTask()
    {
        promptUI.SetUpText("I am too tired to finish this now.");
    }
}
