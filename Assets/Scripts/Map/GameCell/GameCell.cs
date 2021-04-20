using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CellMarker))]
public class GameCell : MonoBehaviour
{
    [SerializeField] private Floor _floor;
    [SerializeField] private CellMarker _cellMarker;
    [SerializeField] private DirtySelector _dirtySelector;
    [SerializeField] private Material _floorMaterial;

    private Vector2Int _position;
    private Dictionary<Vector2Int, GameCell> _adjacentCells;

    public event UnityAction<GameCell> Damaged;
    public event UnityAction<GameCell> Marked;
    public event UnityAction<GameCell> Unmarked;

    public bool IsMarked { get; private set; }
    public bool IsPartiallyMarked { get; private set; }
    public Vector2Int Position => _position;

    public void Init(Vector2Int position, Texture floorUV, Sprite[] dirtySprites)
    {
        IsPartiallyMarked = false;
        IsMarked = false;
        _position = position;
        _dirtySelector.Init(dirtySprites);
        _floorMaterial.mainTexture = floorUV;
    }

    public void InitAdjacentCells(Dictionary<Vector2Int, GameCell> adjacentCells)
    {
        _adjacentCells = adjacentCells;
    }

    public void EnableFrame()
    {
        _floor.EnableFrame();
    }

    public void HideFrame()
    {
        _floor.HideFrame();
    }

    public void ApplyDamage()
    {
        _cellMarker.SetColor(Color.red);
        Damaged?.Invoke(this);
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

    public void PartiallyMark()
    {
        IsMarked = true;
        IsPartiallyMarked = true;
        _cellMarker.PartiallyMark();
        Marked?.Invoke(this);
    }

    public void Mark(CellMarker.Type type = CellMarker.Type.Normal)
    {
        IsPartiallyMarked = false;
        IsMarked = true;
        _cellMarker.Mark(type);
        Marked?.Invoke(this);
    }

    public void DoubleMark()
    {
        _cellMarker.DoubleMark();
    }

    public void Unmark()
    {
        IsMarked = false;
        _cellMarker.Unmark();
        Unmarked?.Invoke(this);
    }
}
