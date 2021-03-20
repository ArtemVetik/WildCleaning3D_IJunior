using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapFiller : MonoBehaviour
{
    [SerializeField] private CurrentLevelLoader _levelLoader;

    public event UnityAction<FillData> StartFilling;
    public event UnityAction<FillData> EndFilled;

    public void TryFill(PlayerTail tail)
    {
        Vector2Int levelSize = _levelLoader.CurrentLevel.Size;
        int[,] mapTemplate = new int[levelSize.y, levelSize.x];

        FillData leftFillData = new FillData(new List<GameCell>(), true);
        FillData rightFillData = new FillData(new List<GameCell>(), true);

        foreach (var leftTail in tail.GetFromLeftSide())
            leftFillData = SetFillData(leftTail, leftFillData, mapTemplate);
        foreach (var rightTail in tail.GetFromRightSide())
            rightFillData = SetFillData(rightTail, rightFillData, mapTemplate);

        FillData targetFillData;

        if (leftFillData.IsClosed != rightFillData.IsClosed)
        {
            targetFillData = leftFillData.IsClosed ? leftFillData : rightFillData;
        }
        else
        {
            if (leftFillData.CellCount < rightFillData.CellCount && leftFillData.CellCount != 0)
            {
                targetFillData = leftFillData;
            }
            else if (rightFillData.CellCount < leftFillData.CellCount && rightFillData.CellCount != 0)
            {
                targetFillData = rightFillData;
            }
            else
            {
                if (leftFillData.CellCount != 0)
                    targetFillData = leftFillData;
                else
                    targetFillData = rightFillData;
            }
        }

        StartFilling?.Invoke(targetFillData);
        StartCoroutine(Fill(targetFillData));
    }

    private IEnumerator Fill(FillData fillData)
    {
        WaitForSeconds delay = new WaitForSeconds(0.01f);

        foreach (var cell in fillData.FilledCells)
        {
            cell.Mark(CellMarker.Type.Combo);
            yield return delay;
        }

        EndFilled?.Invoke(fillData);
    }

    private FillData SetFillData(GameCell currentCell, FillData data, int[,] mapTemplate)
    {
        if (currentCell == null)
            return new FillData(new List<GameCell>(data.FilledCells), false);
        if (currentCell.IsMarked || mapTemplate[currentCell.Position.y, currentCell.Position.x] != 0)
            return data;

        mapTemplate[currentCell.Position.y, currentCell.Position.x] = 1;
        data.Add(currentCell);

        data = SetFillData(currentCell.TryGetAdjacent(Vector2Int.left), data, mapTemplate);
        data = SetFillData(currentCell.TryGetAdjacent(Vector2Int.right), data, mapTemplate);
        data = SetFillData(currentCell.TryGetAdjacent(Vector2Int.up), data, mapTemplate);
        data = SetFillData(currentCell.TryGetAdjacent(Vector2Int.down), data, mapTemplate);
        return data;
    }
}

public struct FillData
{
    private List<GameCell> _filledCells;

    public IEnumerable<GameCell> FilledCells => _filledCells;
    public int CellCount => _filledCells.Count;
    public bool IsClosed { get; private set; }

    public FillData(List<GameCell> filledCells, bool isClosed)
    {
        _filledCells = filledCells;
        IsClosed = isClosed;
    }

    public GameCell Find(System.Predicate<GameCell> match)
    {
        return _filledCells.Find(match);
    }

    public void Add(GameCell filledCell)
    {
        _filledCells.Add(filledCell);
    }
}