using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelStages _stages;
    [SerializeField] private PlayerInitializer _playerInitializer;
    [SerializeField] private SwipeInput _inputSystem;
    [SerializeField] private GameCanvas _gameCanvas;
    [SerializeField] private EndOfGameCanvas _endOfGameCanvas;

    private Player _player;

    private void OnEnable()
    {
        _stages.AllStageCompeted += OnAllStagesComplete;
        _playerInitializer.PlayerInitialized += OnPlayerInitialized;
    }

    private void OnDisable()
    {
        _stages.AllStageCompeted -= OnAllStagesComplete;
        _playerInitializer.PlayerInitialized -= OnPlayerInitialized;

        if (_player)
            _player.Died -= OnPlayerDied;
    }

    private void OnAllStagesComplete()
    {
        _inputSystem.enabled = false;

        _gameCanvas.Hide();
        _endOfGameCanvas.ShowWin();
    }

    private void OnPlayerInitialized(Player player)
    {
        _player = player;
        _player.Died += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        _inputSystem.enabled = false;

        _gameCanvas.Hide();
        _endOfGameCanvas.ShowLoose();
    }
}
