using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelStages : MonoBehaviour
{
    [SerializeField] private LevelSpawner _spawner;
    [SerializeField] private EnemyContainer _enemyContainer;
    [SerializeField] private CurrentLevelLoader _levelNumber;

    private StageInfo _currentStage;

    public event UnityAction<int> StageCompeted;
    public event UnityAction AllStageCompeted;

    private void OnEnable()
    {
        _spawner.SpawnCompleted += OnSpawnComplete;
    }

    private void OnDisable()
    {
        _spawner.SpawnCompleted -= OnSpawnComplete;
    }

    private void OnSpawnComplete()
    {
        InitStage(0);
    }

    private void InitStage(int stage)
    {
        Vector2Int keyStageCell = _levelNumber.CurrentLevel.KeyStagesPoint[stage];
        GameCell checkpointCell = _spawner.InstCells.Find((cell) => cell.Position == keyStageCell);

        _currentStage = new StageInfo(stage, checkpointCell, _enemyContainer.Enemies);
        _currentStage.StageCompleted += OnStageComplete;
    }

    private void OnStageComplete(int stage)
    {
        StageCompeted?.Invoke(stage);
        _currentStage.StageCompleted -= OnStageComplete;

        stage++;
        if (stage < _levelNumber.CurrentLevel.KeyStagesPoint.Count)
            InitStage(stage);
        else
            AllStageCompeted?.Invoke();
    }
}
