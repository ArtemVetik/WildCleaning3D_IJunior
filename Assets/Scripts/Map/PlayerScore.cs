using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct EnemyInContour
{
    private FillData _fillData;
    private HashSet<Enemy> _enemies;

    public EnemyInContour(FillData fillData, EnemyContainer _enemyContainer)
    {
        _fillData = fillData;
        _enemies = new HashSet<Enemy>();

        foreach (var enemy in _enemyContainer.Enemies)
        {
            if (fillData.FilledCells.Find((cell) => cell.Position == enemy.CurrentCell.Position))
                _enemies.Add(enemy);
        }
    }

    public FillData FillData => _fillData;
    public int EnemyCount => _enemies.Count;

    public bool Has(Enemy enemy)
    {
        return _enemies.Contains(enemy);
    }
}

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private MapFiller _mapFiller;

    private List<EnemyInContour> _enemyInContours = new List<EnemyInContour>();

    public event UnityAction<int> ScoreChanged;

    public int Score { get; private set; }

    private void OnEnable()
    {
        _enemyContainer.EnemyDied += OnEnemyDied;
        _mapFiller.StartFilled += OnStartFilled;
        _mapFiller.EndFilled += OnEndFilled;
    }

    private void OnDisable()
    {
        _enemyContainer.EnemyDied -= OnEnemyDied;
        _mapFiller.StartFilled -= OnStartFilled;
    }

    private void OnStartFilled(FillData fillData)
    {
        _enemyInContours.Add(new EnemyInContour(fillData, _enemyContainer));
    }

    private void OnEndFilled(FillData fillData)
    {
        var findData = _enemyInContours.Find((data) => data.FillData.Equals(fillData));
        _enemyInContours.Remove(findData);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        Score += CalculateScore(enemy);
        ScoreChanged?.Invoke(Score);
    }

    private int CalculateScore(Enemy enemy)
    {
        int score = 1;

        foreach (var contourData in _enemyInContours)
            if (contourData.Has(enemy))
                score = contourData.EnemyCount / 10 + 1;

        return score;
    }
}
