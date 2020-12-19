using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFiller : MonoBehaviour
{
    [SerializeField] private CurrentLevelLoader _levelLoader;

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
            

        if (leftFillData.FilledCells.Count == 0 || rightFillData.FilledCells.Count == 0)
            return;

        if (leftFillData.IsClosed != rightFillData.IsClosed)
        {
            StartCoroutine(Fill(leftFillData.IsClosed ? leftFillData : rightFillData));
            return;
        }

        if (leftFillData.FilledCells.Count < rightFillData.FilledCells.Count)
            StartCoroutine(Fill(leftFillData));
        else
            StartCoroutine(Fill(rightFillData));

    }

    private IEnumerator Fill(FillData fillData)
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);

        foreach (var cell in fillData.FilledCells)
        {
            cell.Mark();
            yield return delay;
        }
    }

    private FillData SetFillData(GameCell currentCell, FillData data, int[,] mapTemplate)
    {
        if (currentCell == null)
            return new FillData(data.FilledCells, false);
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
    public List<GameCell> FilledCells { get; private set; }
    public bool IsClosed { get; private set; }

    public FillData(List<GameCell> filledCells, bool isClosed)
    {
        FilledCells = filledCells;
        IsClosed = isClosed;
    }

    public void Add(GameCell filledCell)
    {
        FilledCells.Add(filledCell);
    }
}