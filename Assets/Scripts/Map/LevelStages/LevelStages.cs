using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelStages : MonoBehaviour
{
    [SerializeField] private LevelSpawner _spawner;
    [SerializeField] private CurrentLevelLoader _levelNumber;

    private HashSet<GameCell> _stageCells;
    private LevelData _currentLevel;
    private int _currentStage;

    public event UnityAction<int> StageCompeted;
    public event UnityAction AllStageCompeted;

    private void OnEnable()
    {
        _spawner.SpawnCompleted += OnSpawnComplete;
        StageCompeted += OnStageComplete;
    }

    private void OnDisable()
    {
        _spawner.SpawnCompleted -= OnSpawnComplete;
        StageCompeted -= OnStageComplete;
    }

    private void Start()
    {
        _currentLevel = _levelNumber.CurrentLevel;
    }

    private void OnSpawnComplete()
    {
        _currentStage = 0;
        InitStage(_currentStage);
    }

    private void InitStage(int stage)
    {
        Vector2Int keyStageCell = _currentLevel.KeyStagesPoint[stage];

        _stageCells = new HashSet<GameCell>();
        InitStageCells(_spawner.InstCells.Find((cell) => cell.Position == keyStageCell));
    }

    private void InitStageCells(GameCell currentCell)
    {
        _stageCells.Add(currentCell);
        currentCell.Marked += OnCellMarked;

        GameCell adjacentCell = null;
        if (currentCell.TryGetAdjacent(out adjacentCell, Vector2Int.left) && _stageCells.Contains(adjacentCell) == false)
            InitStageCells(adjacentCell);
        if (currentCell.TryGetAdjacent(out adjacentCell, Vector2Int.right) && _stageCells.Contains(adjacentCell) == false)
            InitStageCells(adjacentCell);
        if (currentCell.TryGetAdjacent(out adjacentCell, Vector2Int.up) && _stageCells.Contains(adjacentCell) == false)
            InitStageCells(adjacentCell);
        if (currentCell.TryGetAdjacent(out adjacentCell, Vector2Int.down) && _stageCells.Contains(adjacentCell) == false)
            InitStageCells(adjacentCell);
    }

    private void OnCellMarked(GameCell markedCell)
    {
        markedCell.Marked -= OnCellMarked;
        _stageCells.Remove(markedCell);

        if (_stageCells.Count == 0)
            StageCompeted?.Invoke(_currentStage);
    }

    private void OnStageComplete(int stage)
    {
        stage++;

        if (stage < _currentLevel.KeyStagesPoint.Count)
            InitStage(++_currentStage);
        else
            AllStageCompeted?.Invoke();
    }
}
