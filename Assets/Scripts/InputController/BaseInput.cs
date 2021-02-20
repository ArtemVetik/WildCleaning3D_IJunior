using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInput : MonoBehaviour
{
    private IMoveable _moveableObject;

    public void Init(IMoveable moveableObject)
    {
        _moveableObject = moveableObject;
    }

    protected void Move(Vector2Int direction)
    {
        _moveableObject?.Move(direction);
    }
}
