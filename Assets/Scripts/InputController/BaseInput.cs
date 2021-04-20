using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseInput : MonoBehaviour
{
    private IMoveable _moveableObject;

    public event UnityAction<float> ScalingChanged;

    public void Init(IMoveable moveableObject)
    {
        _moveableObject = moveableObject;
    }

    protected void Move(Vector2Int direction)
    {
        _moveableObject?.Move(direction);
    }

    protected void Scaling(float value)
    {
        return;
        ScalingChanged?.Invoke(value);
    }
}
