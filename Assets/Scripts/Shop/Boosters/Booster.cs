using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Booster : BaseScriptableObject
{
    public abstract event UnityAction<Booster> Used;
    public int instanceID;

    public abstract void Use();

    private void OnValidate()
    {
        instanceID = GetInstanceID();
    }
}
