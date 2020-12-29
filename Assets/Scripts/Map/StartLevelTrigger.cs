using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInitializer _playerInitializer;
    [SerializeField] private UICanvas _uiCanvas;

    private Player _player;

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
        _uiCanvas.Hide();

        _player.MoveStarted -= OnPlayerMoveStarted;
        _player = null;
    }
}
