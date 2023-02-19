using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField]
    private UnityEvent animationEvent;

    public void ActivateEvent()
    {
        animationEvent.Invoke();
    }
}
