using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradePlayer : MonoBehaviour
{
    [SerializeField] private CleanersDataBase _dataBase;
    [SerializeField] private PlayerInitializer _playerInitializer;
    [SerializeField] private EndLevelTrigger _endTrigger;
    [SerializeField] private PlayerSelector _playerSelector;

    public event UnityAction<IPlayerData, IPlayerData> PlayerUpgraded;

    private void OnLevelCompleted()
    {
        CleanerInventory inventory = new CleanerInventory(_dataBase);
        inventory.Load(new JsonSaveLoad());

        var player = _playerInitializer.InstPlayer;
        var oldData = player.PlayerDataClone;

        if (_playerSelector.HasInDictionary(player) && inventory.Contains(player.DefaultCharacteristics) == false)
        {
            PlayerUpgraded?.Invoke(oldData, oldData);
            return;
        }

        var newData = player.Upgrade();

        PlayerUpgraded?.Invoke(oldData, newData);
    }

    private void OnEnable()
    {
        _endTrigger.LevelCompleted += OnLevelCompleted;
        _endTrigger.LevelFailed += OnLevelFailed;
    }

    private void OnLevelFailed()
    {
        CleanerInventory inventory = new CleanerInventory(_dataBase);
        inventory.Load(new JsonSaveLoad());

        var player = _playerInitializer.InstPlayer;
        var oldData = player.PlayerDataClone;

        if (_playerSelector.HasInDictionary(player) && inventory.Contains(player.DefaultCharacteristics) == false)
        {
            PlayerUpgraded?.Invoke(oldData, oldData);
            return;
        }
        var newData = player.Downgrade();

        PlayerUpgraded?.Invoke(oldData, newData);
    }

    private void OnDestroy()
    {
        _endTrigger.LevelCompleted -= OnLevelCompleted;
        _endTrigger.LevelFailed -= OnLevelFailed;
    }
}
