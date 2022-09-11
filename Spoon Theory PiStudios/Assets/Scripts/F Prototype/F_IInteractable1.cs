using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface F_IInteractable
{

    public string InteractionPrompt { get ; }

    public bool Interacted { get; }
    public Task Task { get; }

    public int Interact(F_CharacterInteractor interactor);
}
