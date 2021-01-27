using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : CellObject
{
    [SerializeField] private CellMarker _marker;
    [SerializeField] private ParticleSystem _cleanEffectTemplate;

    public bool IsMarked { get; private set; }

    private void OnEnable()
    {
        _marker.Marked += OnMarked;
        _marker.Unmarked += OnUnmarked;
    }

    private void OnDisable()
    {
        _marker.Marked -= OnMarked;
        _marker.Unmarked -= OnUnmarked;
    }

    private void Start()
    {
        IsMarked = false;
    }

    private void OnMarked()
    {
        if (IsMarked == false)
            Instantiate(_cleanEffectTemplate, transform.position, _cleanEffectTemplate.transform.rotation);
        
        IsMarked = true;
    }

    private void OnUnmarked()
    {
        IsMarked = false;
    }
}
