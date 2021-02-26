using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoveSystem
{
    private PlaneMoveSystem _moveSystem;
    private Vector2Int _nextMoveDirection;
    private MeshHeight _meshHeight;

    public Vector2Int CurrentDirection => _moveSystem.Direction;
    public bool IsMoving => _moveSystem.IsMoving;

    public event UnityAction<GameCell> MoveEnded;
    public event UnityAction<GameCell> Stopped;
    public event UnityAction<GameCell> MarkedCellCrossed;

    public PlayerMoveSystem(PlaneMoveSystem moveSystem, MeshHeight meshHeight)
    {
        _moveSystem = moveSystem;
        _meshHeight = meshHeight;
    }

    public bool Move(GameCell fromCell, Vector2Int direction)
    {
        if (direction == _moveSystem.Direction * -1)
            return false;

        _nextMoveDirection = direction;

        if (_moveSystem.IsMoving == false)
            return MoveNext(fromCell);

        return false;
    }

    public void ForceStop()
    {
        _nextMoveDirection = Vector2Int.zero;
        _moveSystem.ForceStop();
    }

    public void ResetDirection()
    {
        _moveSystem.ResetDirection();
    }

    private void OnMoveEnded(GameCell finishCell)
    {
        _moveSystem.MoveEnded -= OnMoveEnded;
        MoveEnded?.Invoke(finishCell);

        MoveNext(finishCell);
    }

    private bool MoveNext(GameCell from)
    {
        GameCell adjacentCell = from.TryGetAdjacent(_nextMoveDirection);
        if (adjacentCell == null)
        {
            Stopped?.Invoke(from);
            return false;
        }
        if (adjacentCell.IsMarked)
        {
            MarkedCellCrossed?.Invoke(from);
        }

        _moveSystem.MoveEnded += OnMoveEnded;
        _moveSystem.StartMove(adjacentCell, _nextMoveDirection, _meshHeight.MaxMeshHeight);
        return true;
    }
}
