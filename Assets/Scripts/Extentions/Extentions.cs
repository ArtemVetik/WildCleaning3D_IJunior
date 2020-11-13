using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static Vector2Int Left(this Vector2Int vector)
    {
        return vector + Vector2Int.left;
    }

    public static Vector2Int Right(this Vector2Int vector)
    {
        return vector + Vector2Int.right;
    }

    public static Vector2Int Up(this Vector2Int vector)
    {
        return vector + Vector2Int.up;
    }

    public static Vector2Int Down(this Vector2Int vector)
    {
        return vector + Vector2Int.down;
    }

    public static Vector3 ToVector3(this Vector2Int vector, float y = 0)
    {
        return new Vector3(vector.x, y, vector.y);
    }
}
