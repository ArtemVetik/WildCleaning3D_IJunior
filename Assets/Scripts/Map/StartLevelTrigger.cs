using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartLevelTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInitializer _playerInitializer;
    [SerializeField] private UICanvas _uiCanvas;
    [SerializeField] private GameCanvas _gameCanvas;

    private Player _player;

    public event UnityAction GameStarting;
    public event UnityAction GameStarted;

    private void OnEnable()
    {
        _playerInitializer.PlayerInitialized += OnPlayerInitialized;
    }

    private void OnDisable()
    {
        _playerInitializer.PlayerInitialized -= OnPlayerInitialized;

        if (_player != null)
            _player.MoveStarted -= OnPlayerMoveStarted;
    }

    private void OnPlayerInitialized(Player player)
    {
        _player = player;
        _player.MoveStarted += OnPlayerMoveStarted;
    }

    private void OnPlayerMoveStarted()
    {
        GameStarting?.Invoke();

        _uiCanvas.Hide();
        _gameCanvas.Show();

        _player.MoveStarted -= OnPlayerMoveStarted;
        _player = null;

        GameStarted?.Invoke();
    }
}
