using CustomRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;

[Serializable]
public enum Direction
{
    Left, Right, Up, Down, None
}

[Serializable]
public struct MovePatternDirection
{
    [SerializeField] private Direction _direction;
    [SerializeField] private int _count;

    public Direction Direction => _direction;
    public int Count => _count;
}

[CreateAssetMenu(fileName = "New Move Pattern", menuName = "Redactor/ObjectParameters/MovePattern", order = 51)]
public class MovePattern : ObjectParameters
{
    [SerializeField] private List<MovePatternDirection> _pattern;

    public List<Vector2Int> VectorPattern { get; private set; }

    public override string Name
    {
        get
        {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            return Path.GetFileNameWithoutExtension(assetPath);
        }
    }

    private void OnEnable()
    {
        VectorPattern = InitVectorPattern();
    }

    public override void Apply(CellObject cellObject)
    {
        (cellObject as Enemy).InitMovePattern(this);
    }

    public override bool CanApply(LevelObject cellObject)
    {
        return cellObject is CustomRedactor.Enemy;
    }

    private List<Vector2Int> InitVectorPattern()
    {
        List<Vector2Int> pattern = new List<Vector2Int>();
        foreach (var item in _pattern)
        {
            Vector2Int direction = VectorByDirection(item.Direction);
            for (int i = 0; i < item.Count; i++)
                pattern.Add(direction);
        }

        return pattern;
    }

    private Vector2Int VectorByDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return Vector2Int.left;
            case Direction.Right:
                return Vector2Int.right;
            case Direction.Up:
                return Vector2Int.up;
            case Direction.Down:
                return Vector2Int.down;
            default:
                return Vector2Int.zero;
        }
    }
}
