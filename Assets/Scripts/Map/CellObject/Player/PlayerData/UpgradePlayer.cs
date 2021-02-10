using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlayer : MonoBehaviour
{
    [SerializeField] private CleanersDataBase _dataBase;
    [SerializeField] private PlayerInitializer _playerInitializer;
    [SerializeField] private EndLevelTrigger _endTrigger;
    [SerializeField] private PlayerSelector _playerSelector;

    private void OnLevelCompleted()
    {
        CleanerInventory inventory = new CleanerInventory(_dataBase);
        inventory.Load(new JsonSaveLoad());

        var player = _playerInitializer.InstPlayer;

        if (_playerSelector.HasInDictionary(player) && inventory.Contains(player.DefaultCharacteristics) == false)
        {
            Debug.Log("HAS!");
            return;
        }

        _playerInitializer.InstPlayer.Upgrade();
    }

    private void OnEnable()
    {
        _endTrigger.LevelCompleted += OnLevelCompleted;
    }

    private void OnDestroy()
    {
        _endTrigger.LevelCompleted -= OnLevelCompleted;
    }
}
