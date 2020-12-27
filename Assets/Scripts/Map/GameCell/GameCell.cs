using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CellMarker))]
public class GameCell : MonoBehaviour
{
    private Vector2Int _position;
    private Dictionary<Vector2Int, GameCell> _adjacentCells;
    private CellMarker _cellMarker;

    public event UnityAction<GameCell> Marked;
    public event UnityAction<GameCell> Unmarked;

    public bool IsMarked { get; private set; }
    public Vector2Int Position => _position;

    private void Awake()
    {
        _cellMarker = GetComponent<CellMarker>();
    }

    public void Init(Vector2Int position)
    {
        _position = position;
    }

    public void InitAdjacentCells(Dictionary<Vector2Int, GameCell> adjacentCells)
    {
        _adjacentCells = adjacentCells;
    }

    public GameCell TryGetAdjacent(Vector2Int adjacentDirection)
    {
        if (_adjacentCells.ContainsKey(adjacentDirection))
            return _adjacentCells[adjacentDirection];

        return null;
    }

    public bool TryGetAdjacent(out GameCell adjacentCell, Vector2Int adjacentDirection)
    {
        if (_adjacentCells.ContainsKey(adjacentDirection))
        {
            adjacentCell = _adjacentCells[adjacentDirection];
            return true;
        }

        adjacentCell = null;
        return false;
    }

    public void Mark()
    {
        IsMarked = true;
        _cellMarker.Mark();
        Marked?.Invoke(this);
    }

    public void Unmark()
    {
        IsMarked = false;
        _cellMarker.Unmark();
        Unmarked?.Invoke(this);
    }
}
