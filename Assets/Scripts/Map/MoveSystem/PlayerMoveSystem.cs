using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoveSystem
{
    private MoveSystem _moveSystem;
    private Vector2Int _nextMoveDirection;

    public event UnityAction<GameCell> MoveEnded;

    public PlayerMoveSystem(MoveSystem moveSystem)
    {
        _moveSystem = moveSystem;
    }

    public void Move(GameCell fromCell, Vector2Int direction)
    {
        if (direction == _moveSystem.Direction * -1)
            return;

        _nextMoveDirection = direction;

        if (_moveSystem.IsMoving == false)
            MoveNext(fromCell);
    }

    public void ForceStop()
    {
        _moveSystem.ForceStop();
    }

    private void OnMoveEnded(GameCell finishCell)
    {
        _moveSystem.MoveEnded -= OnMoveEnded;
        MoveEnded?.Invoke(finishCell);

        MoveNext(finishCell);
    }

    private void MoveNext(GameCell from)
    {
        GameCell adjacentCell = from.TryGetAdjacent(_nextMoveDirection);
        if (adjacentCell == null)
            return;

        _moveSystem.MoveEnded += OnMoveEnded;
        _moveSystem.Move(adjacentCell, _nextMoveDirection);
    }
}
