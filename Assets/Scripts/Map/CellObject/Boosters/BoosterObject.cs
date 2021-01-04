using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoosterObject : CellObject
{
    [SerializeField] private BoostersDataBase _boostersDataBase;
    [SerializeField] private Booster _booster;

    protected BoostersDataBase BoostersDataBase => _boostersDataBase;
    protected Booster Booster => _booster;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CellObject cellObject))
            Triggered(cellObject);
    }

    public abstract void Triggered(CellObject cellObject);    
}
