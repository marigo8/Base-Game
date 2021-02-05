using UnityEngine;
using UnityEngine.Events;

public class TriggerEventBehaviour : EventBehaviour
{
    public ID filter;

    public UnityEvent<GameObject> enterEvent, stayEvent, exitEvent;

    protected int colliderCount;

    public int GetColliderCount(){
        return colliderCount;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(filter != null)
        {
            if(!CheckFilter(other)) return;
        }
        colliderCount++;
        enterEvent.Invoke(other.gameObject);
    }

    private void OnTriggerStay(Collider other) 
    {
        if(filter != null)
        {
            if(!CheckFilter(other)) return;
        }
        stayEvent.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other) 
    {
        if(filter != null)
        {
            if(!CheckFilter(other)) return;
        }
        colliderCount--;
        exitEvent.Invoke(other.gameObject);
    }

    protected bool CheckFilter(Collider other)
    {
        var idBehaviour = other.GetComponent<IDBehaviour>();
        if(idBehaviour != null)
        {
            return idBehaviour.HasIDMatch(filter);
        }
        return false;
    }
}
