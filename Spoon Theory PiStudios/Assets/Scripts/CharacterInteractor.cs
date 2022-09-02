using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractor : MonoBehaviour
{
    Camera cam;
    [SerializeField] LayerMask interactableMask;

    [SerializeField] Transform interactionPoint;
    [SerializeField] float clickInteractionDistance = 5, FInteractionDistance = 3;
    bool canInteract;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ClickInteract();
        }

        Collider[] interactionHit = Physics.OverlapSphere(interactionPoint.position, FInteractionDistance, interactableMask, QueryTriggerInteraction.Collide);

        if (interactionHit.Length > 0)
        {
            IInteractable interactable = interactionHit[0].GetComponent<IInteractable>();

            if (interactable != null && Input.GetKeyDown(KeyCode.F))
            {
                interactable.Interact(this);
            }
        }
    }


    private void ClickInteract()
    {
        //Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, clickInteractionDistance, interactableMask, QueryTriggerInteraction.Collide))
        {
            IInteractable interactable = raycastHit.transform.GetComponent<IInteractable>();

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
