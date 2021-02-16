using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BoosterObject : CellObject
{
    [SerializeField] private BoostersDataBase _boostersDataBase;
    [SerializeField] private Booster _booster;
    [SerializeField] private GameObject _collectedEffect;

    protected BoostersDataBase BoostersDataBase => _boostersDataBase;
    protected Booster Booster => _booster;

    public event UnityAction<BoosterObject> Collected;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CellObject cellObject))
            Triggered(cellObject);

        Instantiate(_collectedEffect, transform.position, _collectedEffect.transform.rotation);
        Collected?.Invoke(this);
    }

    public abstract void Triggered(CellObject cellObject);    
}
