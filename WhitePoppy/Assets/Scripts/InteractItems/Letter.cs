using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : InteractItem
{
    [SerializeField]
    private string letterDate;

    [SerializeField]
    private string letterAddressee;

    [SerializeField]
    private string letterContent;

    [SerializeField]
    private string letterSender;

    private PlayerInterface _playerInterface;

    private void Awake()
    {
        _playerInterface = GameObject.Find("GameManager").GetComponent<PlayerInterface>();
    }

    public void InteractLetter()
    {
        _playerInterface.DisplayLetter(letterDate, letterAddressee, letterContent, letterSender);
    }
}
