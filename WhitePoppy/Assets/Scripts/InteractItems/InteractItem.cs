using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            BaseCharacter.interactItem = this;
            PlayerInterface.DisplayInteractButton(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(BaseCharacter.interactItem == this)
            {
                BaseCharacter.interactItem = null;
                PlayerInterface.DisplayInteractButton(false);
            }
        }
    }
}
