using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEventBehaviour : EventBehaviour
{
    public string button;
    public UnityEvent inputDownEvent, inputEvent, inputUpEvent;

    public void Update()
    {
        if(Input.GetButtonDown(button))
        {
            inputDownEvent.Invoke();
        }

        if (Input.GetButton(button))
        {
            inputEvent.Invoke();
        }

        if (Input.GetButtonUp(button))
        {
            inputUpEvent.Invoke();
        }
    }
}
