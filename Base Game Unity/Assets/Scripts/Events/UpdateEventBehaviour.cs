
using UnityEngine.Events;

public class UpdateEventBehaviour : EventBehaviour
{
    public UnityEvent updateEvent;

    private void Update() 
    {
        updateEvent.Invoke();
    }
}
