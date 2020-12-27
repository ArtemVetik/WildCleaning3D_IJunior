using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTransition : MonoBehaviour
{
    [SerializeField] private LevelStages _levelStages;
    [SerializeField] private CurrentLevelLoader _levelLoader;
    [SerializeField] private PlayerInitializer _playerInitializer;
    [SerializeField] private LevelSpawner _spawner;
    private void OnEnable()
    {
        _levelStages.StageCompeted += OnStageComplete;
    }

    private void OnDisable()
    {
        _levelStages.StageCompeted -= OnStageComplete;
    }

    private void OnStageComplete(int stage)
    {
        stage++;
        if (stage >= _levelLoader.CurrentLevel.KeyStagesPoint.Count)
            return;

        var nextPosition = _levelLoader.CurrentLevel.KeyStagesPoint[stage];
        var nextStageCell = _spawner.InstCells.Find(cell => cell.Position == nextPosition);

        _playerInitializer.InstPlayer.Replace(nextStageCell);
    }
}
