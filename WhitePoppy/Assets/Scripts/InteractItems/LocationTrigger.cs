using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LocationTrigger : MonoBehaviour
{
    [SerializeField]
    private bool isEnabled = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isEnabled)
            {
                LocationReached();
                isEnabled = false;
            }
        }
    }

    public void LocationReached()
    {
        GameManager.objectiveManager.UpdateObjective(1, Objective.ObjectiveType.ReachLocation);
        Debug.Log("Yay!");
    }
}
