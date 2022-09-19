using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface F_IInteractable
{

    public string InteractionPrompt { get ; }

    public bool Interacted { get; }
    public Task Task { get; }

    public void Interact();
    public void InProgress(F_CharacterInteractor interactor);

    public IEnumerator Progress(F_CharacterInteractor interactor);
    public void Finished(F_CharacterInteractor interactor);
}
