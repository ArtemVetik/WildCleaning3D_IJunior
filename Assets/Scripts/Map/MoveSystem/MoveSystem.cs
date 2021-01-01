using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveSystem : MonoBehaviour
{
    private ISpeedyObject _moveable;
    private GameCell _targetCell;

    public Vector2Int Direction { get; private set; }
    public bool IsMoving => enabled;

    public event UnityAction<GameCell> MoveEnded;

    private void OnEnable()
    {
        if (_targetCell == null)
            enabled = false;
    }

    public void Init(ISpeedyObject moveable)
    {
        _moveable = moveable;
    }

    public void Move(GameCell toCell, Vector2Int direction)
    {
        _targetCell = toCell;
        Direction = direction;
        enabled = true;
    }

    public void ForceStop()
    {
        enabled = false;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetCell.transform.position, _moveable.Speed * Time.deltaTime);
        if (transform.position == _targetCell.transform.position)
        {
            enabled = false;
            MoveEnded?.Invoke(_targetCell);
        }
    }
}
