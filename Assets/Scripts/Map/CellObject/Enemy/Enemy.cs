using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MoveSystem))]
public abstract class Enemy : CellObject
{
    protected EnemyContainer Container;
    
    private MovePattern _movePattern;
    private PatternMoveSystem _patternMoveSystem;

    public abstract event UnityAction<Enemy> Died;

    private void OnEnable()
    {
        _patternMoveSystem = new PatternMoveSystem(GetComponent<MoveSystem>());
        _patternMoveSystem.MoveStarted += OnMoveStarted;
    }

    private void OnDisable()
    {
        _patternMoveSystem.MoveStarted -= OnMoveStarted;
        CurrentCell.Marked -= OnStepToMarkedCell;
    }

    private void Start()
    {
        CurrentCell.Marked += OnStepToMarkedCell;
        _patternMoveSystem.StartMove(CurrentCell, _movePattern);
    }

    public void InitMovePattern(MovePattern movePattern)
    {
        _movePattern = movePattern;
    }

    public void InitContainer(EnemyContainer container)
    {
        Container = container;
    }

    private void OnMoveStarted(GameCell nextCell)
    {
        if (nextCell.IsMarked)
        {
            OnStepToMarkedCell(nextCell);
            return;
        }

        CurrentCell.Marked -= OnStepToMarkedCell;

        CurrentCell = nextCell;
        CurrentCell.Marked += OnStepToMarkedCell;
    }

    protected abstract void OnStepToMarkedCell(GameCell markedCell);
}
