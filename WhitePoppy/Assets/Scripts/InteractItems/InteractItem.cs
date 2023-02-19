using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    [SerializeField]
    private bool itemEnabled = true;

    public enum LevelToLoadByItem {None, House, BattlefieldTrenches, ForestBattlefield, InvadedFacility}
    
    [SerializeField]
    private LevelToLoadByItem levelToLoadByItem;

    [SerializeField]
    private bool isObjectiveAndIsActive;

    public LevelToLoadByItem levelToLoadByItemRef
    {
        get { return levelToLoadByItem; }
    }

    public bool ItemEnabled
    {
        get { return itemEnabled; }
        set { itemEnabled = value; }
    }

    public bool IsObjectiveAndIsActive
    {
        get { return isObjectiveAndIsActive; }
        set { isObjectiveAndIsActive = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(itemEnabled)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.interactItem = this;
                PlayerInterface.DisplayInteractButton(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(itemEnabled)
        {
            if (other.CompareTag("Player"))
            {
                if (GameManager.interactItem == this)
                {
                    GameManager.interactItem = null;
                    PlayerInterface.DisplayInteractButton(false);
                }
            }
        }
    }
}
