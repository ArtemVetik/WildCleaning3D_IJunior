using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyInContour
{
    private FillData _fillData;
    private HashSet<Enemy> _enemies;

    public event UnityAction<EnemyInContour> ContourFilling;

    public EnemyInContour(FillData fillData, EnemyContainer _enemyContainer)
    {
        _fillData = fillData;
        _enemies = new HashSet<Enemy>();

        foreach (var enemy in _enemyContainer.Enemies)
        {
            if (fillData.FilledCells.Find((cell) => cell.Position == enemy.CurrentCell.Position))
                _enemies.Add(enemy);
        }

        if (_enemies.Count > 0)
        {
            var first = _enemies.First();
            first.Died += OnFirstEnemyDied;
        }
    }

    private void OnFirstEnemyDied(Enemy enemy)
    {
        ContourFilling?.Invoke(this);
        enemy.Died -= OnFirstEnemyDied;
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
    [SerializeField] private EndLevelTrigger _endTrigger;
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private MapFiller _mapFiller;

    private List<EnemyInContour> _enemyInContours = new List<EnemyInContour>();

    public event UnityAction<int> ScoreChanged;
    public event UnityAction<int> ScoreCombined;

    public int Score { get; private set; }

    private void OnEnable()
    {
        _enemyContainer.EnemyDied += OnEnemyDied;
        _mapFiller.StartFilled += OnStartFilled;
        _mapFiller.EndFilled += OnEndFilled;
        _endTrigger.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        _enemyContainer.EnemyDied -= OnEnemyDied;
        _mapFiller.StartFilled -= OnStartFilled;
        _mapFiller.EndFilled -= OnEndFilled;
        _endTrigger.LevelCompleted -= OnLevelCompleted;
    }

    private void OnStartFilled(FillData fillData)
    {
        var newContour = new EnemyInContour(fillData, _enemyContainer);
        newContour.ContourFilling += OnContourFilling;

        _enemyInContours.Add(newContour);
    }

    private void OnLevelCompleted()
    {
        ScoreBalance balance = new ScoreBalance();
        balance.Load(new JsonSaveLoad());
        balance.AddScore(Score);
        balance.Save(new JsonSaveLoad());
    }

    private void OnContourFilling(EnemyInContour contour)
    {
        var enemiesInContour = contour.EnemyCount;
        var scoreBonus = enemiesInContour / 10 + 1;

        Score += scoreBonus * enemiesInContour;
        ScoreChanged?.Invoke(Score);

        if (scoreBonus > 1)
            ScoreCombined?.Invoke(scoreBonus);

        contour.ContourFilling -= OnContourFilling;
    }

    private void OnEndFilled(FillData data)
    {
        var findContour = _enemyInContours.Find((contour) => contour.FillData.Equals(data));
        _enemyInContours.Remove(findContour);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        foreach (var contour in _enemyInContours)
        {
            if (contour.Has(enemy))
                return;
        }

        Score ++;
        ScoreChanged?.Invoke(Score);
    }
}
