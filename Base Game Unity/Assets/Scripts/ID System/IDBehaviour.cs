using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDBehaviour : MonoBehaviour
{
    public List<ID> ids;

    public bool HasIDMatch(ID id)
    {
        return ids.Contains(id);
    }
}
