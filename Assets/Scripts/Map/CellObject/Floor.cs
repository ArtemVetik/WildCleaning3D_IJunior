using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : CellObject
{
    [SerializeField] private CellMarker _marker;
    [SerializeField] private GameObject _frame;
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

    public void EnableFrame()
    {
        _frame.SetActive(true);
    }

    public void HideFrame()
    {
        _frame.SetActive(false);
    }

    private void OnMarked()
    {
        if (IsMarked == false)
            Instantiate(_cleanEffectTemplate, transform.position + Vector3.up * transform.localScale.y / 2, _cleanEffectTemplate.transform.rotation);
        
        IsMarked = true;
        HideFrame();
    }

    private void OnUnmarked()
    {
        IsMarked = false;
    }
}
