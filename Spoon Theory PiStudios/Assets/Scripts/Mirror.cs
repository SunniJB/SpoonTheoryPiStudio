using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, IInteractable
{
    public bool Interacted => false;

    public bool Interact(CharacterInteractor interactor)
    {
        Debug.Log("Its a mirror");
        return true;
    }
}
