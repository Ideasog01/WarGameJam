using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Animator door = null;
    [SerializeField]
    private bool isOpen = false;

    // Interface member implementations
    public void Interact(Transform interactor)
    {
       
        if (isOpen)
        {
            Debug.Log("Door closed");
            isOpen = false;
            door.Play("DoorClose", 0, 0.0f);
        }
        else
        {
            Debug.Log("Door opened");
            isOpen = true;
            door.Play("DoorOpen", 0, 0.0f);
        }
       
        Debug.Log("DOOR USED");
    }

    public string GetInteractPrompt()
    {
        return "Use Door";
    }
}
