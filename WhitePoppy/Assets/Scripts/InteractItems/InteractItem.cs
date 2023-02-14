using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerCharacter.interactItem = this;
            PlayerInterface.DisplayInteractButton(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(PlayerCharacter.interactItem == this)
            {
                PlayerCharacter.interactItem = null;
                PlayerInterface.DisplayInteractButton(false);
            }
        }
    }
}
