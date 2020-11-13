using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    private IMoveable _moveableObject;

    public void Init(IMoveable moveableObject)
    {
        _moveableObject = moveableObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _moveableObject.Move(Vector2Int.left);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            _moveableObject.Move(Vector2Int.right);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            _moveableObject.Move(Vector2Int.up);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            _moveableObject.Move(Vector2Int.down);
    }
}
