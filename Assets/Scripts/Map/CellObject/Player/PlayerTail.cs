using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct TailData
{
    public GameCell Cell { get; private set; }
    public Vector2Int ForwardDirection { get; private set; }

    public TailData(GameCell gameCell, Vector2Int forwardDirection)
    {
        Cell = gameCell;
        ForwardDirection = forwardDirection;
    }
}

public class PlayerTail
{
    private List<TailData> _tail;

    public PlayerTail()
    {
        _tail = new List<TailData>();
    }

    public int Count => _tail.Count;
    public event UnityAction Damaged;

    public void Add(GameCell cell, Vector2Int forwardDirection)
    {
        _tail.Add(new TailData(cell, forwardDirection));
        cell.Damaged += OnCellDamaged;
    }

    private void OnCellDamaged(GameCell cell)
    {
        Damaged?.Invoke();
    }

    public void FillTail()
    {
        foreach (var tailData in _tail)
            tailData.Cell.Mark();
    }

    public void Clear()
    {
        foreach (var tailData in _tail)
            tailData.Cell.Damaged -= OnCellDamaged;

        _tail.Clear();
    }

    public IEnumerable<GameCell> GetFromLeftSide()
    {
        return GetFromSide(90f);
    }

    public IEnumerable<GameCell> GetFromRightSide()
    {
        return GetFromSide(-90f);
    }

    private IEnumerable<GameCell> GetFromSide(float angle)
    {
        HashSet<GameCell> sideCells = new HashSet<GameCell>();

        Vector2Int sideDirection;
        GameCell sideCell, forwardCell;
        for (int i = 0; i < _tail.Count; i++)
        {
            sideDirection = _tail[i].ForwardDirection.Rotate(angle);

            sideCell = _tail[i].Cell.TryGetAdjacent(sideDirection);
            forwardCell = _tail[i].Cell.TryGetAdjacent(_tail[i].ForwardDirection);

            if (sideCell != null && sideCell.IsMarked == false)
                if (sideCells.Contains(sideCell) == false)
                    sideCells.Add(sideCell);

            if (sideCell == null || sideCell.IsMarked == false)
                if (forwardCell != null && forwardCell.IsMarked == false)
                    if (sideCells.Contains(forwardCell) == false)
                        sideCells.Add(forwardCell);
        }

        return sideCells;
    }
}
