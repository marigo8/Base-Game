using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InteractionEventBehaviour : TriggerEventBehaviour
{
    public UnityEvent interactStartEvent, interactHoldEvent, interactEndEvent;

    private void Update()
    {
        if(GetColliderCount() <= 0) return;

        if(Input.GetButtonDown("Interact"))
        {
            interactStartEvent.Invoke();
        }

        if(Input.GetButton("Interact"))
        {
            interactHoldEvent.Invoke();
        }

        if(Input.GetButtonUp("Interact"))
        {
            interactEndEvent.Invoke();
        }
    }
}
