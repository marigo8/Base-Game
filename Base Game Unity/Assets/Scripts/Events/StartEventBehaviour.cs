using UnityEngine.Events;

public class StartEventBehaviour : EventBehaviour
{
    public UnityEvent startEvent;

    private void Start()
    {
        startEvent.Invoke();
    }
}
