using UnityEngine;
using UnityEngine.Events;

public class TriggerEventBehaviour : EventBehaviour
{
    public ID filter;

    public UnityEvent<Collider> enterEvent, stayEvent, exitEvent;

    private int colliderCount;

    public int GetColliderCount(){
        return colliderCount;
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("AHHHHHHHHHHHHHHHH");
        if(filter != null)
        {
            if(!CheckFilter(other)) return;
        }
        colliderCount++;
        enterEvent.Invoke(other);
    }

    private void OnTriggerStay(Collider other) 
    {
        if(filter != null)
        {
            if(!CheckFilter(other)) return;
        }
        stayEvent.Invoke(other);
    }

    private void OnTriggerExit(Collider other) 
    {
        if(filter != null)
        {
            if(!CheckFilter(other)) return;
        }
        colliderCount--;
        exitEvent.Invoke(other);
    }

    private bool CheckFilter(Collider other)
    {
        var idBehaviour = other.GetComponent<IDBehaviour>();
        if(idBehaviour != null)
        {
            return idBehaviour.HasIDMatch(filter);
        }
        return false;
    }
}
