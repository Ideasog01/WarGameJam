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
            }
            else if (interactItem.TryGetComponent<Item>(out Item item))
            {
                item.InteractItem();
            }
        }
    }
}
