using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoosterObject : CellObject
{
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CellObject cellObject))
            Triggered(cellObject);
    }

    public abstract void Triggered(CellObject cellObject);    
}
