using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueCanvas;

    [Header("Dialogue Display")]

    [SerializeField]
    private TextMeshProUGUI characterNameText;

    [SerializeField]
    private TextMeshProUGUI dialogueContentText;

    [SerializeField]
    private GameObject[] dialogueChoiceObj;

    [SerializeField]
    private TextMeshProUGUI[] dialogueChoiceText;

    [SerializeField]
    private Button nextButton;

    [SerializeField]
    private GameObject crossHair;

    private int _dialogueIndex;

    private Dialogue _currentDialogue;

    private DialogueTrigger _dialogueTrigger;

    public void StartDialogue(DialogueTrigger dialogueTrigger)
    {
        dialogueCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _dialogueIndex = 0;

        NewDialogue(dialogueTrigger.DialogueInteractions[dialogueTrigger.DialogueIndex]);
        _dialogueTrigger = dialogueTrigger;

        PlayerInterface.DisplayInteractButton(false);

        SoldierCharacter.disableCombatMechanics = true;

        crossHair.SetActive(false);
        _dialogueTrigger.NPCCamera.SetActive(true);
        GameManager.EnableCamera(false);

        GameManager.isInteracting = true;
    }

    public void NewDialogue(Dialogue dialogue)
    {
        _dialogueIndex = 0;
        _currentDialogue = dialogue;
        nextButton.interactable = true;

        DisplayDialogue();
    }

    public void ContinueDialogue() //Via Inspector
    {
        _dialogueIndex++;

        if (_dialogueIndex > _currentDialogue.DialogueContent.Length - 1)
        {
            DisplayChoices();
        }
        else
        {
            DisplayDialogue();
        }
    }

    public void DisplayDialogue()
    {
        characterNameText.text = _currentDialogue.CharacterName;
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
    }

    private IEnumerator TypeSentence()
    {
        string sentence = _currentDialogue.DialogueContent[_dialogueIndex];

        dialogueContentText.text = "";

        foreach (char character in sentence.ToCharArray())
        {
            dialogueContentText.text += character;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void EndDialogue()
    {
        if (GameManager.interactItem.IsObjectiveAndIsActive)
        {
            GameManager.objectiveManager.UpdateObjective(1, Objective.ObjectiveType.TalkToCharacter);
        }

        _dialogueTrigger.NPCCamera.SetActive(false);

        dialogueCanvas.SetActive(false);
        GameManager.EnableCamera(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        SoldierCharacter.disableCombatMechanics = false;
        crossHair.SetActive(true);

        _dialogueTrigger = null;

        GameManager.isInteracting = false;
    }

    public void DisplayChoices()
    {
        if(_currentDialogue.ChoiceDescription.Length == 0)
        {
            EndDialogue();
        }
        else
        {
            nextButton.interactable = false;

            for (int i = 0; i < _currentDialogue.ChoiceDescription.Length; i++)
            {
                dialogueChoiceObj[i].SetActive(true);
                dialogueChoiceText[i].text = _currentDialogue.ChoiceDescription[i];
            }
        }
    }

    public void SelectChoice(int index) //Via Inspector
    {
        for (int i = 0; i < _currentDialogue.ChoiceDescription.Length; i++)
        {
            dialogueChoiceObj[i].SetActive(false);
        }

        NewDialogue(_dialogueTrigger.DialogueInteractions[_currentDialogue.ChoiceIndex[index]]);
    }
}
