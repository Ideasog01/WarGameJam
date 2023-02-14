using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.interactItem = this;
            PlayerInterface.DisplayInteractButton(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(GameManager.interactItem == this)
            {
                GameManager.interactItem = null;
                PlayerInterface.DisplayInteractButton(false);
            }
        }
    }
}
