using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLantern : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Light wallLantern = null;
    [SerializeField]
    private bool isOn = false;

    // Interface member implementations
    public void Interact(Transform interactor)
    {
        if (isOn)
        {
            isOn = false;
            wallLantern.enabled = false;
        }
        else
        {
            isOn = true;
            wallLantern.enabled = true;
        }
        Debug.Log("WALL LANTERN USED");
    }

    public string GetInteractPrompt()
    {
        return "Use Wall Lantern";
    }
}
