using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HorizontalLayoutGameObject : MonoBehaviour
{
    public enum Alignment
    {
        Left, Center, Right,
    }

    [SerializeField] private float _cellSize;
    [SerializeField] private float _spacing;
    [SerializeField] private Alignment _childAlignment;

    private void OnValidate()
    {
        Arrange();
    }

    private void OnTransformChildrenChanged()
    {
        Arrange();
    }

    private void Arrange()
    {
        var allChilds = GetAllChilds();

        float allChildsSize = _cellSize * transform.childCount;
        float allSpacingSize = _spacing * (transform.childCount - 1);

        float startXvalue = _cellSize / 2;

        if (_childAlignment == Alignment.Center)
            startXvalue += (allChildsSize + allSpacingSize) / -2;
        if (_childAlignment == Alignment.Right)
            startXvalue += -allChildsSize - allSpacingSize;

        Vector3 nextPosition = new Vector3(startXvalue, 0, 0);
        foreach (var child in allChilds)
        {
            child.transform.localPosition = nextPosition;

            nextPosition += Vector3.right * _cellSize;
            nextPosition += Vector3.right * _spacing;
        }
    }

    private IEnumerable<Transform> GetAllChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
            yield return transform.GetChild(i);
    }
}
