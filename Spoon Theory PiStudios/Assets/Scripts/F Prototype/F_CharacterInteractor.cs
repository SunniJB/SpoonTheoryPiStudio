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

    int numberOfSpoons;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        numberOfSpoons = Random.Range(10, 31);
        spoonSlider.maxValue = spoonSlider.value = numberOfSpoons;
    }

    // Update is called once per frame
    void Update()
    {
        //Click interaction

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    ClickInteract();
        //}

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

                interactionHit[0].gameObject.layer = defaultLayer;
            }
        }
    }
    private void ClickInteract()
    {
        //Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, clickInteractionDistance, interactableLayer, QueryTriggerInteraction.Collide))
        {
            F_IInteractable interactable = raycastHit.transform.GetComponent<F_IInteractable>();

            if (interactable != null)
            {
                interactable.Interact(this);
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
