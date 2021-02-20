using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : BaseInput
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Move(Vector2Int.left);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Move(Vector2Int.right);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Move(Vector2Int.up);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            Move(Vector2Int.down);
    }
}
