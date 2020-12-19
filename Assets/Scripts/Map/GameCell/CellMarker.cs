using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMarker : MonoBehaviour
{
    private Material _material;
    private Color _startColor;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _startColor = _material.color;
    }

    public void Mark()
    {
        _material.color = Color.white;
    }

    public void Unmark()
    {
        _material.color = _startColor;
    }
}
