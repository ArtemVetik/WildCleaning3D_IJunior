using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class CellMarker : MonoBehaviour
{
    [SerializeField] private Material _cleanMaterial;
    [SerializeField] private Material _dirtyMaterial;

    private MeshRenderer _meshRenderer;

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
    }

    public void Unmark()
    {
        _meshRenderer.material = _dirtyMaterial;
    }
}
