using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class CellMarker : MonoBehaviour
{
    [SerializeField] private Material _cleanMaterial;
    [SerializeField] private Material _dirtyMaterial;

    private MeshRenderer _meshRenderer;

    public event UnityAction Marked;
    public event UnityAction Unmarked;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _meshRenderer.material = _dirtyMaterial;
    }

    public void Mark()
    {
        _meshRenderer.material = _cleanMaterial;
        Marked?.Invoke();
    }

    public void Unmark()
    {
        _meshRenderer.material = _dirtyMaterial;
        Unmarked?.Invoke();
    }
}
