using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemArray;

    private void Awake()
    {
        int itemCount = PlayerPrefs.GetInt("itemCount");

        switch(itemCount)
        {
            case 0:
                itemArray[0].SetActive(true);
                itemArray[1].SetActive(false);
                itemArray[2].SetActive(false);
                break;

            case 1:
                itemArray[0].SetActive(false);
                itemArray[1].SetActive(true);
                itemArray[2].SetActive(false);
                break;

            case 2:
                itemArray[0].SetActive(false);
                itemArray[1].SetActive(false);
                itemArray[2].SetActive(true);
                break;
        }
    }

    public static void IncreaseItemCollection()
    {
        int itemCount = PlayerPrefs.GetInt("itemCount");

        itemCount++;

        PlayerPrefs.SetInt("itemCount", itemCount);
    }
}
