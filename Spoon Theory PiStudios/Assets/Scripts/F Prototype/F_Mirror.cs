using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Mirror : MonoBehaviour, F_IInteractable
{
    public bool Interacted => false;

    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    public int SpoonsCost => 2;

    public int Interact(F_CharacterInteractor interactor)
    {
        Debug.Log("Its a mirror");
        return SpoonsCost;
    }
}
