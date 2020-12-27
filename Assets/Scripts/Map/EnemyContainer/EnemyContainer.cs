using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private LevelSpawner _spawner;

    private List<Enemy> _enemies;

    public event UnityAction<Enemy> EnemyDied;

    public IEnumerable<Enemy> Enemies => _enemies;

    private void OnEnable()
    {
        _spawner.CellObjectSpawned += OnCellObjectSpawned;
    }

    private void OnDisable()
    {
        _spawner.CellObjectSpawned -= OnCellObjectSpawned;
    }

    private void Start()
    {
        _enemies = new List<Enemy>();
    }

    private void OnCellObjectSpawned(CellObject cellObject)
    {
        if (cellObject is Enemy)
            AddEnemy(cellObject as Enemy);
    }

    private void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.Died -= OnEnemyDied;
        
        EnemyDied?.Invoke(enemy);
    }
}
