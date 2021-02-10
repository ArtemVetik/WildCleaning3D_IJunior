using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaneMoveSystem : MonoBehaviour
{
    private ISpeedyObject _moveable;
    private GameCell _targetCell;
    private Vector3 _targetPosition;

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

    public void Move(GameCell toCell, Vector2Int direction, float height = 1f)
    {
        _targetCell = toCell;
        _targetPosition = toCell.transform.position + Vector3.up * height / 2f;
        Direction = direction;
        enabled = true;
    }

    public void ForceStop()
    {
        enabled = false;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveable.Speed * Time.deltaTime);

        if (transform.position.x == _targetPosition.x && transform.position.z == _targetPosition.z)
        {
            enabled = false;
            MoveEnded?.Invoke(_targetCell);
        }
    }
}
