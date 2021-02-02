using UnityEngine;
using UnityEngine.Events;

public class InteractionEventBehaviour : TriggerEventBehaviour
{
    public UnityEvent interactStartEvent, interactHoldEvent, interactEndEvent;

    private bool interacting;

    private void Update()
    {
        if(GetColliderCount() <= 0)
        {
            if (interacting)
            {
                interacting = false;
                interactEndEvent.Invoke();
            }
            return;
        }

        if (Input.GetButtonDown("Interact"))
        {
            interacting = true;
            interactStartEvent.Invoke();
            
        }

        if (!interacting) return;

        if(Input.GetButton("Interact"))
        {
            interactHoldEvent.Invoke();
        }

        if(Input.GetButtonUp("Interact"))
        {
            interacting = false;
            interactEndEvent.Invoke();
        }
    }
}
