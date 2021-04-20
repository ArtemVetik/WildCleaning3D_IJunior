using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerReplacer : MonoBehaviour
{
    private Transform _player;
    private Vector3 _targetPosition;
    private GameCell _cell;

    public event UnityAction<GameCell> Replaced;

    public void Replace(Transform player, Vector3 position, GameCell cell)
    {
        _player = player;
        _targetPosition = position;
        _cell = cell;

        enabled = true;
    }

    private void Update()
    {
        var deltaPosition = _targetPosition - _player.position;
        Vector2Int currentDirection;

        if (Mathf.Abs(deltaPosition.x) > 0.1f)
        {
            var targetHorizontalPosition = _player.position + Vector3.right * Mathf.Sign(deltaPosition.x);
            _player.position = Vector3.MoveTowards(_player.position, targetHorizontalPosition, 10f * Time.deltaTime);

            currentDirection = Vector2Int.right * (int)Mathf.Sign(deltaPosition.x);
        }
        else
        {
            _player.position = Vector3.MoveTowards(_player.position, _targetPosition, 10f * Time.deltaTime);
            currentDirection = Vector2Int.down * (int)Mathf.Sign(deltaPosition.y);
        }

        float yAngle = Mathf.Atan2(currentDirection.x, currentDirection.y);
        var targetRotation = Quaternion.Euler(0, yAngle * Mathf.Rad2Deg - 90f, 0);

        _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, targetRotation, 1000f * Time.deltaTime);

        if (_player.position == _targetPosition)
        {
            Replaced?.Invoke(_cell);
            enabled = false;
        }
    }
}
