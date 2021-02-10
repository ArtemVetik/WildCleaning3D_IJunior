using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshHeight))]
public abstract class CellObject : MonoBehaviour
{
    public GameCell CurrentCell { get; protected set; }
    public MeshHeight MeshHeight { get; protected set; }

    private void Awake()
    {
        MeshHeight = GetComponent<MeshHeight>();
        OnAwake();
    }

    protected virtual void OnAwake() { }

    public void Init(GameCell currentCell)
    {
        CurrentCell = currentCell;
    }
}
