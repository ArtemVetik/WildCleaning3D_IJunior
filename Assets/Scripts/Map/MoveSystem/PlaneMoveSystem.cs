using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class MoveParameter
{
    [SerializeField] private PlaneMoveSystem.MoveType _type;
    [SerializeField] private AnimationCurve _curve;

    public PlaneMoveSystem.MoveType Type => _type;
    public AnimationCurve Curve => _curve;

    public void Validate()
    {
        for (int i = 0; i < _curve.keys.Length; i++)
        {
            var temp = _curve.keys[i];
            temp.time = Mathf.Clamp(temp.time, 0f, 10f);
            temp.value = Mathf.Clamp(temp.value, 0f, 1f);

            _curve.MoveKey(i, temp);
        }
    }
}

public class PlaneMoveSystem : MonoBehaviour
{
    public enum MoveType
    {
        Normal, StartLerp, EndLerp, StartEndLerp
    }

    [SerializeField] private List<MoveParameter> _moveParameters;

    private ISpeedyObject _moveable;
    private GameCell _targetCell;
    private Vector3 _targetPosition;
    private Vector3 _startMovePosition;
    private AnimationCurve _curve;
    private float _curveParameter;

    public Vector2Int Direction { get; private set; }
    public bool IsMoving => enabled;

    public event UnityAction<GameCell> MoveEnded;

    private void OnValidate()
    {
        foreach (var parameter in _moveParameters)
            parameter.Validate();
    }

    private void OnEnable()
    {
        if (_targetCell == null)
            enabled = false;
    }

    public void Init(ISpeedyObject moveable)
    {
        _moveable = moveable;
    }

    public void StartMove(GameCell toCell, Vector2Int direction, float height = 1f, MoveType type = MoveType.Normal)
    {
        _targetCell = toCell;
        _targetPosition = toCell.transform.position;
        _targetPosition.y = transform.position.y;
        Direction = direction;
        
        _curve = _moveParameters.Find(parameter => parameter.Type == type).Curve;
        _startMovePosition = transform.position;
        _curveParameter = 0;

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
        _curveParameter = Mathf.MoveTowards(_curveParameter, _curve.length, _moveable.Speed * Time.deltaTime);

        var curveValue = _curve.Evaluate(_curveParameter);
        var direction = _targetPosition - _startMovePosition;

        transform.position = _startMovePosition + direction * curveValue;

        if (transform.position == _targetPosition)
        {
            enabled = false;
            MoveEnded?.Invoke(_targetCell);
        }
    }
}
