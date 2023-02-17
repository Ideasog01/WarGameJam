using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    // Declarations
    void Interact(Transform interactor);

    string GetInteractPrompt();
}
