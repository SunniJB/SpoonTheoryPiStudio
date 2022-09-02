using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Mirror : MonoBehaviour, F_IInteractable
{
    public bool Interacted => false;

    public bool Interact(F_CharacterInteractor interactor)
    {
        Debug.Log("Its a mirror");
        return true;
    }
}
