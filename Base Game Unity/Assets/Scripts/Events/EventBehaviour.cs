using UnityEngine;

public class EventBehaviour : MonoBehaviour
{
    public void DestoryObject(GameObject obj)
    {
        Destroy(obj);
    }

    public void DebugMessage(string message)
    {
        Debug.Log(message);
    }
}
