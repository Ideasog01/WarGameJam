using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractItem
{
    [SerializeField]
    private Mesh itemMesh;

    [SerializeField]
    private Material itemMaterial;

    [SerializeField]
    private Vector3 itemScale;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private string itemDescription;

    private ItemDisplay _itemDisplay;

    private void Awake()
    {
        _itemDisplay = GameObject.Find("GameManager").GetComponent<ItemDisplay>();
    }

    public void InteractItem()
    {
        _itemDisplay.DisplayItem(itemMesh, itemMaterial, itemScale, itemDescription, itemName, this.transform.GetChild(0).gameObject);
        PlayerInterface.DisplayInteractButton(false);
        GameManager.levelToLoad = levelToLoadByItemRef;
    }
}
