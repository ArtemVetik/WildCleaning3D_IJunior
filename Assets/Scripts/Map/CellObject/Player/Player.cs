using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveSystem))]
public class Player : CellObject, IMoveable
{
    private PlayerMoveSystem _playerMoveSystem;

    private void Awake()
    {
        _playerMoveSystem = new PlayerMoveSystem(GetComponent<MoveSystem>());
    }

    private void OnMoveEnded(GameCell finishCell)
    {
        CurrentCell = finishCell;
        finishCell.Mark();
    }

    public void Move(Vector2Int direction)
    {
        _playerMoveSystem.Move(CurrentCell, direction);
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
    }

    private void OnDisable()
    {
        _playerMoveSystem.MoveEnded -= OnMoveEnded;
    }
}
