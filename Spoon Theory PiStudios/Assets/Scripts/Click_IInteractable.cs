using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Click_IInteractable
{

    //public string InteractionPrompt { get ; }

    public bool Interacted { get; }

    public bool Interact(Click_CharacterInteractor interactor);
}
