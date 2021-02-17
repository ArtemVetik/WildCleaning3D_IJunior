using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageInfo
{
    private HashSet<GameCell> _stageCells;
    private HashSet<Enemy> _enemies;

    public IEnumerable<GameCell> StageCells => _stageCells;

    public event UnityAction<int> StageCompleted;
    public event UnityAction<int> FilledCountChanged;

    public int StageNumber { get; private set; }
    public int Microbes { get; private set; }
    public int FilledCount { get; private set; }
    public int CellCount { get; private set; }

    public StageInfo(int stageNumber, GameCell checkpointCell, IEnumerable<Enemy> enemies)
    {
        StageNumber = stageNumber;
        _stageCells = new HashSet<GameCell>();
        InitStageCells(checkpointCell);
        InitEnemies(enemies);

        CellCount = _stageCells.Count;
        FilledCount = 0;
    }

    ~StageInfo()
    {
        foreach (var cell in _stageCells)
            cell.Marked -= OnCellMarked;
    }

    private void InitEnemies(IEnumerable<Enemy> enemies)
    {
        _enemies = new HashSet<Enemy>();
        Microbes = 0;

        foreach (var enemy in enemies)
        {
            if (_stageCells.Contains(enemy.CurrentCell))
            {
                _enemies.Add(enemy);
                enemy.InitStage(this);
                enemy.Died += OnEnemyDied;

                if (enemy is Microbe)
                    Microbes++;
            }
        }
    }

    public bool Contains(GameCell cell)
    {
        return _stageCells.Contains(cell);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        _enemies.Remove(enemy);

        if (enemy is Microbe)
            Microbes--;
    }

    private void InitStageCells(GameCell checkpointCell)
    {
        _stageCells.Add(checkpointCell);
        checkpointCell.Marked += OnCellMarked;

        GameCell adjacentCell = null;
        if (checkpointCell.TryGetAdjacent(out adjacentCell, Vector2Int.left) && _stageCells.Contains(adjacentCell) == false)
            InitStageCells(adjacentCell);
        if (checkpointCell.TryGetAdjacent(out adjacentCell, Vector2Int.right) && _stageCells.Contains(adjacentCell) == false)
            InitStageCells(adjacentCell);
        if (checkpointCell.TryGetAdjacent(out adjacentCell, Vector2Int.up) && _stageCells.Contains(adjacentCell) == false)
            InitStageCells(adjacentCell);
        if (checkpointCell.TryGetAdjacent(out adjacentCell, Vector2Int.down) && _stageCells.Contains(adjacentCell) == false)
            InitStageCells(adjacentCell);
    }

    private void OnCellMarked(GameCell markedCell)
    {
        markedCell.Marked -= OnCellMarked;
        markedCell.Unmarked += OnCellUnmarked;
        _stageCells.Remove(markedCell);

        FilledCount++;
        FilledCountChanged?.Invoke(FilledCount);

        if (_stageCells.Count == 0)
            StageCompleted?.Invoke(StageNumber);
    }

    private void OnCellUnmarked(GameCell unmarkedCell)
    {
        unmarkedCell.Unmarked -= OnCellUnmarked;
        unmarkedCell.Marked += OnCellMarked;

        FilledCount--;
        FilledCountChanged?.Invoke(FilledCount);

        _stageCells.Add(unmarkedCell);
    }
}
