using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaneMoveSystem : MonoBehaviour
{
    private ISpeedyObject _moveable;
    private GameCell _targetCell;
    private Vector3 _targetPosition;
    private bool _isLerpMoving;

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

    public void StartMove(GameCell toCell, Vector2Int direction, float height = 1f)
    {
        _isLerpMoving = false;
        _targetCell = toCell;
        _targetPosition = toCell.transform.position;
        _targetPosition.y = transform.position.y;
        Direction = direction;
        enabled = true;
    }

    public void StartMoveLerp(GameCell toCell, Vector2Int direction, float height = 1f)
    {
        _isLerpMoving = true;
        _targetCell = toCell;
        _targetPosition = toCell.transform.position;
        _targetPosition.y = transform.position.y;
        Direction = direction;
        enabled = true;
    }

    public void ForceStop()
    {
        enabled = false;
    }

    public void ResetDirection()
    {
        Direction = Vector2Int.zero;
    }

    private void Update()
    {
        if (_isLerpMoving)
            MoveLerp();
        else
            Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveable.Speed * Time.deltaTime);

        if (transform.position == _targetPosition)
        {
            enabled = false;
            MoveEnded?.Invoke(_targetCell);
        }
    }

    private void MoveLerp()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveable.Speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.05f)
        {
            enabled = false;
            MoveEnded?.Invoke(_targetCell);
        }
    }
}
