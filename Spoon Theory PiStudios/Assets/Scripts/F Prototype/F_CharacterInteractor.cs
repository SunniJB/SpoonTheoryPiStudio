using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F_CharacterInteractor : MonoBehaviour
{
    Camera cam;
    [SerializeField] LayerMask interactableLayer, defaultLayer;
    [SerializeField] PromptUI promptUI;
    [SerializeField] Slider spoonSlider;

    [SerializeField] Transform interactionPoint;
    [SerializeField] float clickInteractionDistance = 5, FInteractionDistance = 3;

    CharacterMovement1stPerson characterMovement;

    [Header("Tasks")]
    [SerializeField] Image taskCanvas;
    [SerializeField] TaskManager taskManager;
    bool taskCanvasEnabled;

    int numberOfSpoons;

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
            characterMovement.canMove = !taskCanvasEnabled;

            if(taskCanvasEnabled) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }

        FInteraction();

        promptUI.DisplaySpoons(numberOfSpoons);

        UpdateSpoonSlider();
    }

    private void FInteraction()
    {
        Collider[] interactionHit = Physics.OverlapSphere(interactionPoint.position, FInteractionDistance, interactableLayer, QueryTriggerInteraction.Collide);

        if (interactionHit.Length > 0)
        {
            F_IInteractable interactable = interactionHit[0].GetComponent<F_IInteractable>();

            if (interactable != null && Input.GetKeyDown(KeyCode.F))
            {
                numberOfSpoons -= interactable.Interact(this);
                promptUI.SetUpText(interactable.InteractionPrompt);

                Outline outline = interactionHit[0].GetComponent<Outline>();

                if (outline == null)
                    return;

                outline.enabled = false;

                taskManager.TaskCompleted(interactable.Task);

                interactionHit[0].gameObject.layer = defaultLayer;
            }
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
            spoonSlider.value = 0;
            ZeroSpoons();
        }
        else
            spoonSlider.value = numberOfSpoons;
    }

    private void ZeroSpoons()
    {

    }


}
