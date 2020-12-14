using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveSystem))]
public abstract class Enemy : CellObject
{
    private MovePattern _movePattern;
    private PatternMoveSystem _patternMoveSystem;

    private void OnEnable()
    {
        _patternMoveSystem = new PatternMoveSystem(GetComponent<MoveSystem>());
        _patternMoveSystem.MoveStarted += OnMoveStarted;
    }

    private void OnDisable()
    {
        _patternMoveSystem.MoveStarted -= OnMoveStarted;
        CurrentCell.Marked -= OnCellMarked;
        Debug.Log("-1 " + name);
    }

    private void Start()
    {
        CurrentCell.Marked += OnCellMarked;
        Debug.Log("+1 " + name);
        _patternMoveSystem.StartMove(CurrentCell, _movePattern);
    }

    public void InitMovePattern(MovePattern movePattern)
    {
        _movePattern = movePattern;
    }

    private void OnMoveStarted(GameCell nextCell)
    {
        if (nextCell.IsMarked)
        {
            Destroy(gameObject);
            return;
        }

        CurrentCell.Marked -= OnCellMarked;

        CurrentCell = nextCell;
        CurrentCell.Marked += OnCellMarked;
    }

    private void OnCellMarked()
    {
        Destroy(gameObject);
    }
}
