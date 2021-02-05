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
        if(Input.GetButtonDown(button) || Input.GetKeyDown(button))
        {
            inputDownEvent.Invoke();
        }

        if (Input.GetButton(button) || Input.GetKey(button))
        {
            inputEvent.Invoke();
        }

        if (Input.GetButtonUp(button) || Input.GetKeyUp(button))
        {
            inputUpEvent.Invoke();
        }
    }
}
