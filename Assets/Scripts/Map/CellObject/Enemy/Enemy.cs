using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlaneMoveSystem))]
public abstract class Enemy : CellObject, ISpeedyObject
{    
    protected StageInfo CurrentStage;
    
    private MovePattern _movePattern;
    private PatternMoveSystem _patternMoveSystem;

    public float Speed => 2f;

    public event UnityAction<GameCell> MoveStarting;
    public event UnityAction MovePausing;
    public abstract event UnityAction<Enemy> Died;

    private void OnEnable()
    {
        var moveSystem = GetComponent<PlaneMoveSystem>();
        moveSystem.Init(this);

        _patternMoveSystem = new PatternMoveSystem(moveSystem);
        _patternMoveSystem.MoveStarted += OnMoveStarted;
        _patternMoveSystem.MovePausing += OnMovePausing;
    }

    private void OnDisable()
    {
        _patternMoveSystem.MoveStarted -= OnMoveStarted;
        _patternMoveSystem.MovePausing -= OnMovePausing;
        CurrentCell.Marked -= OnStepToMarkedCell;
    }

    private void Start()
    {
        CurrentCell.Marked += OnStepToMarkedCell;
        _patternMoveSystem.StartMove(CurrentCell, _movePattern, MeshHeight);
    }

    public void InitMovePattern(MovePattern movePattern)
    {
        _movePattern = movePattern;
    }

    public void InitStage(StageInfo currentStage)
    {
        CurrentStage = currentStage;
    }

    private void OnMoveStarted(GameCell nextCell)
    {
        MoveStarting?.Invoke(nextCell);
        if (nextCell.IsMarked)
        {
            OnStepToMarkedCell(nextCell);
            return;
        }

        CurrentCell.Marked -= OnStepToMarkedCell;

        CurrentCell = nextCell;
        CurrentCell.Marked += OnStepToMarkedCell;
    }

    private void OnMovePausing()
    {
        MovePausing?.Invoke();
    }

    public abstract void Die();
    protected abstract void OnStepToMarkedCell(GameCell markedCell);
}
