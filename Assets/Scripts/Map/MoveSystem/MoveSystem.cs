using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveSystem : MonoBehaviour
{
    [SerializeField] private float _speed;

    private GameCell _targetCell;

    public Vector2Int Direction { get; private set; }
    public bool IsMoving => enabled;

    public event UnityAction<GameCell> MoveEnded;

    private void OnEnable()
    {
        if (_targetCell == null)
            enabled = false;
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
        transform.position = Vector3.MoveTowards(transform.position, _targetCell.transform.position, _speed * Time.deltaTime);
        if (transform.position == _targetCell.transform.position)
        {
            enabled = false;
            Direction = Vector2Int.zero;
            MoveEnded?.Invoke(_targetCell);
        }
    }
}
