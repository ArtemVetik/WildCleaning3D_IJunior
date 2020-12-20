using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private MapFiller _mapFiller;

    private Dictionary<Enemy, int> _comboKilledEnemy = new Dictionary<Enemy, int>();

    public event UnityAction<int> ScoreChanged;

    public int Score { get; private set; }

    private void OnEnable()
    {
        _enemyContainer.EnemyDied += OnEnemyDied;
        _mapFiller.StartFilled += OnStartFilled;
    }

    private void OnDisable()
    {
        _enemyContainer.EnemyDied -= OnEnemyDied;
        _mapFiller.StartFilled -= OnStartFilled;
    }

    private void OnStartFilled(FillData fillData)
    {
        List<Enemy> enemiesInСontour = new List<Enemy>();

        foreach (var enemy in _enemyContainer.Enemies)
        {
            if (enemy is Virus)
                continue;

            if (fillData.FilledCells.Find((cell) => cell.Position == enemy.CurrentCell.Position))
                enemiesInСontour.Add(enemy);
        }

        int newScore = enemiesInСontour.Count / 10 + 1;
        foreach (var enemy in enemiesInСontour)
            _comboKilledEnemy.Add(enemy, newScore);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        if (_comboKilledEnemy.ContainsKey(enemy))
        {
            Score += _comboKilledEnemy[enemy];
            _comboKilledEnemy.Remove(enemy);
        }
        else
        {
            Score += 1;
        }
        ScoreChanged?.Invoke(Score);
    }
}
