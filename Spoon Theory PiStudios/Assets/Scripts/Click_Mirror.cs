using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Mirror : MonoBehaviour, Click_IInteractable
{
    public bool Interacted => false;

    public bool Interact(Click_CharacterInteractor interactor)
    {
        Debug.Log("Its a mirror");
        return true;
    }
}
