using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface F_IInteractable
{

    public string InteractionPrompt { get ; }

    public bool Finished { get; set; }

    public bool InProgress { get; set; }
    public Task Task { get; }

    public int Interact(F_CharacterInteractor interactor);

    IEnumerator Progress(F_CharacterInteractor interactor, Transform yourself)
    {
        InProgress = true;
        int spoonsTaken = 0;
        while(Input.GetKey(KeyCode.F) && spoonsTaken < Task.spoonCost && Vector3.Distance(interactor.transform.position, yourself.position) <= interactor.FInteractionDistance)
        {
            GameManager.Instance.spoons--;
            spoonsTaken++;
            yield return new WaitForSeconds(1);
        }

        Finished = true;
    }
}
