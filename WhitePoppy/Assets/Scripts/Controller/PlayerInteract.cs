using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public void Interact()
    {
        InteractItem interactItem = GameManager.interactItem;

        if (interactItem != null)
        {
            if (interactItem.TryGetComponent<Letter>(out Letter letter))
            {
                letter.InteractLetter();
                SoldierCharacter.disableCombatMechanics = true;
            }
            else if (interactItem.TryGetComponent<Item>(out Item item))
            {
                item.InteractItem();
                SoldierCharacter.disableCombatMechanics = true;
            }
            else if(interactItem.TryGetComponent<DialogueTrigger>(out DialogueTrigger trigger))
            {
                trigger.ActivateDialogue();
            }
        }
    }
}
