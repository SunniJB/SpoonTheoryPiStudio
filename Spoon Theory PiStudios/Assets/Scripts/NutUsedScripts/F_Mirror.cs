using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Mirror : MonoBehaviour, F_IInteractable //In inheritance, the objects are the same "class." In interfaces, they just have to share a characteristic
{
    public bool Finished => false;

    [SerializeField] private string _prompt; //In private variables, it is common practice to preface the name with an underscore

    [SerializeField] Task _task;
    public Task Task => _task;  //When working with interfaces, you have to use a lambda operator to assign a value. So it has to be => instead of =.

    public string InteractionPrompt => Task.description;

    bool F_IInteractable.Finished { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool InProgress { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        Task.outlineObject = GetComponent<Outline>();
        Task.outlineObject.enabled = false;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public int Interact(CharacterInteractor interactor)
    {
        throw new System.NotImplementedException();
    }
}
