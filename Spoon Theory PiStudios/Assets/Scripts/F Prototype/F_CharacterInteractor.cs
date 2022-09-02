using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_CharacterInteractor : MonoBehaviour
{
    Camera cam;
    [SerializeField] LayerMask interactableMask;
    [SerializeField] PromptUI promptUI;

    [SerializeField] Transform interactionPoint;
    [SerializeField] float clickInteractionDistance = 5, FInteractionDistance = 3;

    [SerializeField] int numberOfSpoons;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        numberOfSpoons = Random.Range(10, 31);
    }

    // Update is called once per frame
    void Update()
    {
        //Click interaction

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    ClickInteract();
        //}

        Collider[] interactionHit = Physics.OverlapSphere(interactionPoint.position, FInteractionDistance, interactableMask, QueryTriggerInteraction.Collide);

        if (interactionHit.Length > 0)
        {
            F_IInteractable interactable = interactionHit[0].GetComponent<F_IInteractable>();

            if (interactable != null && Input.GetKeyDown(KeyCode.F))
            {
                interactable.Interact(this);
                promptUI.SetUpText(interactable.InteractionPrompt);
                numberOfSpoons -= interactable.SpoonsCost;
            }
        }

        promptUI.DisplaySpoons(numberOfSpoons);
    }


    private void ClickInteract()
    {
        //Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, clickInteractionDistance, interactableMask, QueryTriggerInteraction.Collide))
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

}
