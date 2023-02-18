using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class ObjectiveManager : MonoBehaviour
{
    public static Objective currentObjective;

    [SerializeField]
    private Objective[] objectiveList;

    [SerializeField]
    private int objectiveIndex;

    [SerializeField]
    private Animator objectiveAnimator;

    [SerializeField]
    private bool autoSceneTransition;

    private GameManager _gameManager;

    private PlayerInterface _playerInterface;

    private void Awake()
    {
        _gameManager = this.GetComponent<GameManager>();
        _playerInterface = this.GetComponent<PlayerInterface>();
    }

    private void Start()
    {
        if(objectiveList.Length > 0)
        {
            currentObjective = objectiveList[objectiveIndex];
        }
    }

    public void UpdateObjective(int increaseAmount, Objective.ObjectiveType type)
    {
        if(type != currentObjective.ObjectiveTypeRef || objectiveList.Length == 0)
        {
            return;
        }

        currentObjective.Progression += increaseAmount;

        if(currentObjective.Progression >= currentObjective.MaxProgress)
        {
            currentObjective.CompleteEvents.Invoke();

            objectiveIndex++;

            if(objectiveIndex >= objectiveList.Length && autoSceneTransition)
            {
                GameManager.levelToLoad = InteractItem.LevelToLoadByItem.House;
                _gameManager.LoadSceneTransition();
                return;
            }
            else
            {
                currentObjective = objectiveList[objectiveIndex];
                objectiveAnimator.SetTrigger("activate");
            }

            
            Debug.Log("Objective Complete");
        }

        _playerInterface.UpdateObjectiveText();
    }
}

[System.Serializable]
public struct Objective
{
    public enum ObjectiveType { Miscellaneous, DefeatEnemy, ReachLocation, FindItem, DeliverItem, TalkToCharacter };

    [SerializeField]
    private string objectiveDescription;

    [SerializeField]
    private bool displayProgression;

    [SerializeField]
    private ObjectiveType objectiveType;

    [SerializeField]
    private int progression;

    [SerializeField]
    private int maxProgress;

    [SerializeField]
    private UnityEvent completeEvents;

    public string ObjectiveDescription
    {
        get { return objectiveDescription; }
    }

    public bool DisplayProgression
    {
        get { return displayProgression; }
    }

    public ObjectiveType ObjectiveTypeRef
    {
        get { return objectiveType; }
    }

    public int Progression
    {
        get { return progression; }
        set { progression = value; }
    }

    public int MaxProgress
    {
        get { return maxProgress; }
    }

    public UnityEvent CompleteEvents
    {
        get { return completeEvents; }
    }
}