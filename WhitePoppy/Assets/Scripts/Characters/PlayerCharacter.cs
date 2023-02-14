using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    public static InteractItem interactItem;

    public void Interact()
    {
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
