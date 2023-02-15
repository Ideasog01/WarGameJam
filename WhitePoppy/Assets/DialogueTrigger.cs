using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : InteractItem
{
    [SerializeField]
    private Dialogue[] dialogueInteractions;

    [SerializeField]
    private int dialogueIndex;

    [SerializeField]
    private GameObject npcCamera;

    private DialogueManager _dialogueManager;

    public Dialogue[] DialogueInteractions
    {
        get { return dialogueInteractions; }
    }

    public int DialogueIndex
    {
        get { return dialogueIndex; }
    }

    public GameObject NPCCamera
    {
        get { return npcCamera; }
    }

    private void Awake()
    {
        _dialogueManager = GameObject.Find("GameManager").GetComponent<DialogueManager>();
    }

    public void ActivateDialogue()
    {
        if(ItemEnabled)
        {
            _dialogueManager.StartDialogue(this);
            ItemEnabled = false;
        }
    }
}

[System.Serializable]
public struct Dialogue
{
    [SerializeField]
    private string characterName;

    [SerializeField]
    private string[] dialogueContent;

    [SerializeField]
    private string[] choiceDescription;

    [SerializeField]
    private int[] choiceIndex;

    public string CharacterName
    {
        get { return characterName; }
    }

    public string[] DialogueContent
    {
        get { return dialogueContent; }
    }

    public string[] ChoiceDescription
    {
        get { return choiceDescription; }
    }

    public int[] ChoiceIndex
    {
        get { return choiceIndex; }
    }


}
