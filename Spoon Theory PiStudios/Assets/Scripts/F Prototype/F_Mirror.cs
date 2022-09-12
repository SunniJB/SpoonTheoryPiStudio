using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Mirror : MonoBehaviour, F_IInteractable //In inheritance, the objects are the same "class." In interfaces, they just have to share a characteristic
{
    public bool Interacted => false;

    [SerializeField] private string _prompt; //In private variables, it is common practice to preface the name with an underscore

    [SerializeField] Task _task;
    public Task Task => _task;  //When working with interfaces, you have to use a lambda operator to assign a value. So it has to be => instead of =.

    public string InteractionPrompt => Task.description;

    private void Awake()
    {
        Task.outlineObject = GetComponent<Outline>();
        //Task.outlineObject.enabled = false;
    }

    public int Interact(F_CharacterInteractor interactor)
    {
        Debug.Log("Its a mirror");
        return Task.spoonCost;
    }
}
