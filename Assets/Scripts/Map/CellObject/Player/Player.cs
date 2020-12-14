using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveSystem))]
public class Player : CellObject, IMoveable
{
    private PlayerMoveSystem _playerMoveSystem;
    private MapFiller _filler;
    private PlayerTail _tail;

    private void Awake()
    {
        _playerMoveSystem = new PlayerMoveSystem(GetComponent<MoveSystem>());
        _tail = new PlayerTail();
    }

    public void Init(MapFiller filler)
    {
        _filler = filler;
    }

    private void OnMoveEnded(GameCell finishCell)
    {
        CurrentCell = finishCell;
        finishCell.Mark();

        _tail.Add(finishCell, _playerMoveSystem.CurrentDirection);
    }

    public void Move(Vector2Int direction)
    {
        _playerMoveSystem.Move(CurrentCell, direction);
    }

    private void OnMarkedCellCrossed(GameCell cell)
    {
        _filler.TryFill(_tail);
        _tail.Clear();
    }

    private void OnPlayerStopped(GameCell cell)
    {
        _filler.TryFill(_tail);
        _tail.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _playerMoveSystem.ForceStop();
        }
    }

    private void OnEnable()
    {
        _playerMoveSystem.MoveEnded += OnMoveEnded;
        _playerMoveSystem.Stopped += OnPlayerStopped;
        _playerMoveSystem.MarkedCellCrossed += OnMarkedCellCrossed;
    }

    private void OnDisable()
    {
        _playerMoveSystem.MoveEnded -= OnMoveEnded;
        _playerMoveSystem.Stopped -= OnPlayerStopped;
        _playerMoveSystem.MarkedCellCrossed -= OnMarkedCellCrossed;
    }
}
