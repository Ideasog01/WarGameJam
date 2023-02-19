using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private float range = 2f;

    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider c in colliders) {
            // Debug.Log(c);
            if (c.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(transform);
            }
        }
    }
}
