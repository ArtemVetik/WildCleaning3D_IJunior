using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : CellObject
{
    [SerializeField] private CellMarker _marker;
    [SerializeField] private GameObject _frame;
    [SerializeField] private ParticleSystem _cleanEffectTemplate;
    [SerializeField] private ParticleSystem _superCleanEffectTemplate;

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

    private void OnMarked(CellMarker.Type type)
    {
        if (IsMarked == false)
        {
            var position = transform.position + Vector3.up * transform.localScale.y / 1.5f;
            if (type == CellMarker.Type.Normal)
                Instantiate(_cleanEffectTemplate, position, _cleanEffectTemplate.transform.rotation);
            else if (type == CellMarker.Type.Combo)
                Instantiate(_superCleanEffectTemplate, position, _superCleanEffectTemplate.transform.rotation);
        }

        IsMarked = true;
        HideFrame();
    }

    private void OnUnmarked()
    {
        IsMarked = false;
    }
}
