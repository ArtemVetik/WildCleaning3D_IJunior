using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private LevelDataBase _levelDataBase;
    [SerializeField] private GameCell _floor;
    [SerializeField] private PlayerInitializer _playerInitializer;

    private List<GameCell> _instCells;
    private int _currentLevel;

    private void Start()
    {
        _currentLevel = 0;
        _instCells = new List<GameCell>();

        foreach (Vector2Int cell in _levelDataBase[_currentLevel].Map.Keys)
        {
            GameCell gameCell = SpawnCell(cell, _floor);
            SpawnLevelObject(_levelDataBase[_currentLevel].Map[cell], gameCell);
        }

        InitAdjacentCells(_instCells);
    }

    private void SpawnLevelObject(CellData cellData, GameCell targetCell)
    {
        if (cellData.LevelObject == null)
            return;

        var inst = Instantiate(cellData.LevelObject.Prefab, targetCell.Position.ToVector3(0.5f), Quaternion.identity);
        inst.Init(targetCell);
        cellData.Parameters?.Apply(inst);

        if (inst.TryGetComponent(out Player player))
            _playerInitializer.SetPlayer(player);
    }

    private GameCell SpawnCell(Vector2Int cell, GameCell template)
    {
        GameCell gameCell = Instantiate(template, cell.ToVector3(), Quaternion.identity);
        gameCell.Init(cell);
        _instCells.Add(gameCell);
        return gameCell;
    }

    private void InitAdjacentCells(List<GameCell> cells)
    {
        foreach (GameCell gameCell in cells)
        {
            var adjacentDirections = new List<Vector2Int>() { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
            var adjacentCells = GetAdjacentCells(gameCell.Position, adjacentDirections);
            gameCell.InitAdjacentCells(adjacentCells);
        }
    }

    private Dictionary<Vector2Int, GameCell> GetAdjacentCells(Vector2Int fromPosition, List<Vector2Int> adjacentDirections)
    {
        var adjacentCells = new Dictionary<Vector2Int, GameCell>();

        foreach (Vector2Int direction in adjacentDirections)
            adjacentCells.Add(direction, _instCells.Find((cell) => cell.Position == fromPosition + direction));

        return adjacentCells;
    }
}
