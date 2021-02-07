using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CoolDownEventBehaviour : EventBehaviour
{
    public UnityEvent cooldownEvent;

    bool coolingDown;

    public void InvokeEvent(float cooldown)
    {
        if (coolingDown) return;
        cooldownEvent.Invoke();
        StartCoroutine(Cooldown(cooldown));
    }

    private IEnumerator Cooldown(float cooldown)
    {
        coolingDown = true;
        yield return new WaitForSeconds(cooldown);
        coolingDown = false;
    }
}
